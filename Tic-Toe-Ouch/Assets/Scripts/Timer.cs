using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerTxt;
    
   

    [Header("Timer Settings")]
    public float totalTime;
    public bool ctDown = true;
    public bool isActive = false;

    [Header("Second Component")]
    public GameObject warningTxt;

    private void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
        SetTimerText();
        SetWarningTxt();
                
    }

    public void CountDown()
    {
        if (ctDown == true)
        {
            totalTime -= Time.deltaTime;
            if (totalTime <= 10)
            {
                isActive = true;
                timerTxt.color = Color.red;                
                if (totalTime <= 0)
                {
                    Debug.Log("End scene!");
                }                
            }
           
        }
    }

    private void SetTimerText()
    {
        timerTxt.text = totalTime.ToString("0");
    }

    private void SetWarningTxt()
    {
        if (isActive == true && totalTime < 8)
        {
            warningTxt.SetActive(true);           
        }

        if (totalTime < 5)
        {
            isActive = false;
            warningTxt.SetActive(false);
        }
        
    }

  

   
  
}
