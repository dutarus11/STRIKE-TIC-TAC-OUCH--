using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public TextMeshProUGUI winningObj;
    public int lives;
 
    private Cell count;

   // int currentLives;
   // public static int maxLives = 3;
   //// int damage = 1;
   // int finalDeath;

    bool lostLife = false;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        WinningCondition();
    }

  
    private void WinningCondition()
    {
        if (gameObject.tag == "Cell1" && count.gameObjectCount ==  3)
        {
            Debug.Log("You win");
        }
        
        //if (count.gameObjectCount == 3)
        //{
        //    Debug.Log("You win!");
        //}
    }
   

   
    
}
