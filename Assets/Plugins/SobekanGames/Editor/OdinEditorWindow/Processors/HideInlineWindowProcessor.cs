using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using SobekanGames.OdinEditorWindow.Components;
using System;
using System.Collections.Generic;

namespace SobekanGames.OdinEditorWindow.Processors
{
  public class HideInlineWindowProcessor<T> : OdinAttributeProcessor<T> where T : Canvas
  {
    public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
    {
      attributes.Add(new HideReferenceObjectPickerAttribute());
    }
  }
}