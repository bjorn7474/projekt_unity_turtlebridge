using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierController : MonoBehaviour {



    public List<Transform> positions = new List<Transform>();
    public List<GameObject> turtles = new List<GameObject>();
    public List<Transform> diePositions2 = new List<Transform>();
    public List<Transform> diePositions3 = new List<Transform>();
    public List<Transform> diePositions4 = new List<Transform>();
    public List<Transform> diePositions5 = new List<Transform>();

    private const int N_TURTLES = 4;
    private const int N_POS_BENEATH_SURFACE = 4;
    bool hasPackage = true; // han har ett paket som nu ska lämnas på andra sidan.

    


    public Texture[] myTextures = new Texture[1];

    int beneathSurfIdx;

    int currentPosition = 0;

    int frameCnt;
    int mod = 10;

    private bool isDying, dead;
    private int divingTurtlePosition = -1; // positionen för sköldpaddan som dyker på den platsen där bäraren befinner sig

    public PointsController pointsController;
    public LifeViewController lifeViewController;
  

    private void OnEnable()
    {
        Input.OnLeftPressed += Input_OnLeftPressed;
        Input.OnRightPressed += Input_OnRightPressed;
    }

    private void OnDisable()
    {
        Input.OnLeftPressed -= Input_OnLeftPressed;
        Input.OnRightPressed -= Input_OnRightPressed;
    }

    private void Start()
    {


        //instansiera gameobjektet Points
        GameObject points = GameObject.Find("Points");
        pointsController = points.GetComponent<PointsController>();

        //instansiera gameobjektet Lives
        GameObject lives = GameObject.Find("Lives");
        lifeViewController = lives.GetComponent<LifeViewController>();

        transform.position = positions[currentPosition].position;

        Renderer m_ObjectRenderer = GetComponent<Renderer>();
        m_ObjectRenderer.material.mainTexture = myTextures[0];

      

    }




    void Input_OnLeftPressed()
    {
        if (currentPosition > 0)
        {
            currentPosition--;
            transform.position = positions[currentPosition].position;

            Debug.Log("position = " + currentPosition);

            leaveRetreivePackage();

        }

      
    }

    void Input_OnRightPressed()
    {
        if (currentPosition < positions.Count - 1)
        {
            currentPosition++;
            transform.position = positions[currentPosition].position;

            leaveRetreivePackage();
        }

      
    }


    void surfaceChecker()
    {


        // utför endast och endast OM bäraren inte håller på att dö - då är ju divingTurtlePosition redan satt och det är den information vi behöver för att "dränka" honom.
        if (!isDying)
        {
            int[] posTurtle = new int[N_TURTLES];
            for (int i = 0; i < N_TURTLES; i++)
            {
                posTurtle[i] = turtles[i].GetComponentInChildren<TurtleController>().getBeneathSurfaceIdx();
            }


            //kontrollerar om spelaren är ovanpå en sköldpadda som dyker. Dyker gör den om posTurtle != 0
            for (int i = 0; i < N_TURTLES; i++)
            {

                if (currentPosition == i + 1 && posTurtle[N_TURTLES - 1 - i] != 0)
                {
                    divingTurtlePosition = i;
                    isDying = true;
                }
            }
        } 

    }

    private void Update()
    {

        surfaceChecker();

        if (divingTurtlePosition != -1)
        {


            if (frameCnt % mod == 0)
            {
                beneathSurfIdx++;
            }

            if (beneathSurfIdx > N_POS_BENEATH_SURFACE - 1)
            {
                beneathSurfIdx = 0;
             
                dead = true;

            }

        
            switch (divingTurtlePosition)
            {


                case 0:
                    transform.position = diePositions2[beneathSurfIdx].position;
                   
                    break;
                case 1:
                    transform.position = diePositions3[beneathSurfIdx].position;
                    break;
                case 2:
                    transform.position = diePositions4[beneathSurfIdx].position;
                    break;
                case 3:
                    transform.position = diePositions5[beneathSurfIdx].position;
                    break;


            }

        }

        if (dead)
        {
            decreaseLife();
            resetGame();

        }

            

        frameCnt++;


     

    }

    private void resetGame()
    {
        currentPosition = 0;
        transform.position = positions[currentPosition].position;
        dead = false;
        isDying = false;
        hasPackage = true;
        divingTurtlePosition = -1;
        beneathSurfIdx = 0;

    }

    private void leaveRetreivePackage()
    {
        
        // paketet lämnas vid andra änden och sätts till false - för att få ny poäng måste han återvända till den andra landsänden igen för att hämta ett nytt paket.
        if (currentPosition == 5 && hasPackage)
        {
            Debug.Log(currentPosition);
            pointsController.SetPoint(1);
            hasPackage = false;

        }

        if (currentPosition == 0)
            hasPackage = true;

        

    }

    private void decreaseLife()
    {

        bool nextLife = lifeViewController.RemoveLife();
        if (!nextLife)
        {

            lifeViewController.RestoreAllLives();
        }
        Debug.Log("GAMING = " + nextLife);
    }


   
}
