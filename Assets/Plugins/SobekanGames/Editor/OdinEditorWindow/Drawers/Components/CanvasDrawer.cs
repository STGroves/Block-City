using Sirenix.OdinInspector.Editor;
using SobekanGames.OdinEditorWindow.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Canvas = SobekanGames.OdinEditorWindow.Components.Canvas;

namespace SobekanGames.OdinEditorWindow.Drawers.Components
{
  [DrawerPriority(0,0,2)]
  internal class CanvasDrawer<T> : PanelDrawer<T> where T : Canvas
  {
    protected InspectorProperty children;

    protected override void Initialize()
    {
      base.Initialize();
      children = Property.Children["Children"];
    }

    protected override void Draw()
    {
      DrawContent();
    }

    protected override void DrawContent()
    {
      for (int i = 0; i < Property.Children.Count; i++)
      {
        if (Property.Children[i].Name != "Children")
          Property.Children[i].Draw();

        else
        {
          UpdateChildren();
          DrawChildren();
        }
      }
    }

    void UpdateChildren()
    {
      List<Element> childElements = (List<Element>)children.ValueEntry.WeakSmartValue;

      for (int i = 0; i < children.Children.Count; i++)
      {
        if (children.Children[i].ValueEntry.WeakSmartValue == null)
          ((List<Element>) children.ValueEntry.WeakSmartValue).RemoveAt(i);
      }

      childElements = childElements.OrderByDescending((x) => x.ZLayer).ToList();
      children.ValueEntry.WeakSmartValue = childElements;
    }

    void DrawChildren()
    {
      GUI.depth = 0; 

      for (int i = 0; i < children.Children.Count; i++)
      {
        GUI.depth = (int) children.Children[i].Children["_z"].ValueEntry.WeakSmartValue;
        children.Children[i].Draw();
      }
    }
  }
}