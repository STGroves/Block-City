using System;
using Sirenix.Serialization;
using SobekanGames.OdinEditorWindow.Components.Windows;
using SobekanGames.OdinEditorWindow.Enums;
using UnityEngine;

namespace SobekanGames.OdinEditorWindow.Components
{
  public class DockArea
  {
    [OdinSerialize] Rect[] _dockAreas = new Rect[9];
    [OdinSerialize] DockableLocations _dockType;
    [OdinSerialize] DockableLocations _overlayType;
    DockableLocations dockableAreaSelected;

    float triggerZone;
    bool isPercentage;

    public DockArea(DockableLocations dockableAreas, DockableLocations overlayAreas, float triggerZoneValue, bool useAsPercentage)
    {
      _dockType = dockableAreas;
      _overlayType = overlayAreas;

      isPercentage = useAsPercentage;

      if (isPercentage && triggerZoneValue > 0.5f || triggerZone <= 0)
        throw new ArgumentOutOfRangeException();

      triggerZone = triggerZoneValue;
    }

    void UpdateDockableAreas(Rect elementRect)
    {
      Vector2 size = elementRect.size;
      Vector2 pos = elementRect.position;

      float xTriggerSize = isPercentage ? size.x * triggerZone : triggerZone;
      float yTriggerSize = isPercentage ? size.y * triggerZone : triggerZone;

      _dockAreas[0] = new Rect(pos.x, pos.y, xTriggerSize, yTriggerSize);
      _dockAreas[1] = new Rect(pos.x + xTriggerSize, pos.y, size.x - xTriggerSize * 2, yTriggerSize);
      _dockAreas[2] = new Rect(pos.x + size.x - xTriggerSize, pos.y, xTriggerSize, yTriggerSize);
      _dockAreas[3] = new Rect(pos.x, pos.y + yTriggerSize, xTriggerSize, size.y - yTriggerSize * 2);
      _dockAreas[4] = new Rect(pos.x + xTriggerSize, pos.y + yTriggerSize, size.x - xTriggerSize * 2,
        size.y - yTriggerSize * 2);
      _dockAreas[5] = new Rect(pos.x + (size.x - xTriggerSize), pos.y + yTriggerSize, xTriggerSize,
        size.y - yTriggerSize * 2);
      _dockAreas[6] = new Rect(pos.x, pos.y + (size.y - yTriggerSize), xTriggerSize, yTriggerSize);
      _dockAreas[7] = new Rect(pos.x + xTriggerSize, pos.y + (size.y - yTriggerSize), size.x - xTriggerSize * 2,
        yTriggerSize);
      _dockAreas[8] = new Rect(pos.x + size.x - xTriggerSize, pos.y + size.y - yTriggerSize, xTriggerSize,
        yTriggerSize);
    }

    protected internal bool ProcessEvents(Event e)
    {
      InlineWindow window = Cursor.GetCurrentSelection() as InlineWindow;

      switch (e.type)
      {
        case EventType.MouseUp:
          if (CheckDockValidity(window))
            Dock(window);
          break;
      }

      return false;
    }

    protected bool CheckDockValidity(InlineWindow window)
    {
      if (window == null || !window.beingDragged)
        return false;

      for (int i = 0; i < _dockAreas.Length; i++)
      {
        if (!_dockAreas[i].Contains(Cursor.Position) || !_dockType.HasFlag((DockableLocations)Mathf.Pow(2, i)))
          continue;

        dockableAreaSelected = (DockableLocations)Mathf.Pow(2, i);
        return true;
      }

      return false;
    }

    protected internal void Dock(InlineWindow window)
    {
      switch (dockableAreaSelected)
      {
        case DockableLocations.TopLeft:
          
          break;
        case DockableLocations.Top:
          break;
        case DockableLocations.TopRight:
          break;
        case DockableLocations.Left:
          break;
        case DockableLocations.Center:
          break;
        case DockableLocations.Right:
          break;
        case DockableLocations.BottomLeft:
          break;
        case DockableLocations.Bottom:
          break;
        case DockableLocations.BottomRight:
          break;
      }
    }
  }
}