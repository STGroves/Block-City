using Sirenix.OdinInspector;
using Sirenix.Serialization;
using SobekanGames.OdinEditorWindow.Components;
using SobekanGames.OdinEditorWindow.Enums;
using UnityEngine;

public class TestInlineWindow : InlineWindow
{
  [OdinSerialize, TabGroup("A"), LabelWidth(50)]
  int testA = 0;
  [OdinSerialize, TabGroup("B"), LabelWidth(50)]
  int testB = 0;

  public TestInlineWindow(Rect rect, ResizeType resizeType = ResizeType.All, bool canMove = false) : base(rect, resizeType, canMove)
  {
  }

  public TestInlineWindow(Rect rect, string title, ResizeType resizeType = ResizeType.All, bool canMove = false) : base(rect, title, resizeType, canMove)
  {
  }
}