using System.Collections.Generic;
using System.Linq;
using Sirenix.Serialization;
using SobekanGames.OdinEditorWindow.Enums;
using UnityEngine;

namespace SobekanGames.OdinEditorWindow.Components
{
  public class Canvas : Panel
  {
    [OdinSerialize] public List<Element> Children = new List<Element>();

    public Canvas(Rect canvasRect, bool canMove = false, ResizeType resizeType = ResizeType.None) : base(canvasRect, canMove, resizeType) { }

    protected internal override bool ProcessEvents(Event e)
    {
      Children.RemoveAll(x => x == null);

      List<Element>childElements = Children.OrderBy((x) => x.ZLayer).ToList();

      for (int i = 0; i < childElements.Count; i++)
        Children[i].ProcessEvents(e);

      base.ProcessEvents(e);

      return false;
    }
  }
}
