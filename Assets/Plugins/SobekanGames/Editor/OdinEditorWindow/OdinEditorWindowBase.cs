using System.Reflection;
using SE = Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using SobekanGames.OdinEditorWindow.Components;
using UE = UnityEngine;

namespace SobekanGames.OdinEditorWindow
{
  public class OdinEditorWindow : SE.OdinEditorWindow
  {
    [OdinSerialize] Panel panel;
    [OdinSerialize] protected DockArea _dockArea;

    protected T GetBase<T>() where T : Panel
    {
      return panel as T;
    }

    protected override void Initialize()
    {
      base.Initialize();

      minSize = UE.Vector2.one * 50;
      maxSize = new UE.Vector2(640, 480);

      panel = new Canvas(new UE.Rect(0, 0, 640, 480));
    }

    protected override void DrawEditors()
    {
      base.DrawEditors();

      _dockArea.ProcessEvents(UE.Event.current);

      MethodInfo m = panel.GetType().GetMethod("ProcessEvents", BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance);
      m?.Invoke(panel, new object[] {UE.Event.current});
    }
  }
}