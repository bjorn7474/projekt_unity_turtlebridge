using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeViewController : MonoBehaviour
{

    [SerializeField]
    public const int N_LIVES = 4;
    private int lives = N_LIVES;
    public float distance = 1f;

    private void Start()
    {
        for (int i = 1; i < lives; i++)
        {
            GameObject newLife = Instantiate(transform.GetChild(0).gameObject);
            newLife.transform.SetParent(transform);
            Vector3 pos = newLife.transform.position;
            pos.x += (i * distance);
            newLife.transform.position = pos;

        }
    }


    public bool RemoveLife()
    {

        lives--;
        if (lives >= 0)
            transform.GetChild(lives).gameObject.SetActive(false);

        if (lives < 0)
            return false;

        return true;

    }

  

    public void RestoreAllLives()
    {

        foreach (Transform child in transform)
        {

            lives = N_LIVES;
            child.gameObject.SetActive(true);
        }

    }


}
