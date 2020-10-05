using Sirenix.Serialization;
using SobekanGames.OdinEditorWindow.Enums;
using UnityEngine;

namespace SobekanGames.OdinEditorWindow.Components
{
  public class DockableWindow : InlineWindow
  {
    [OdinSerialize] public bool Docked { get; protected set; }

    public DockableWindow(Rect rect, ResizeType resizeType = ResizeType.All, bool canMove = false) : base(rect, resizeType, canMove)
    {
    }

    public DockableWindow(Rect rect, string title, ResizeType resizeType = ResizeType.All, bool canMove = false) : base(rect, title, resizeType, canMove)
    {
    }
  }
}
