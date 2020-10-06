using Sirenix.Serialization;
using SobekanGames.OdinEditorWindow;
using UnityEditor;
using SobekanGames.OdinEditorWindow.Components;
using UnityEngine;

public class TestWindow : OdinEditorWindow
{
  [OdinSerialize] InlineWindow _propertyWindow;

  [MenuItem("Window/Project/Test Window")]
  static void Init()
  {
    // Get existing open window or if none, make a new one
    GetWindow(typeof(TestWindow)).Show();
  }

  protected override void OnEnable()
  {
    base.OnEnable();

    _propertyWindow = new TestInlineWindow(new Rect(0, 0, 200, 300), "Test", canMove: true);
  }
}