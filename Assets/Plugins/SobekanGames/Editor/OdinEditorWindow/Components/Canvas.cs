using System.Collections.Generic;
using Sirenix.Serialization;
using SobekanGames.OdinEditorWindow.Enums;
using UnityEngine;

namespace SobekanGames.OdinEditorWindow.Components
{
  public class Canvas : Panel
  {
    [OdinSerialize] public List<Element> Children = new List<Element>();

    public Canvas(Rect canvasRect, bool canMove = false, ResizeType resizeType = ResizeType.None) : base(canvasRect, canMove, resizeType) { }

    
  }
}
