// (c) Copyright 2015 Luke Light&Magic. All rights reserved.

using UnityEngine;
using UnityEditor;

public class EasyHotKeysEditorTool : ScriptableObject {

  //Add New Child Hot key = "Shit + Alt + N"

  [MenuItem("GameObject/Selection/Add New Child #&N")]
  static void CreateLocalGameObject() {
    //if (PrefabCheck()) {

    // Create our new GameObject
    GameObject newGameObject = new GameObject();
    newGameObject.name = "GameObject";

    // Make this action undoable
    UnityEditor.Undo.RegisterCreatedObjectUndo(newGameObject, "Add New Child " + newGameObject.name);

    // If there is a selected object in the scene then make the new object its child.
    if (Selection.activeTransform != null) {
      newGameObject.transform.parent = Selection.activeTransform;
      newGameObject.name = "Child";

      // Place the new GameObject at the same position as the parent.
      newGameObject.transform.localPosition = Vector3.zero;
      newGameObject.transform.localRotation = Quaternion.identity;
      newGameObject.transform.localScale = Vector3.one;
      newGameObject.layer = Selection.activeGameObject.layer;
    }

    // Select our newly created GameObject
    Selection.activeGameObject = newGameObject;

    //}
  }

  static bool PrefabCheck() {
    if (Selection.activeTransform != null) {
      // Check if the selected object is a prefab instance and display a warning
      PrefabType type = PrefabUtility.GetPrefabType(Selection.activeGameObject);

      if (type == PrefabType.PrefabInstance) {
        return EditorUtility.DisplayDialog("Losing prefab",
          "This action will lose the prefab connection. Are you sure you wish to continue?",
          "Continue", "Cancel");
      }
    }
    return true;
  }

  //Add New Child Hot key = "Shit + Alt + N"

  [MenuItem("GameObject/Reset Transform #&T")]
  static void ResetTransform() {
    if (Selection.activeTransform != null) {
      Transform obj = Selection.activeTransform;
#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2
      UnityEditor.Undo.RegisterUndo(obj, "/Reset Transform " + obj.name);
#else
      UnityEditor.Undo.RecordObject(obj, "/Reset Transform " + obj.name);
#endif
      obj.localPosition = Vector3.zero;
      obj.localRotation = Quaternion.identity;
      obj.localScale = Vector3.one;
    }
  }
}
