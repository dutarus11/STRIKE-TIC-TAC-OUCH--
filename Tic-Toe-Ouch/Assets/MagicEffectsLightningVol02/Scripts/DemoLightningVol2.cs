// (c) Copyright 2015 Luke Light&Magic. All rights reserved.

using UnityEngine;
using System.Collections;

public class DemoLightningVol2 : MonoBehaviour {
  public Animation CharAnim, ProjAnim;
  public Transform surround_point, ground;
  public Transform[] camPos;
  public Transform[] effects = { null };
  public Transform[] ProjEffects = { null };

  

  private const string animIdle = "Stand", animBeHit01 = "BeHit01", animBeHit02 = "BeHit02",
                        animWarmUp = "SpellCastDirected", animWarmUp2 = "ChannelCastDirected",
                        animWarmUp3 = "ChannelCastOmni", animProj1 = "anim_proj", animProj2 = "anim_proj2";

  private Animation anim;
  private int subState;
  private int effectIndex, goIndex, fpsNumber;
  private float repeatTime = 4;
  private float timer;
  private float fpsTimer;
  private Transform effect;
  private float fps, fpsTotal;
  private bool setCam0 = true, setCam3;
  private string effectName;


  void Start() {
    anim = CharAnim;
    PlayEffects();
    effectName = effects[effectIndex].name;
    //InitEffect();
  }

  void OnGUI() {
    GUILayout.Label("Effect Name : " + effectName);
    GUILayout.Label("Effect Index : " + effectIndex);
    GUILayout.Label("Total Effects : " + effects.Length);

    GUILayout.BeginHorizontal();
    GUILayout.Label("View Scrollwheel", GUILayout.MaxWidth(100));
    if (GUILayout.Button("+", GUILayout.Width(30))) UpdateCam(0.1f);
    if (GUILayout.Button("-", GUILayout.Width(30))) UpdateCam(-0.1f);
    GUILayout.EndHorizontal();

    //Go index
    GUILayout.BeginHorizontal();
    bool goButton = GUILayout.Button("Go Effect Index", GUILayout.Width(120));
    GUILayout.Label(" ==> ", GUILayout.Width(30));
    int.TryParse(GUILayout.TextField(goIndex.ToString(), GUILayout.MaxWidth(50)), out goIndex);
    if (goIndex >= effects.Length) goIndex = effects.Length - 1;
    Event e = Event.current;
    if (e.isKey && e.keyCode == KeyCode.Return || goButton)
      GoEffectIndex();
    GUILayout.EndHorizontal();

    //GUILayout.BeginHorizontal();
    //GUILayout.Label("Repeat Time : ", GUILayout.MaxWidth(85));
    //repeatTime = float.Parse(GUILayout.TextField(repeatTime.ToString(), GUILayout.MaxWidth(50)));
    //GUILayout.EndHorizontal();

    if (GUILayout.Button("Show/Hide Ground", GUILayout.Width(120))) {
      if (ground != null) {
        ground.gameObject.SetActive(!ground.gameObject.activeSelf);
      }
    }
    GUILayout.Space(6);

    if (GUILayout.Button("Previous Effect", GUILayout.Width(120))) {
      //Previous Effect
      PlayEffects(false);
      --effectIndex;
      if (effectIndex < 0)
        effectIndex = effects.Length - 1;
      SelectEffect();
    }
    else if (GUILayout.Button("Next Effect", GUILayout.Width(120))) {
      //Next Effect
      PlayEffects(false);
      ++effectIndex;
      if (effectIndex >= effects.Length)
        effectIndex = 0;
      SelectEffect();
    }
    GUILayout.Space(6);
    GUILayout.Label("fps = " + fps);
  }

  void Update() {
    UpdateActions();
    UpdateCam(Input.GetAxis("Mouse ScrollWheel"));
    
    //Checking fps
    if (Time.time > fpsTimer) {
      fps = fpsTotal / fpsNumber;
      fpsTimer = Time.time + 0.5f;
      if (fpsTotal >= 10000)
        fpsTotal = fpsNumber = 0;
    }
    else {
      fpsTotal += 1 / Time.deltaTime;
      ++fpsNumber;
    }
  }



  void UpdateActions() {
    //Select the action behavior
    if (effectIndex == 0 || effectIndex == 1 || effectIndex >= 10)
      anim.CrossFade(animIdle, 0.2f);
    else if (effectIndex == 2 || effectIndex == 3)
      anim.CrossFade(animWarmUp2, 0.2f);
    else if (effectIndex == 4 || effectIndex == 5 || effectIndex == 8 || effectIndex == 9)
      BeHittingAction();
    //else if (effectIndex == 10 || effectIndex == 11)
    //  anim.CrossFade(animBeHit01, 0.2f);

    if (effectIndex >= 12)
      RepeatAction();
  }

  void UpdateCam(float scrollwheel) {
    if (scrollwheel != 0) {
      Transform trans = Camera.main.transform;
      Vector3 pos = trans.localPosition + scrollwheel * trans.forward * 10;
      if (pos.z <= -1.4f && pos.z >= -25)
        trans.localPosition = pos;

      if (effectIndex == 18) {
        if (pos.z > -9 && setCam0 && scrollwheel > 0) {
          SetCamPos(0);
          setCam0 = false;
          setCam3 = true;
        }
        else if (pos.z < -10.5f && setCam3 && scrollwheel < 0) {
          SetCamPos(3);
          setCam3 = false;
          setCam0 = true;
        }
      }
    }
  }

  void SelectCamPos() {
    var camPosID = 0;
    if (effectIndex == 2 || effectIndex == 3) camPosID = 1;
    else if (effectIndex == 16 || effectIndex == 17) camPosID = 2;
    else if (effectIndex == 18 ) camPosID = 3;
    SetCamPos(camPosID);
  }

  void SetCamPos(int posID) {
    var trans = Camera.main.transform;
    trans.parent = camPos[posID];
    trans.localPosition = Vector3.zero;
    trans.localRotation = Quaternion.identity;
    trans.parent = null;
  }

  //Effect Actions
  bool SpecificAction() {
    if (effectIndex == 4 || effectIndex == 5 || effectIndex == 8 || effectIndex == 9) {
      subState = 0;
      repeatTime = 1;
      if (effectIndex == 8 || effectIndex == 9) repeatTime = 1.5f;

      if (effectIndex == 4) PlayProjEffects(0);
      else if (effectIndex == 5) PlayProjEffects(1);
      else if (effectIndex == 8) PlayProjEffects(2);
      else if (effectIndex == 9) PlayProjEffects(3);
      return true;
    }
    else {
      timer = 0;
      if (effectIndex == 12 || effectIndex == 13 || effectIndex == 16 || effectIndex == 17) repeatTime = 6;
      else if (effectIndex == 14 || effectIndex == 15) repeatTime = 3;
      else if (effectIndex == 18 ) repeatTime = 7;
      return false;
    }
  }

  void BeHittingAction() {
    //BeHit effect action
    if (subState == 0) {
      //Substate 0 proj
      if (!ProjAnim.isPlaying) {
        subState = 1;
        PlayEffects();
      }
      else
        anim.CrossFade(animIdle, 0.2f);

      if (effectIndex == 8 || effectIndex == 9) {
        if (ProjAnim[animProj2].time >= 0.47777f) {
          subState = 1;
          PlayEffects();
        }
      }
    }
    else if (subState == 1) {
      //Substate 1 behitting
      if (!anim.isPlaying) {
        subState = 2;
        timer = 0;
      }
      else {
        if (effectIndex == 8 || effectIndex == 9)
          anim.CrossFade(animBeHit02, 0.2f);
        else
          anim.CrossFade(animBeHit01, 0.2f);
      }

    }
    else if (subState == 2) {
      //Substate 2 repeat
      if (timer >= repeatTime) {
        subState = 0;
        SpecificAction();
      }
      else {
        RunTimer();
        anim.CrossFade(animIdle, 0.2f);
      }
    }
  }

  void RepeatAction() {
    if (timer >= repeatTime) {
      timer = 0;
      PlayEffects();
    }
    else
      RunTimer();
  }


  //local functions
  void PlayEffects(bool On = true) {
    if (On) {
      EffectController ec = effects[effectIndex].GetComponent<EffectController>();
      if (ec)
        ec.StopAllEffects();
      else
        effects[effectIndex].gameObject.SetActive(false);
    }
    else {
      var pss = effects[effectIndex].GetComponentsInChildren<ParticleSystem>();
      foreach (var ps in pss)
        ps.Clear();
    }
    effects[effectIndex].gameObject.SetActive(On);
  }

  void PlayProjEffects(int index) {
    if (index == 0 || index == 1)
      ProjAnim.Play(animProj1);
    else if (index == 2 || index == 3)
      ProjAnim.Play(animProj2);
    ProjEffects[index].gameObject.SetActive(true);
  }

  void RunTimer() {
    timer += Time.deltaTime;
  }

  void GoEffectIndex() {
    if (goIndex == effectIndex || goIndex < 0 || goIndex >= effects.Length)
      return;
    PlayEffects(false);
    effectIndex = goIndex;
    SelectEffect();
  }

  void SelectEffect() {
    if (!SpecificAction())
      PlayEffects();
    effectName = effects[effectIndex].name;
    SelectCamPos();
  }

  

  IEnumerator DelayActive(GameObject effect) {
    yield return new WaitForSeconds(0.1f);
    if (effect != null)
      effect.SetActive(true);
  }

}
