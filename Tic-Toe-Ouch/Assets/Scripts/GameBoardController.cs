using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoardController : Cell
{
    public Cell[] cells;
    public TMPro.TextMeshProUGUI token;
    public Button newGame;
    
    private string[] tokens = new string[] { "X", "0" };
    private int currentToken = 0;
    private GameObject cellObj1;
    private TicTacToeOuchEngine engine = new TicTacToeOuchEngine();
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
            // cell.SetText("");
            
            cellObj1 = cellObj;
            cellObj1.SetActive(false);
        }
        
    }

    public void NewGame()
    {
        currentToken = 0;
        token.text = tokens[currentToken];
        newGame.interactable = false;
    }
    private void clicked(int index)
    {
        print(index);
    }

    
}
