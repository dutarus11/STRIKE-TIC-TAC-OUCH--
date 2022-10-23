using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public event EventHandler click;

    private TMPro.TextMeshPro text;


    public void SetText(string _text) // set X O andSquare Objects 
    {
        //if (text == null)
        //{
        //    text = GetComponentInChildren<TMPro.TextMeshPro>();
        //}
        //text.text = _text;
    }
    private void OnMouseDown()
    {
        click?.Invoke(this, EventArgs.Empty);
    }
}
