// (c) Copyright 2013-2015 Luke Light&Magic. All rights reserved.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectLightsFade : MonoBehaviour {
  public float totalTime;
  public float fadeInTime;
  public float fadeOutTime;
  public bool DisableOnEnd = true, Looping;

  private Light dynamicLight;

  void Awake() {
    dynamicLight = GetComponent<Light>();
    if (dynamicLight != null) {
      float intensity = dynamicLight.intensity;
      Animation animLight = GetComponent<Animation>();
      if (animLight == null)
        animLight = gameObject.AddComponent<Animation>();
      else
        return;
      if (fadeInTime < 0) fadeInTime = 0;
      if (fadeInTime > totalTime) fadeInTime = totalTime;
      if (fadeOutTime < 0) fadeOutTime = 0;
      if (fadeOutTime > totalTime) fadeOutTime = totalTime;
      if (fadeInTime + fadeOutTime > totalTime) fadeInTime = 0;

      List<Keyframe> keyList = new List<Keyframe>();
      if (fadeInTime != 0) {
        AnimationCurve linearCurve = AnimationCurve.Linear(0, 0, fadeInTime, intensity);
        keyList.Add(linearCurve[0]);
        keyList.Add(linearCurve[1]);
      }
      if (fadeOutTime != 0) {
        AnimationCurve linearCurve = AnimationCurve.Linear(totalTime - fadeOutTime, intensity, totalTime, 0);
        if (fadeInTime == 0 || fadeInTime != totalTime - fadeOutTime)
          keyList.Add(linearCurve[0]);
        else {
          Keyframe key = keyList[1];
          key.outTangent = linearCurve[0].outTangent;
          keyList[1] = key;
        }
        keyList.Add(linearCurve[1]);
      }
      AnimationCurve curve = new AnimationCurve(keyList.ToArray());
      AnimationClip clip = new AnimationClip();
#if UNITY_5
      clip.legacy = true;
#endif
      clip.SetCurve("", typeof(Light), "m_Intensity", curve);
      if (Looping) clip.wrapMode = WrapMode.Loop;
      clip.name = "light_fade";
      animLight.AddClip(clip, clip.name);
      animLight.clip = clip;
      animLight.Play();
    }

	}

  void Update() {
    if (!DisableOnEnd || Looping) return;
    Animation anim = GetComponent<Animation>();
    if (anim)
      if (!anim.isPlaying)
        if (dynamicLight)
          dynamicLight.enabled = false;
  }

  void OnEnable() {
    if (dynamicLight)
      dynamicLight.enabled = true;
  }


  void Reset() {
    totalTime = 1;
    fadeInTime = 0;
    fadeOutTime = 1;
    DisableOnEnd = true;
  }
}
