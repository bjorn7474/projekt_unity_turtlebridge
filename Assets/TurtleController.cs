using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : MonoBehaviour {


    //public List<Transform> positions = new List<Transform>();
    public Transform positions;
    int N_POSITIONS = 4;
    int incr_pos;
    bool dive_fullfilled = false;


    // Use this for initialization
    void Start () {

        transform.position = positions.GetChild(3).transform.position;
    }
	
	// Update is called once per frame
	void Update () {


        /*Debug.Log("" + incr_pos);
        if (incr_pos >= N_POSITIONS - 1)
        {
            dive_fullfilled = true;
        }



        transform.position = positions.GetChild(incr_pos).transform.position;
      

        if (!dive_fullfilled)
            incr_pos++;
            */
        transform.position = positions.GetChild(3).transform.position;

    }
}
