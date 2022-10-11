// (c) Copyright 2015 Luke Light&Magic. All rights reserved.

using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class EffectLoopPlayback : MonoBehaviour {

  public Transform EffectTransRoot;
  public float TotalPlayingTime = 3;
  
  public bool StopLooping;

#if UNITY_EDITOR
  public float CurrentTime;
#endif

  private float t = 0;
  private bool stopUpdate;
  private bool ParticleSystemRootOnly;


  void Start() {
    ParticleSystem ps = GetComponent<ParticleSystem>();
    if (ps) {
      EffectTransRoot = transform;
      ParticleSystemRootOnly = true;
    }
  }

  void Update() {
    
    if (stopUpdate) {
      stopUpdate = StopLooping;
      return;
    }

    Transform trans = EffectTransRoot;
    if (TotalPlayingTime < 0) TotalPlayingTime = 0;

    if (StopLooping) {
      if (trans) {
        if (ParticleSystemRootOnly) {
          ParticleSystem ps = trans.GetComponent<ParticleSystem>();
          if (ps) ps.Stop();
        }
        else
          trans.gameObject.SetActive(false);
      }
      stopUpdate = true;
    }
    else {
      if (TotalPlayingTime > 0) {
        if (t > TotalPlayingTime) {
          t = 0;

          if (!trans) {
            if (ParticleSystemRootOnly) EffectTransRoot = transform;
          }
          else {
            if (ParticleSystemRootOnly) {
              ParticleSystem ps = trans.GetComponent<ParticleSystem>();
              if (ps) {
                ps.Stop();
                ps.Play();
              }
            }
            else {
              if (trans != transform) {
                trans.gameObject.SetActive(false);
                trans.gameObject.SetActive(true);
              }
              else
                Debug.LogWarning(gameObject.name + 
                " EfectLoopPlayback can not loop transform itself, select a child or external object please.", gameObject);
            }
          }
        }
        else {
          if (Application.isPlaying) {
            //In game mode or editor game mode
            t += Time.deltaTime;
          }
          else if (ParticleSystemRootOnly) {
            //Only running in editor mode and checked ParticleSystemRootOnly
            t += Time.fixedDeltaTime;
          }

#if UNITY_EDITOR
          CurrentTime = t;
#endif

        }
      }
    }

    
  }

}
