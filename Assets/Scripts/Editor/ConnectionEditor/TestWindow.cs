using Sirenix.Serialization;
using SobekanGames.OdinEditorWindow;
using UnityEditor;
using SobekanGames.OdinEditorWindow.Components;
using UnityEngine;

public class TestWindow : OdinEditorWindow
{
  [OdinSerialize] TestInlineWindow _propertyWindow;
  [OdinSerialize] TestInlineWindow _propertyWindow2;

  [MenuItem("Window/Project/Test Window")]
  static void Init()
  {
    // Get existing open window or if none, make a new one
    GetWindow(typeof(TestWindow)).Show();
  }

  protected override void OnEnable()
  {
    base.OnEnable();

    _propertyWindow = new TestInlineWindow(new Rect(0, 0, 200, 300), "Test1", canMove: true);
    _propertyWindow2 = new TestInlineWindow(new Rect(300, 0, 200, 300), "Test2", canMove: true);

    _propertyWindow.SetZ(2);

    canvas.Children.Add(_propertyWindow);
    canvas.Children.Add(_propertyWindow2);
  }
}