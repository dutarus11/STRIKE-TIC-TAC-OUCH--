using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardController : MonoBehaviour
{
    public Cell[] cells;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var cell in cells)
        {
            cell.click += (o, e) =>
              {
                  var index = Array.IndexOf(cells, o);
                  clicked(index);
              };
        }
    }

    private void clicked(int index)
    {
        print(index);
    }

    
}
