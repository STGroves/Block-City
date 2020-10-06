using Sirenix.OdinInspector.Editor;
using UnityEngine;
using SobekanGames.OdinEditorWindow.Components;

namespace SobekanGames.OdinEditorWindow.Drawers.Components
{
  [DrawerPriority(0,0,1)]
  internal class PanelDrawer<T> : ElementDrawer<T> where T : Panel
  {
    protected InspectorProperty backgroundStyle;

    protected override void Initialize()
    {
      base.Initialize();
      backgroundStyle = Property.Children["_backgroundStyle"];
    }

    protected override void AssignArea()
    {
      GUILayout.BeginArea((Rect)rect.ValueEntry.WeakSmartValue, (GUIStyle)backgroundStyle.ValueEntry.WeakSmartValue);
    }

    protected override void Draw()
    {
      DrawContent();
    }

    protected virtual void DrawContent() { }
  }
}