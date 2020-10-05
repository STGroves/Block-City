//using Sirenix.OdinInspector;
//using Sirenix.OdinInspector.Editor;
//using Sirenix.Serialization;
//using UnityEditor;
//using UnityEngine;

//public class OdinConnectionEditorWindow : OdinEditorWindow
//{
//  [OdinSerialize, HideReferenceObjectPicker] OdinConnectionEditorPropertyWindow propWin;

//  bool top;
//  bool left;
//  bool right;
//  bool bottom;

//  Node topNode;
//  Node leftNode;
//  Node rightNode;
//  Node bottomNode;

//  const int CENTER_SIZE = 200;
//  const float CENTER_BUTTON_PADDING = 25f;
//  const float CENTER_NODE_PADDING = 5f;

//  static string dependencyGUID;

//  [MenuItem("Window/Project/Odin Connection Editor Window")]
//  static void Init()
//  {
//    // Get existing open window or if none, make a new one
//    GetWindow(typeof(OdinConnectionEditorWindow)).Show();
//  }

//  protected override void OnEnable()
//  {
//    minSize = Vector2.one * 50;

//    if (string.IsNullOrEmpty(dependencyGUID))
//      dependencyGUID = GUID.Generate().ToString();

//    DependencyObject.EnforceDependencyInstance(dependencyGUID, true);

//    propWin = new OdinConnectionEditorPropertyWindow(new Rect(0, 0, 300, 500), "Connections Properties");
//  }
//}