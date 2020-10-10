using Sirenix.OdinInspector.Editor;
using System.Linq;
using SobekanGames.OdinEditorWindow.Components;

namespace SobekanGames.OdinEditorWindow.Drawers.Components
{
  [DrawerPriority(0,0,2)]
  internal class CanvasDrawer<T> : PanelDrawer<T> where T : Canvas
  {
    protected InspectorProperty children;
    protected InspectorProperty[] childElements;

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
      int childCount = children.Children.Count;

      for (int i = 0; i < childCount; i++)
      {
        if (children.Children[i].ValueEntry.WeakSmartValue != null)
          continue;

        children.Children[i].Dispose();
      }

      childElements = children.Children.OrderBy((x) => (int) x.Children["_z"].ValueEntry.WeakSmartValue).ToArray();
    }

    void DrawChildren()
    {
      for (int i = 0; i < childElements.Length; i++)
        childElements[i].Draw();
    }
  }
}