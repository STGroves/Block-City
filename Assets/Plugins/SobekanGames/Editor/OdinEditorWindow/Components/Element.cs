using Sirenix.Serialization;
using SobekanGames.OdinEditorWindow.Enums;
using UnityEngine;

namespace SobekanGames.OdinEditorWindow.Components
{
  public class Element
  {
    public delegate void MouseClickEvent(int button, Element element);
    public delegate void MouseDragEvent(int button, Vector2 delta, Element element);

    [OdinSerialize] protected Rect _elementRect;
    [OdinSerialize] protected int _z;

    [OdinSerialize] protected bool _moveable;
    [OdinSerialize] protected ResizeType _resizeType;

    [OdinSerialize] protected Vector2 _minSize;
    [OdinSerialize] protected Vector2 _maxSize;

    protected internal bool beingDragged;
    protected internal bool beingResized;

    [OdinSerialize] Rect[] _resizeAreas;
    ResizeType resizeAreaSelected;

    public int ZLayer => _z;

    MouseClickEvent OnMouseDown;
    MouseClickEvent OnMouseUp;
    MouseDragEvent OnMouseDrag;

    public const int MARGIN = 2;

    public Element(Rect elementRect, bool canMove, ResizeType resizeType)
    {
      _elementRect = elementRect;
      _moveable = canMove;
      _resizeType = resizeType;

      _resizeAreas = new Rect[8];

      UpdateResizeAreas();
    }

    public Rect GetRect()
    {
      return _elementRect;
    }

    public void SetZ(int zLayer)
    {
      _z = zLayer;
    }

    void UpdateResizeAreas()
    {
      Vector2 size = _elementRect.size;
      Vector2 pos = _elementRect.position;

      _resizeAreas[0] = new Rect(pos, Vector2.one * MARGIN);
      _resizeAreas[1] = new Rect(pos.x + MARGIN, pos.y, size.x - MARGIN * 2, MARGIN);
      _resizeAreas[2] = new Rect(pos + Vector2.right * (size.x - MARGIN), Vector2.one * MARGIN);
      _resizeAreas[3] = new Rect(pos.x, pos.y + MARGIN, MARGIN, size.y - MARGIN * 2);
      _resizeAreas[4] = new Rect(pos.x + (size.x - MARGIN), pos.y + MARGIN, MARGIN, size.y - MARGIN * 2);
      _resizeAreas[5] = new Rect(pos.x, pos.y + (size.y - MARGIN), MARGIN, MARGIN);
      _resizeAreas[6] = new Rect(pos.x + MARGIN, pos.y + (size.y - MARGIN), size.x - MARGIN * 2, MARGIN);
      _resizeAreas[7] = new Rect(pos + size - Vector2.one * MARGIN, Vector2.one * MARGIN);
    }

    protected internal virtual bool ProcessEvents(Event e)
    {
      Vector2 size = _elementRect.size;

      //Rect rectCheck = new Rect(Vector2.zero, size);

      bool useEvent = false;

      switch (e.type)
      {
        case EventType.MouseDown:
          ProcessMouseDown(e.button, ref useEvent);

          OnMouseDown.Invoke(e.button, this);

          if (useEvent)
            e.Use();

          break;

        case EventType.MouseUp:
          ProcessMouseUp(e.button, ref useEvent);

          OnMouseUp.Invoke(e.button, this);

          if (useEvent)
            e.Use();

          break;

        case EventType.MouseDrag:
          ProcessMouseDrag(e.button, e.delta, ref useEvent);

          OnMouseDrag.Invoke(e.button, e.delta, this);

          if (useEvent)
            e.Use();

          break;
      }

      return false;
    }

    protected virtual void ProcessMouseDown(int button, ref bool useEvent)
    {
      if (button != 0)
        return;

      if (_resizeType != ResizeType.None && CheckResizeAreas(Cursor.Position))
      {
        beingResized = true;
        GUI.changed = true;
        useEvent = true;
      }

      else if (_elementRect.Contains(Cursor.Position) && _moveable)
      {
        beingDragged = true;
        GUI.changed = true;
        useEvent = true;
      }

      else
        GUI.changed = true;
    }

    protected virtual void ProcessMouseUp(int button, ref bool useEvent)
    {
      beingDragged = false;
      beingResized = false;
    }
    
    protected virtual void ProcessMouseDrag(int button, Vector2 delta, ref bool useEvent)
    {
      if (button != 0)
        return;
      
      if (beingDragged)
      {
        Drag(delta);
        UpdateResizeAreas();
        useEvent = true;

        GUI.changed = true;
      }

      else if (beingResized)
      {
        Resize(delta);
        UpdateResizeAreas();
        useEvent = true;

        GUI.changed = true;
      }
    }

    bool CheckResizeAreas(Vector2 mousePosition)
    {
      for (int i = 0; i < _resizeAreas.Length; i++)
      {
        if (!_resizeAreas[i].Contains(mousePosition) || !_resizeType.HasFlag((ResizeType)Mathf.Pow(2, i)))
          continue;

        resizeAreaSelected = (ResizeType)Mathf.Pow(2, i);
        return true;
      }

      return false;
    }

    void Resize(Vector2 delta)
    {
      float tempWidth = _elementRect.size.x - _minSize.x;
      float tempHeight = _elementRect.size.y - _minSize.y;

      switch (resizeAreaSelected)
      {
        case ResizeType.TopLeft:
          _elementRect.xMin += tempWidth - delta.x > 0 ? delta.x : tempWidth;
          _elementRect.yMin += tempHeight - delta.y > 0 ? delta.y : tempHeight;
          break;

        case ResizeType.Top:
          _elementRect.yMin += tempHeight - delta.y > 0 ? delta.y : tempHeight;
          break;

        case ResizeType.TopRight:
          _elementRect.xMax += tempWidth + delta.x > 0 ? delta.x : tempWidth;
          _elementRect.yMin += tempHeight - delta.y > 0 ? delta.y : tempHeight;
          break;

        case ResizeType.Left:
          _elementRect.xMin += tempWidth - delta.x > 0 ? delta.x : tempWidth;
          break;

        case ResizeType.Right:
          _elementRect.xMax += tempWidth + delta.x > 0 ? delta.x : tempWidth;
          break;

        case ResizeType.BottomLeft:
          _elementRect.xMin += tempWidth - delta.x > 0 ? delta.x : tempWidth;
          _elementRect.yMax += tempHeight + delta.y > 0 ? delta.y : tempHeight;
          break;

        case ResizeType.Bottom:
          _elementRect.yMax += tempHeight + delta.y > 0 ? delta.y : tempHeight;
          break;

        case ResizeType.BottomRight:
          _elementRect.xMax += tempWidth + delta.x > 0 ? delta.x : tempWidth;
          _elementRect.yMax += tempHeight + delta.y > 0 ? delta.y : tempHeight;
          break;
      }

      _elementRect.width = Mathf.Max(_elementRect.width, _minSize.x);
      _elementRect.height = Mathf.Max(_elementRect.height, _minSize.y);
    }

    protected void Drag(Vector2 delta)
    {
      _elementRect.position += delta;
    }
  }
}
