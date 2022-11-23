using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Cell cellScript;
    public GameObject projectile;
    public Vector3 posOffSet;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        //if (cellScript.isTapped == true)
        //{
        //    Debug.Log("Other success!");
        //    cellScript.transPos = cellScript.alter.transform;
        //    Instantiate(projectile, transform.position, Quaternion.identity);
        //};
        //if (Input.GetMouseButtonDown(0))
        //{
        //    GameObject a = Instantiate(projectile) as GameObject;
        //    a.transform.position = cellScript.alter.transform.position;
        //    //Instantiate(projectile, transform.position, Quaternion.identity);
        //    Debug.Log("Second Success");
        //}

        if (cellScript.alter == null)
        {           
            //Explode();
            Debug.Log("Success with explosion!");
        }
    }
    
    public void Explode()
    {
       
            GameObject explosionFX = Instantiate(projectile, transform.position + posOffSet, Quaternion.identity) as GameObject;
            Destroy(explosionFX, 2);       
    }
}
