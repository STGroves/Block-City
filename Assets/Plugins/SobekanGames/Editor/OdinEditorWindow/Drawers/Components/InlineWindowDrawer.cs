using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using SobekanGames.OdinEditorWindow.Components;

namespace SobekanGames.OdinEditorWindow.Drawers.Components
{
  [DrawerPriority(0,0,3)]
  internal class InlineWindowDrawer<T> : PanelDrawer<T> where T : InlineWindow
  {
    protected InspectorProperty title;
    protected InspectorProperty titleStyle;

    Vector2 scrollPos;

    protected override void Initialize()
    {
      base.Initialize();

      title = Property.Children["_title"];
      titleStyle = Property.Children["_titleStyle"];
    }

    protected override void Draw()
    {
      Rect r = (Rect) rect.ValueEntry.WeakSmartValue;
      r.position = Vector2.zero;

      SirenixEditorGUI.DrawBorders(r,Element.MARGIN);

      EditorGUILayout.BeginVertical();

      if (!string.IsNullOrEmpty((string)title.ValueEntry.WeakSmartValue))
        DrawTitlebar();

      scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

      r.size -= Vector2.one * Element.MARGIN * 2;


      GUILayout.BeginArea(r);
      
      EditorGUILayout.BeginVertical();

      DrawContent();

      EditorGUILayout.EndVertical();

      GUILayout.EndArea();

      EditorGUILayout.EndVertical();

      EditorGUILayout.EndScrollView();
    }

    protected virtual void DrawTitlebar()
    {
      GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
      GUILayout.Label((string)title.ValueEntry.WeakSmartValue, (GUIStyle)titleStyle.ValueEntry.WeakSmartValue);
      if (GUILayout.Button("X", GUILayout.Width(25)))
        Property.ValueEntry.WeakSmartValue = null;
      GUILayout.EndHorizontal();
      SirenixEditorGUI.DrawThickHorizontalSeparator();
    }

    protected override void DrawContent()
    {
      for (int i = 0; i < Property.Children.Count; i++)
        Property.Children[i].Draw();
    }
  }
}