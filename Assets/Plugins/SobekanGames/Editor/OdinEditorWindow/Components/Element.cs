using Sirenix.Serialization;
using SobekanGames.OdinEditorWindow.Enums;
using UnityEngine;

namespace SobekanGames.OdinEditorWindow.Components
{
  public class Element
  {
    [OdinSerialize] protected Rect _elementRect;
    [OdinSerialize] protected int _z = 0;

    [OdinSerialize] protected bool _moveable;
    [OdinSerialize] protected ResizeType _resizeType;

    [OdinSerialize] protected Vector2 _minSize;
    [OdinSerialize] protected Vector2 _maxSize;

    public int ZLayer => _z;

    public const int MARGIN = 2;

    public Element(Rect elementRect, bool canMove, ResizeType resizeType)
    {
      _elementRect = elementRect;
      _moveable = canMove;
      _resizeType = resizeType;
    }

    public void SetZ(int zLayer)
    {
      _z = zLayer;
    }
  }
}
