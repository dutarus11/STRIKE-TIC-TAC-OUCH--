using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

   
    public GameObject cell;
    public GameObject alter;
    public Transform transPos;
    public bool isTapped = false;
    
    void Start()
    {
        
                   
    }

    void Update()
    {

    }

    public void OnMouseDown() //click and point to create and deestroy objects 
    {
        isTapped = true;
        transPos = alter.transform;       
        Instantiate(cell, transform.position, Quaternion.identity);
        Debug.Log("Alter Destroyed!");
        DestroyGameObject();
    }

    void DestroyGameObject()
    {
        DestroyImmediate(alter, true);
    }
}
