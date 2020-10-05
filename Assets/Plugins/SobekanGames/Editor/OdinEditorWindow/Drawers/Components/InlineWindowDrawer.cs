using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using SobekanGames.OdinEditorWindow.Components;

namespace SobekanGames.OdinEditorWindow.Drawers.Components
{
  [DrawerPriority(0,0,2)]
  internal class InlineWindowDrawer<T> : CanvasDrawer<T> where T : InlineWindow
  {
    protected InspectorProperty title;
    protected InspectorProperty titleStyle;

    protected override void Initialize()
    {
      base.Initialize();

      title = Property.Children["_title"];
      titleStyle = Property.Children["_titleStyle"];
    }

    protected override void Draw()
    {
      SirenixEditorGUI.DrawBorders((Rect) rect.ValueEntry.WeakSmartValue,Element.MARGIN);

      EditorGUILayout.BeginVertical();

      if (!string.IsNullOrEmpty((string)title.ValueEntry.WeakSmartValue))
        DrawTitle();

      DrawContent();

      EditorGUILayout.EndVertical();
    }

    protected virtual void DrawTitle()
    {
      EditorGUILayout.LabelField((string)title.ValueEntry.WeakSmartValue, (GUIStyle)titleStyle.ValueEntry.WeakSmartValue);
      SirenixEditorGUI.DrawThickHorizontalSeparator();
    }
  }
}