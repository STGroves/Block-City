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


    protected InspectorProperty moveable;
    protected InspectorProperty resizeType;

    protected InspectorProperty minSize;
    protected InspectorProperty maxSize;

    protected bool beingDragged;
    protected bool beingResized;

    Rect[] resizeAreas;
    ResizeType resizeAreaSelected;

    protected override void Initialize()
    {
      base.Initialize();

      rect = Property.Children["_elementRect"];

      moveable = Property.Children["_moveable"];
      resizeType = Property.Children["_resizeType"];
      minSize = Property.Children["_minSize"];
      maxSize = Property.Children["_maxSize"];

      resizeAreas = new Rect[8];

      UpdateResizeAreas();
    }

    void UpdateResizeAreas()
    {
      if (rect == null)
        return;

      Vector2 size = ((Rect) rect.ValueEntry.WeakSmartValue).size;

      resizeAreas[0] = new Rect(Vector2.zero, Vector2.one * Element.MARGIN);
      resizeAreas[1] = new Rect(Element.MARGIN, 0, size.x - (Element.MARGIN * 2), Element.MARGIN);
      resizeAreas[2] = new Rect(Vector2.right * (size.x - Element.MARGIN), Vector2.one * Element.MARGIN);
      resizeAreas[3] = new Rect(0, Element.MARGIN, Element.MARGIN, size.y - (Element.MARGIN * 2));
      resizeAreas[4] = new Rect(size.x - Element.MARGIN, Element.MARGIN, Element.MARGIN, size.y - (Element.MARGIN * 2));
      resizeAreas[5] = new Rect(0, size.y - Element.MARGIN, Element.MARGIN, Element.MARGIN);
      resizeAreas[6] = new Rect(Element.MARGIN, size.y - Element.MARGIN, size.x - (Element.MARGIN * 2), Element.MARGIN);
      resizeAreas[7] = new Rect(size - (Vector2.one * Element.MARGIN), Vector2.one * Element.MARGIN);
    }

    protected virtual bool ProcessEvents(Event e)
    {
      Vector2 size = ((Rect)rect.ValueEntry.WeakSmartValue).size;

      Rect rectCheck = new Rect(Vector2.zero, size);

      switch (e.type)
      {
        case EventType.MouseDown:
          if (e.button == 0)
          {
            if ((ResizeType) resizeType.ValueEntry.WeakSmartValue != ResizeType.None &&
                CheckResizeAreas(e.mousePosition))
            {
              beingResized = true;
              GUI.changed = true;
            }

            else if (rectCheck.Contains(e.mousePosition) && (bool) moveable.ValueEntry.WeakSmartValue)
            {
              beingDragged = true;
              GUI.changed = true;
            }

            else
              GUI.changed = true;
          }

          break;

        case EventType.MouseUp:
          beingDragged = false;
          beingResized = false;
          break;

        case EventType.MouseDrag:
          if (e.button == 0)
            if (beingDragged)
            {
              Drag(e.delta);
              e.Use();

              return true;
            }
            else if (beingResized)
            {
              Resize(e.delta);
              UpdateResizeAreas();
              e.Use();

              return true;
            }

          break;
      }

      return false;
    }

    bool CheckResizeAreas(Vector2 mousePosition)
    {
      for (int i = 0; i < resizeAreas.Length; i++)
      {
        if (!resizeAreas[i].Contains(mousePosition) || !((ResizeType)resizeType.ValueEntry.WeakSmartValue).HasFlag((ResizeType)Mathf.Pow(2, i)))
          continue;

        resizeAreaSelected = (ResizeType)Mathf.Pow(2, i);
        return true;
      }

      return false;
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

      for (int i = 0; i < resizeAreas.Length; i++)
      {
        if (((ResizeType)resizeType.ValueEntry.WeakSmartValue).HasFlag((ResizeType)Mathf.Pow(2, i)))
          EditorGUIUtility.AddCursorRect(resizeAreas[i], mouseCursors[i]);
      }
    }

    void Resize(Vector2 delta)
    {
      Rect r = ((Rect)rect.ValueEntry.WeakSmartValue);

      Vector2 minSizeValues = (Vector2)minSize.ValueEntry.WeakSmartValue;

      float tempWidth = r.size.x - minSizeValues.x;
      float tempHeight = r.size.y - minSizeValues.y;

      switch (resizeAreaSelected)
      {
        case ResizeType.TopLeft:
          r.xMin += tempWidth - delta.x > 0 ? delta.x : tempWidth;
          r.yMin += tempHeight - delta.y > 0 ? delta.y : tempHeight;
          break;

        case ResizeType.Top:
          r.yMin += tempHeight - delta.y > 0 ? delta.y : tempHeight;
          break;

        case ResizeType.TopRight:
          r.xMax += tempWidth + delta.x > 0 ? delta.x : tempWidth;
          r.yMin += tempHeight - delta.y > 0 ? delta.y : tempHeight;
          break;

        case ResizeType.Left:
          r.xMin += tempWidth - delta.x > 0 ? delta.x : tempWidth;
          break;

        case ResizeType.Right:
          r.xMax += tempWidth + delta.x > 0 ? delta.x : tempWidth;
          break;
        
        case ResizeType.BottomLeft:
          r.xMin += tempWidth - delta.x > 0 ? delta.x : tempWidth;
          r.yMax += tempHeight + delta.y > 0 ? delta.y : tempHeight;
          break;
        
        case ResizeType.Bottom:
          r.yMax += tempHeight + delta.y > 0 ? delta.y : tempHeight;
          break;
        
        case ResizeType.BottomRight:
          r.xMax += tempWidth + delta.x > 0 ? delta.x : tempWidth;
          r.yMax += tempHeight + delta.y > 0 ? delta.y : tempHeight;
          break;
      }

      r.width = Mathf.Max(r.width, minSizeValues.x);
      r.height = Mathf.Max(r.height, minSizeValues.y);

      rect.ValueEntry.WeakSmartValue = r;
    }

    protected void Drag(Vector2 delta)
    {
      Rect r = ((Rect)rect.ValueEntry.WeakSmartValue);
      r.position += delta;
      rect.ValueEntry.WeakSmartValue = r;
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
      ProcessEvents(Event.current);

      GUILayout.EndArea();

      if (GUI.changed)
        ((OdinEditorWindow)Property.SerializationRoot.ValueEntry.WeakSmartValue).Repaint();
    }

    protected abstract void Draw();
  }
}