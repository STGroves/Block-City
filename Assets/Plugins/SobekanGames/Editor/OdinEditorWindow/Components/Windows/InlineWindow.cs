using Sirenix.Serialization;
using SobekanGames.OdinEditorWindow.Enums;
using UnityEngine;

namespace SobekanGames.OdinEditorWindow.Components
{
  public class InlineWindow : Panel
  {
    public string Title
    {
      get => _title;
      set => _title = value;
    }
    
    public bool Docked => _docked;

    [OdinSerialize] protected bool _docked;

    [OdinSerialize] protected string _title;
    [OdinSerialize] protected GUIStyle _titleStyle;
    

    protected const float MIN_SIZE = 15;

    public InlineWindow(Rect windowRect, ResizeType resizeType = ResizeType.All, bool canMove = false) : base(windowRect, canMove, resizeType)
    {
      SetMinSize(MIN_SIZE, MIN_SIZE);

      GenerateDefaultTexture(ref _backgroundTexture, new Color(0.25f, 0.25f, 0.25f));
      UpdateStyles();
    }

    public InlineWindow(Rect windowRect, string title, ResizeType resizeType = ResizeType.All, bool canMove = false) : base(windowRect, canMove, resizeType)
    {
      SetMinSize(MIN_SIZE, MIN_SIZE);

      _title = title;

      GenerateDefaultTexture(ref _backgroundTexture, new Color(0.25f, 0.25f, 0.25f));
      UpdateStyles();
    }

    public void SetMinSize(Vector2 minSize)
    {
      SetMinSize(minSize.x, minSize.y);
    }

    public void SetMinSize(float x, float y)
    {
      _minSize = new Vector2(Mathf.Max(x, MIN_SIZE), Mathf.Max(y, MIN_SIZE));
    }

    protected override void UpdateStyles()
    {
      base.UpdateStyles();

      _backgroundStyle.padding = new RectOffset(MARGIN, MARGIN, MARGIN, MARGIN);

      _titleStyle = new GUIStyle { fontStyle = FontStyle.Normal, fontSize = 16, normal = new GUIStyleState() { textColor = new Color(0.60546875f, 0.60546875f, 0.60546875f) } };
    }
  }
}
