using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Cell[] getCell;


    void Awake()
    {
        GameObject gameCell = GameObject.FindGameObjectWithTag("cBatch");
        
        //cellScript.cell.SetActive(false);
       
       // getCell = GetComponent<Cell>();
        // getCell.cell = GameObject.Find("cBatch");
        //getCell.SetActive(true);
    }
    void Start()
    {

       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
