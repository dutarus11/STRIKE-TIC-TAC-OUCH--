using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    //public GameObject cell;
    public GameObject sp;
    public GameObject sp2;
    public bool isTapped = false;
    //bool isEnabled;
    //public GameObject getAlter;
    void Start()
    {
        //cell = GameObject.FindGameObjectWithTag("Cell1");
        // getAlter = GameObject.FindGameObjectWithTag("alterC");

        //  DisabledObjects();
       
       
       
    }

    void Update()
    {
       // ActivateObjects();

    }

   //void DisabledObjects()
   //{
   //     //cell = GameObject.FindGameObjectWithTag("Cell1");
   //     cell.SetActive(false);
   //     getAlter.SetActive(true);

   // }

    //void ActivateObjects()
    //{
        
    //}
    public void OnMouseDown()
    {
        //if (true)
        //{

        //}
        //  getAlter.SetActive(false);
        //   cell.SetActive(true);

        //print(getAlter.name);
        //sp.enabled ^= true;
        isTapped = true;
        GameObject childObject = Instantiate(sp) as GameObject;
        
        sp2.SetActive(false);


    }
}
