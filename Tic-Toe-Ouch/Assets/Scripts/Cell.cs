using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public GameObject[] cells;

    void Awake()
    {
        cells = GameObject.FindGameObjectsWithTag("Cell1");

        foreach (GameObject cell in cells)
        {
            cell.SetActive(false);
        }

    }

    void Update()
    {
       
        
    }

   

}
