// (c) Copyright 2015 Luke Light&Magic. All rights reserved.

using UnityEngine;
using System.Collections;

public class MatTexSheetAnimator : MonoBehaviour {

  public float TexSheetTotalTime = 1;
  public int TilesX = 1, TilesY = 1;
  public bool TexSheetRandom = false, Loop = true;

  private Material mat;
  private Vector2 texOffset;
  private int _tx, _ty;
  private float[] sheetCoordinates;
  private float timerSheet;
  private bool loop = true;

  void Awake() {
    if (GetComponent<Renderer>().materials.Length != 0) {
      mat = GetComponent<Renderer>().materials[0];
      if (mat) {
        if (TilesX <= 0) TilesX = 1;
        if (TilesY <= 0) TilesY = 1;
        mat.SetTextureScale("_MainTex", new Vector2(1f / TilesX, 1f / TilesY));
      }
    }
  }

  void Update() {
    Material updateMat = mat;
    if (updateMat && loop) {
      if (TilesX <= 0) TilesX = 1;
      if (TilesY <= 0) TilesY = 1;

      //Texture sheet animation 
      if (timerSheet >= TexSheetTotalTime / (TilesX * TilesY)) {
        timerSheet = 0;
        if (!TexSheetRandom) {
          //Sheet animation
          if (++_tx >= TilesX) {
            _tx = 0;
            if (++_ty >= TilesY)
              _ty = 0;
          }
        }
        else {
          //Sheet random animation
          for (var i = 0; i < 10; i++) {
            var randTx = Random.Range(0, TilesX);
            var randTy = Random.Range(0, TilesY);
            if (!(randTx == _tx && randTy == _ty)) {
              _tx = randTx;
              _ty = randTy;
              break;
            }
          }
        }

        if (_tx == 0 && _ty == 0) {
          if (!Loop) {
            loop = false;
            return;
          }
          updateMat.SetTextureScale("_MainTex", new Vector2(1f / TilesX, 1f / TilesY));
        }
        texOffset.x = (float)_tx / TilesX;
        texOffset.y = (float)_ty / TilesY;
        updateMat.SetTextureOffset("_MainTex", texOffset);

      }
      else
        timerSheet += Time.deltaTime;
    }
  }

}
