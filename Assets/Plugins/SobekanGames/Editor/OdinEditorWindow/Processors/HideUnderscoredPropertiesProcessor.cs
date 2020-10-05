using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using SobekanGames.OdinEditorWindow.Components;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SobekanGames.OdinEditorWindow.Processors
{
  public class HideUnderscoredPropertiesProcessor<T> : OdinAttributeProcessor<T> where T : Element
  {
    public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
    {
      if (member.Name[0] == '_')
        attributes.Add(new HideIfAttribute("@true"));
    }
  }
}