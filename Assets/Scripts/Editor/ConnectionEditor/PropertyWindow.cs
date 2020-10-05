using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public abstract class PropertyWindow
{
  protected Rect propRect;
  protected string winTitle;
  protected GUIStyle windowBackStyle = new GUIStyle();
  protected GUIStyle titleStyle = new GUIStyle(EditorStyles.largeLabel);

  public PropertyWindow(Rect rect)
  {
    propRect = rect;
    CreateWindowBackground();
  }

  public PropertyWindow(Rect rect, string title)
  {
    propRect = rect;
    winTitle = title;

    CreateWindowBackground();
  }

  void CreateWindowBackground()
  {
    windowBackStyle.normal.background = new Texture2D(1, 1);
    windowBackStyle.normal.background.SetPixel(0, 0, Color.grey);
    windowBackStyle.normal.background.Apply();
  }

  public virtual void Draw()
  {
    //GUILayout.BeginArea(propRect);
    DrawBackground();
    //GUILayout.BeginVertical();
    
    if (!string.IsNullOrEmpty(winTitle))
    {
      DrawTitle();
      SirenixEditorGUI.HorizontalLineSeparator();
    }

    DrawContent();
    //GUILayout.EndVertical();
    //GUILayout.EndArea();
  }

  protected virtual void DrawContent() {}

  protected virtual void DrawBackground()
  {
    GUI.Box(propRect, "",windowBackStyle);
  }
  protected virtual void DrawTitle()
  {
    EditorGUILayout.BeginHorizontal("Box");
    EditorGUILayout.LabelField(winTitle, titleStyle);
    EditorGUILayout.EndHorizontal();
  }
}
