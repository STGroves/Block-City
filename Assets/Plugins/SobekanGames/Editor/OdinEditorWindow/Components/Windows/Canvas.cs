using SobekanGames.OdinEditorWindow.Enums;
using UnityEngine;

namespace SobekanGames.OdinEditorWindow.Components.Windows
{
  public class Canvas : Panel
  {
    public Canvas(Rect canvasRect, bool canMove = false, ResizeType resizeType = ResizeType.None) : base(canvasRect, canMove, resizeType) { }


  }
}
