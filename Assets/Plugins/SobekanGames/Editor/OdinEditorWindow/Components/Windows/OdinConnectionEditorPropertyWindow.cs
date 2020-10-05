using Sirenix.Serialization;
using SobekanGames.OdinEditorWindow.Components;
using UnityEngine;

public class OdinConnectionEditorPropertyWindow : SobekanGames.OdinEditorWindow.Components.Canvas
{
  [OdinSerialize] Vector2Int minMax = Vector2Int.one;
  //DependencyObject<int> incoming;
  //DependencyObject<int> outgoing;

  public OdinConnectionEditorPropertyWindow(Rect rect) : base(rect)
  {
  }

  //public OdinConnectionEditorPropertyWindow(Rect rect, string title) : base(rect, title) { }


  //protected override void DrawContent()
  //{
  //  DependencyObject<int> incoming = DependencyObject<int>.EnforceDependencyObject("incomingTest", new GUIContent("In"), 10);

  //  EditorGUILayout.BeginVertical("Box");
  //  EditorGUILayout.LabelField("Lanes");
  //  EditorGUILayout.BeginHorizontal();
  //  EditorGUIUtility.labelWidth = 50;
  //  minMax.x = Mathf.Min(Mathf.Max(EditorGUILayout.IntField("Min", minMax.x), 1), minMax.y);
  //  minMax.y = Mathf.Max(EditorGUILayout.IntField("Max", minMax.y), minMax.x);
  //  EditorGUILayout.EndHorizontal();
  //  EditorGUIUtility.labelWidth = 0;
  //  EditorGUILayout.EndVertical();
  //  SirenixEditorGUI.HorizontalLineSeparator();
  //  incoming.Draw();
  //}
}