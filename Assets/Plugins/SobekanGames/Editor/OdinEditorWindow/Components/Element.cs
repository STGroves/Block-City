using Sirenix.OdinInspector;
using Sirenix.Serialization;
using SobekanGames.OdinEditorWindow.Enums;
using UnityEngine;

namespace SobekanGames.OdinEditorWindow.Components
{
  [HideReferenceObjectPicker]
  public class Element
  {
    [OdinSerialize, HideIf("@true")] protected Rect _elementRect;
    [OdinSerialize, HideIf("@true")] protected int z;

    [OdinSerialize, HideIf("@true")] protected bool _moveable;
    [OdinSerialize, HideIf("@true")] protected ResizeType _resizeType;

    [OdinSerialize] protected Vector2 _minSize;

    public const int MARGIN = 2;

    public Element(Rect elementRect, bool canMove, ResizeType resizeType)
    {
      _elementRect = elementRect;
      _moveable = canMove;
      _resizeType = resizeType;
    }
  }
}
