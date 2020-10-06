using System.Linq;
using Sirenix.OdinInspector;
using SE = Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using SobekanGames.OdinEditorWindow.Components;
using UnityEditor;
using static Sirenix.OdinInspector.Editor.InspectorUtilities;
using UE = UnityEngine;

namespace SobekanGames.OdinEditorWindow
{
  [HideReferenceObjectPicker]
  public class OdinEditorWindow : SE.OdinEditorWindow
  {
    [OdinSerialize] protected Panel canvas;

    protected override void Initialize()
    {
      base.Initialize();

      minSize = UE.Vector2.one * 50;
      maxSize = new UE.Vector2(640, 480);

      canvas = new Panel(new UE.Rect(0, 0, 640, 480));
    }

    protected override void DrawEditors()
    {
      int index = CurrentDrawingTargets.IndexOf(this);

      Editor editor = Editor.CreateEditor((UE.Object)CurrentDrawingTargets[index]);
      SE.PropertyTree propertyTree = SE.PropertyTree.Create(editor.serializedObject);

      if (propertyTree == null && (editor == null || editor.target == null))
        return;

      if (propertyTree != null)
      {
        bool applyUndo = propertyTree.WeakTargets.FirstOrDefault() as UE.Object;
        Draw(propertyTree, applyUndo);
      }

      else
      {
        SE.OdinEditor.ForceHideMonoScriptInEditor = true;
        
        try
        {
          editor.OnInspectorGUI();
        }
        
        finally
        {
          SE.OdinEditor.ForceHideMonoScriptInEditor = false;
        }
      }
    }

    void Draw(SE.PropertyTree propertyTree, bool applyUndo)
    {
      BeginDrawPropertyTree(propertyTree, applyUndo);
      SE.InspectorProperty root = propertyTree.GetRootProperty(0);
      root.Draw();
      EndDrawPropertyTree(propertyTree);
    }
  }
}