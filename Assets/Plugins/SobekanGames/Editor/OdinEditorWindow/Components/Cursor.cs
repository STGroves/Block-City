using System.Collections.Generic;
using UnityEngine;

namespace SobekanGames.OdinEditorWindow.Components
{
  public static class Cursor
  {
    public static Vector2 Position => Event.current.mousePosition;

    static List<Element> multiSelectList = new List<Element>();
    static Element currentSelection;

    public static void SetSelection(Element element)
    {
      multiSelectList.Clear();
      currentSelection = element;
    }

    public static void AddToSelection(Element element)
    {
      if (multiSelectList.Contains(element))
        return;

      multiSelectList.Add(element);
    }

    public static void RemoveFromSelection(Element element)
    {
      if (!multiSelectList.Contains(element))
        return;

      multiSelectList.Remove(element);
    }

    public static void SetSelection(Element[] elements)
    {
      multiSelectList.Clear();
      multiSelectList.AddRange(elements);
      currentSelection = null;
    }

    public static Element GetCurrentSelection()
    {
      return currentSelection;
    }

    public static Element[] GetCurrentMultiSelection()
    {
      return multiSelectList.ToArray();
    }
  }
}
