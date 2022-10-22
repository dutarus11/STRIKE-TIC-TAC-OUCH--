using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public event EventHandler click;

    private void OnMouseDown()
    {
        click?.Invoke(this, EventArgs.Empty);
    }
}
