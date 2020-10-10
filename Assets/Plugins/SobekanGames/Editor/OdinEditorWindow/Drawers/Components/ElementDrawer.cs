using Sirenix.OdinInspector.Editor;
using SobekanGames.OdinEditorWindow.Components;
using SobekanGames.OdinEditorWindow.Enums;
using UnityEditor;
using UnityEngine;

namespace SobekanGames.OdinEditorWindow.Drawers.Components
{
  [DrawerPriority(0,0,0)]
  internal abstract class ElementDrawer<T> : OdinValueDrawer<T> where T : Element
  {
    protected InspectorProperty rect;
    protected InspectorProperty resizeType;
    protected InspectorProperty resizeAreas;


    protected override void Initialize()
    {
      base.Initialize();

      rect = Property.Children["_elementRect"];

      resizeType = Property.Children["_resizeType"];
      resizeAreas = Property.Children["_resizeAreas"];
    }

    void AddResizeAreaCursors()
    {
      MouseCursor[] mouseCursors =
      {
        MouseCursor.ResizeUpLeft,
        MouseCursor.ResizeVertical,
        MouseCursor.ResizeUpRight,
        MouseCursor.ResizeHorizontal,
        MouseCursor.ResizeHorizontal,
        MouseCursor.ResizeUpRight,
        MouseCursor.ResizeVertical,
        MouseCursor.ResizeUpLeft
      };


      for (int i = 0; i < ((Rect[]) resizeAreas.ValueEntry.WeakSmartValue).Length; i++)
      {
        if (!((ResizeType) resizeType.ValueEntry.WeakSmartValue).HasFlag((ResizeType) Mathf.Pow(2, i)))
          continue;

        Rect r = ((Rect[]) resizeAreas.ValueEntry.WeakSmartValue)[i];
        r.position -= ((Rect)rect.ValueEntry.WeakSmartValue).position;
        EditorGUIUtility.AddCursorRect(r, mouseCursors[i]);
      }
    }

    protected virtual void AssignArea()
    {
      GUILayout.BeginArea((Rect)rect.ValueEntry.WeakSmartValue);
    }

    protected override void DrawPropertyLayout(GUIContent label)
    {
      AssignArea();

      if ((ResizeType)resizeType.ValueEntry.WeakSmartValue != ResizeType.None)
        AddResizeAreaCursors();

      Draw();

      GUILayout.EndArea();

      if (GUI.changed)
        ((OdinEditorWindow)Property.SerializationRoot.ValueEntry.WeakSmartValue).Repaint();
    }

    protected abstract void Draw();
  }
}