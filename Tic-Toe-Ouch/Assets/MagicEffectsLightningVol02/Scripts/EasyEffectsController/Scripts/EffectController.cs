// (c) Copyright 2015 Luke Light&Magic. All rights reserved.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectController : MonoBehaviour {

  public float TotalPlayTime = 3;
  public bool PlayOnAwake = true;
  public bool DestoryOnEnd = true;
  public Transform[] EffectList = { null };
  public float[] EffectPlayDelayTimeList = { 0 };
  public float[] EffectStopTimeList = { 0 };
  public Animation ControlAnim;
  public bool AnimatingEndToPlayOrStop = true;
  public bool UseExistedAnimation;
  public bool StopToRewind = true;

  private bool isCheckAnimation;

  void Awake() {
    if (transform.childCount != 0)
      if (EffectList[0] == null) {
        //A single effect automatic installation of Transform
        var oneEffect = transform.GetChild(0);
        if (oneEffect != null)
          EffectList[0] = oneEffect;
      }

    foreach (var trans in EffectList)
      ActiveEffect(trans, false);

    if (!PlayOnAwake)
      gameObject.SetActive(false);
  }

  void Update() {
    if (isCheckAnimation) {
      //print("ControlAnim.isPlaying = " + ControlAnim[ControlAnim.clip.name].time);
      if (!ControlAnim.isPlaying) {
       if (UseExistedAnimation) {
         if (AnimatingEndToPlayOrStop)
           PlayAllEffects();
         else
           StopAllEffects();
       }
        isCheckAnimation = false;
        if (StopToRewind)
          StartCoroutine(RewindAnimationPositon());
      }
    }
    
  }

  void OnEnable() {
    //print("OnEnbale!!!!");
    if (EffectList == null)
      return;

    if (ControlAnim != null && UseExistedAnimation) {
      isCheckAnimation = true;
      if (!AnimatingEndToPlayOrStop)
        PlayAllEffects();
      else
        StopAllEffects();
    }
    else
      PlayAllEffects();
  }

  void PlayAllEffects() {
    StopAllCoroutines();
    StartCoroutine(TotalTimeToStop(TotalPlayTime));
    
    for (var i = 0; i < EffectList.Length; i++) {
      //According to the delay set to play a ettect
      bool useDelayTime = false;
      if (EffectPlayDelayTimeList != null)
        if (i < EffectPlayDelayTimeList.Length)
          if (EffectPlayDelayTimeList[i] > 0)
            useDelayTime = true;
      if (useDelayTime)
        StartCoroutine(DelayPlayEffect(EffectPlayDelayTimeList[i], i));
      else
        ActiveEffect(EffectList[i]);

      //Stop playing a special effects
      bool useStopTime = false;
      if (EffectStopTimeList != null)
        if (i < EffectStopTimeList.Length)
          if (EffectStopTimeList[i] > 0 && EffectStopTimeList[i] < TotalPlayTime)
            useStopTime = true;
      if (useStopTime)
        StartCoroutine(DelayPlayEffect(EffectStopTimeList[i], i, false));

    }
  }

  public void StopAllEffects() {
    for (var i = 0; i < EffectList.Length; i++) {
      if (DestoryOnEnd) {
        //Destory the non self root effects
        if (EffectList[i] != null)
          Destroy(EffectList[i].gameObject);
      }
      else
        ActiveEffect(EffectList[i], false);
    }
    //Destory root
    if (DestoryOnEnd)
      Destroy(gameObject);
  }

  void ActiveEffect(Transform effectTrans, bool activate = true) {
    if (effectTrans != null) {
      if (!activate) {
        var pss = effectTrans.GetComponentsInChildren<ParticleSystem>();
        foreach (var ps in pss)
          ps.Clear();
      }
      effectTrans.gameObject.SetActive(activate);
    }
  }

  void SfxPlay(AudioClip sfxClip, bool directPlay = true) {
    AudioSource sfx = GetComponent<AudioSource>();
    if (sfx == null) {
      sfx = gameObject.AddComponent<AudioSource>();
      sfx.playOnAwake = false;
    }

    if (!sfx.isPlaying || directPlay) {
      sfx.clip = sfxClip;
      sfx.Play();
    }

  }
	
	IEnumerator  TotalTimeToStop(float waitTime){
    //The total playing time (seconds), to the time to stop the effects, equal to 0 for the infinite time
    if (waitTime > 0) {
      yield return new WaitForSeconds(waitTime);
      StopAllEffects();
      gameObject.SetActive(false);
    }
  }

  IEnumerator DelayPlayEffect(float delayTime, int effectIndex, bool playEffect = true) {
    if (delayTime < TotalPlayTime) {
      yield return new WaitForSeconds(delayTime);
      ActiveEffect(EffectList[effectIndex], playEffect);
    }
  }

  IEnumerator RewindAnimationPositon() {
    ControlAnim.Play();
    yield return null;
    ControlAnim.Stop();
  }


  //public functions
  public void PlayControlAnimation() {
    if (ControlAnim != null)
      ControlAnim.Play();
  }

  public void Play() {
    OnEnable();
  }

}
