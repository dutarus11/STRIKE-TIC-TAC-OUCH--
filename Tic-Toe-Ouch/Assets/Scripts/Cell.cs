using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
  
    public GameObject cell;
    public GameObject negCell;
    public GameObject alter;
    Projectile projectileScript;
    public Transform transPos;
    [SerializeField]
    private bool isTapped = false;

    int gameObjectCount;

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
        StartCoroutine(WaitForObject());       
        Debug.Log("Alter Destroyed!");
        DestroyGameObject();
    }
    IEnumerator WaitForObject()
    {
        if (isTapped == true)
        {
            Instantiate(negCell, -transform.position, Quaternion.identity);
            isTapped = false;
            
        }
        
        yield return new WaitForSeconds(3f);
        DestroyGameObject();
    }
    void DestroyGameObject()
    {   
        if(gameObject == alter)
        {
            DestroyImmediate(alter, true);
        }       
        else
        {
            DestroyImmediate(cell, true);
        }

       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NegCell1")
        {
            Debug.Log("Collision Succeeded");
        }
        
        //Destroy(negCell);
    }
}
