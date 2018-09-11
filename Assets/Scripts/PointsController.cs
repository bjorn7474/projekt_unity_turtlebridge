using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsController : MonoBehaviour
{

    TextMeshPro textMesh;
    //int points = 0;
    private int points;

    private void Start()
    {
        textMesh = GetComponent<TextMeshPro>();

        //textMesh.SetText("0");

        if (textMesh == null)
        {
            Debug.LogError("Textmesh component not found!");
        }
    }


    public void SetPoint(int points)
    {
        this.points += points;
        textMesh.SetText(this.points.ToString());

    }


}
