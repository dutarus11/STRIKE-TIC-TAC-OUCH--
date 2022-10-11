// (c) Copyright 2015 Luke Light&Magic. All rights reserved.

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EffectController))]
public class EffectControllerEditor : Editor {
  private SerializedObject obj;
  private GUIStyle commentStyle;

  private SerializedProperty totalTimeProperty, playOnWakeProperty, destoryOnEndProperty, EffectListProperty,
                             EffectPlayDelayTimeListProperty, EffectStopTimeListProperty, ControlAnimProperty,
                             AnimatingEndToPlayOrStopProperty, UseExistedAnimationProperty, StopToRewindProperty;
  private int inputCount, setCount;

  void Awake() {
    obj = new SerializedObject(target);
    totalTimeProperty = obj.FindProperty("TotalPlayTime");
    playOnWakeProperty = obj.FindProperty("PlayOnAwake");
    destoryOnEndProperty = obj.FindProperty("DestoryOnEnd");
    EffectListProperty = obj.FindProperty("EffectList");
    EffectPlayDelayTimeListProperty = obj.FindProperty("EffectPlayDelayTimeList");
    EffectStopTimeListProperty = obj.FindProperty("EffectStopTimeList");
    ControlAnimProperty = obj.FindProperty("ControlAnim");
    AnimatingEndToPlayOrStopProperty = obj.FindProperty("AnimatingEndToPlayOrStop");
    UseExistedAnimationProperty = obj.FindProperty("UseExistedAnimation");
    StopToRewindProperty = obj.FindProperty("StopToRewind");
    inputCount = setCount = EffectListProperty.arraySize;
  }

  public override void OnInspectorGUI() {
    if (commentStyle == null) {
      commentStyle = new GUIStyle(GUI.skin.GetStyle("Box"));
      commentStyle.font = EditorStyles.miniFont;
      commentStyle.alignment = TextAnchor.UpperLeft;
    }

    if (obj == null) Awake();

    if (obj.targetObject == target)
      obj.Update();

    GUILayout.Label("EffectController Settings", EditorStyles.boldLabel);
    EditorGUILayout.Space();
    EditorGUILayout.PropertyField(totalTimeProperty, new GUIContent("Total Playing Time"));
    EditorGUILayout.PropertyField(playOnWakeProperty, new GUIContent("Play On Awake"));
    EditorGUILayout.PropertyField(destoryOnEndProperty, new GUIContent("Destory On End"));

    EditorGUILayout.PropertyField(UseExistedAnimationProperty, new GUIContent("Use Animation Control"));
    if (UseExistedAnimationProperty.boolValue) {
      EditorGUILayout.PropertyField(ControlAnimProperty, new GUIContent("  Set Animation"));
      GUILayout.BeginHorizontal();
      GUILayout.Label("  Animating End To Play (True), or Stop (False)");
      EditorGUILayout.PropertyField(AnimatingEndToPlayOrStopProperty, GUIContent.none);
      GUILayout.EndHorizontal();
      EditorGUILayout.PropertyField(StopToRewindProperty, new GUIContent("Stopped To Rewind"));
    }

    EditorGUILayout.Space();
    EditorGUILayout.Space();

    GUI.SetNextControlName("Count");
    GUILayout.BeginHorizontal();
    inputCount = EditorGUILayout.IntField("Effects Count", EffectListProperty.arraySize);
    if (GUI.GetNameOfFocusedControl() == "Count") {
      if (GUI.changed) setCount = inputCount;
      GUILayout.Label(new GUIContent(" " + inputCount + "  ==>  " + setCount));
      Event e = Event.current;
      if (EffectListProperty.arraySize != setCount && e.keyCode == KeyCode.Return) {
        EffectStopTimeListProperty.arraySize = EffectPlayDelayTimeListProperty.arraySize = EffectListProperty.arraySize = setCount;
        obj.ApplyModifiedProperties();
        //GUI.FocusControl("");
      }
    }
    else
      setCount = inputCount;
    GUILayout.EndHorizontal();

    EffectController effectContrl = (EffectController)target;
    for (var i = 0; i < EffectListProperty.arraySize; i++) {
      EditorGUILayout.PropertyField(EffectListProperty.GetArrayElementAtIndex(i), new GUIContent("  Effect  " + (i + 1).ToString() + "   ==>"));
      GUILayout.BeginHorizontal();

      //Check input values
      if (effectContrl.EffectList[i] == effectContrl.transform) effectContrl.EffectList[i] = null;
      if (i < effectContrl.EffectPlayDelayTimeList.Length) {
        float startDelayTime = effectContrl.EffectPlayDelayTimeList[i];
        if (startDelayTime < 0) effectContrl.EffectPlayDelayTimeList[i] = 0;
        else if (startDelayTime > effectContrl.TotalPlayTime) effectContrl.EffectPlayDelayTimeList[i] = effectContrl.TotalPlayTime;
      }
      if (i < effectContrl.EffectStopTimeList.Length) {
        float stopTime = effectContrl.EffectStopTimeList[i];
        if (stopTime < 0) effectContrl.EffectStopTimeList[i] = 0;
        else if (effectContrl.EffectPlayDelayTimeList[i] > stopTime && stopTime != 0)
          effectContrl.EffectStopTimeList[i] = effectContrl.EffectPlayDelayTimeList[i];
        else if (stopTime > effectContrl.TotalPlayTime) effectContrl.EffectStopTimeList[i] = effectContrl.TotalPlayTime;
      }

#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2
      EditorGUILayout.PropertyField(EffectPlayDelayTimeListProperty.GetArrayElementAtIndex(i), new GUIContent("      Start Delay Time"), GUILayout.MaxWidth(200));
      EditorGUIUtility.LookLikeControls(90);
#else
      EditorGUILayout.PropertyField(EffectPlayDelayTimeListProperty.GetArrayElementAtIndex(i), new GUIContent("      Start Delay Time"));
      EditorGUIUtility.LookLikeControls(90, EditorGUIUtility.fieldWidth);
#endif
      EditorGUILayout.PrefixLabel("     Stop Time");
      EditorGUIUtility.LookLikeControls();
      EditorGUILayout.PropertyField(EffectStopTimeListProperty.GetArrayElementAtIndex(i), GUIContent.none, GUILayout.MaxWidth(50));
      if (i < effectContrl.EffectStopTimeList.Length)
        if (effectContrl.EffectStopTimeList[i] == 0) EditorGUILayout.PrefixLabel("= 0 don't use");
      GUILayout.EndHorizontal();
      EditorGUILayout.Space();
      EditorGUILayout.Space();
    }


    if (GUI.changed && GUI.GetNameOfFocusedControl() != "Count") {
      obj.ApplyModifiedProperties();
      //Debug.Log(inputCount);
    }

    //DrawDefaultInspector();
  }
}
