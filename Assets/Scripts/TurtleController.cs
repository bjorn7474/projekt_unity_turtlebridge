using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : MonoBehaviour
{


    //public List<Transform> positions = new List<Transform>();
    public Transform positions;
    private const int MAX_POS_INDEX = 3;
    int incr_pos;
    bool dive_fullfilled = false;
    private int mod = 20;
    private int modSleep = 600;
    private int frameCounter;
    private const int SOUTH = 1;
    private const int NORTH = -1;
    private int dir = SOUTH;
    int randomDiveNmb;
    bool isDiving = false;

    public bool turtleSleep = false;
    private const int TURTLE_SLEEP_START = 300;
    public int turtleSleepVal = 40; // TURTLE_SLEEP_START;

    private int beneath_surface_idx;


    /**
        Sköldpaddan sover - dvs de kan inte för tillfälligt dyka. Då funktionen returnerar false kan den börja dyka närsom.
    **/
    private bool turtle_sleep()
    {

        turtleSleepVal--;
        if (turtleSleepVal <= 0)
        {
            turtleSleep = false;
            turtleSleepVal = TURTLE_SLEEP_START;
            transform.GetComponent<SpriteRenderer>().color = Color.yellow;
            transform.rotation = Quaternion.Euler(0, 0, 45);
            return turtleSleep;
         

        } else
        {
            transform.GetComponent<SpriteRenderer>().color = Color.white;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
            

        return true;
    }

    // Use this for initialization
    void Start()
    {

        transform.position = positions.GetChild(0).transform.position;
        turtleSleep = true; // börja med att sova dvs att sköldpaddorna väntar med att dyka
        turtle_sleep();
    }



    // Update is called once per frame

    void Update()
    {


        randomDiveNmb = Random.Range(1, 100);
        if (randomDiveNmb >= 99 && !isDiving && !turtleSleep)
        {
            
            isDiving = true;
        }

        if (isDiving)
        {

            isDiving = turtleDive();

            if (!isDiving)
            {
                turtleSleep = true;
               
            }

        }

        if (turtleSleep)
            turtle_sleep();


    }

    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(20);
        print(Time.time);
    }


    private bool turtleDive()
    {


        if (dir == SOUTH && beneath_surface_idx == 0)
        {

      


        } else if (dir == SOUTH && beneath_surface_idx == 2)
        {
            //transform.GetComponent<SpriteRenderer>().color = Color.white;

        }


        //Debug.Log("" + beneath_surface_idx);

        if (frameCounter % mod == 0)
        {
            beneath_surface_idx += dir;
        }
        transform.position = positions.GetChild(beneath_surface_idx).transform.position;

        if (beneath_surface_idx >= MAX_POS_INDEX)
        {

          
            dir = NORTH;
        }
        else if (beneath_surface_idx <= 0)
        {
            dir = SOUTH;
            return false;
        }


        frameCounter++;
        return true;


    }

    public int getBeneathSurfaceIdx()
    {

        return beneath_surface_idx;
    }





}