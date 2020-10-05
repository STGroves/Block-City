using SE = Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using SobekanGames.OdinEditorWindow.Components;
using UE = UnityEngine;

namespace SobekanGames.OdinEditorWindow
{
  public class OdinEditorWindow : SE.OdinEditorWindow
  {
    [OdinSerialize] protected Canvas canvas;

    protected override void Initialize()
    {
      base.Initialize();

      minSize = UE.Vector2.one * 50;
      maxSize = new UE.Vector2(640, 480);

      canvas = new Canvas(new UE.Rect(0, 0, 640, 480));
    }
  }
}