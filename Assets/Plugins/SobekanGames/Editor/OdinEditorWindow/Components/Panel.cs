using Sirenix.Serialization;
using SobekanGames.OdinEditorWindow.Enums;
using UnityEngine;

namespace SobekanGames.OdinEditorWindow.Components
{
  public class Panel : Element
  {
    [OdinSerialize] protected GUIStyle _backgroundStyle;
    [OdinSerialize] protected Texture2D _backgroundTexture;

    public Panel(Rect canvasRect, bool canMove = false, ResizeType resizeType = ResizeType.None) : base(canvasRect, canMove, resizeType)
    {
      GenerateDefaultTexture(ref _backgroundTexture, Color.grey);
      UpdateStyles();
    }

    protected virtual void UpdateStyles()
    {
      if (_backgroundStyle == null)
        _backgroundStyle = new GUIStyle();

      _backgroundStyle.normal.background = _backgroundTexture;
    }

    protected void SetPadding(RectOffset rect)
    {
      _backgroundStyle.padding = rect;
    }

    protected void SetPadding(int vertical, int horizontal)
    {
      SetPadding(new RectOffset(horizontal, vertical, horizontal, vertical));
    }

    protected void GenerateDefaultTexture(ref Texture2D texture, Color colour)
    {
      texture = new Texture2D(1, 1);
      texture.SetPixel(0, 0, colour);
      texture.Apply();
    }

    public void SetWindowTexture(Texture2D texture)
    {
      _backgroundTexture = texture;
      UpdateStyles();
    }
  }
}