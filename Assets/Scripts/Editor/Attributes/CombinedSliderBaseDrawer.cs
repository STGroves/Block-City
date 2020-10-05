using Sirenix.OdinInspector.Editor;
using System;
using UnityEditor;
using UnityEngine;

[DrawerPriority(DrawerPriorityLevel.ValuePriority)]
public abstract class CombinedSliderBaseDrawer<T> : OdinAttributeDrawer<CombinedSlider>
{
  bool open;
  protected T[] values;

  protected abstract bool CanDrawTypeInternal(Type type);
  protected abstract T DrawGUIInternal(string label, int idx);
  protected abstract void AdjustSlidersInternal(int idx, T oldValue);

  protected override void Initialize()
  {
    if (Property.ValueEntry.WeakSmartValue == null)
      Property.ValueEntry.WeakSmartValue = new T[Attribute.SliderCount];
  }

  public override bool CanDrawTypeFilter(Type type)
  {
    return CanDrawTypeInternal(type);
  }

  protected override void DrawPropertyLayout(GUIContent label)
  {
    open = EditorGUILayout.Foldout(open, label);

    if (!open)
      return;

    if (Attribute.Labels != null && Attribute.Labels.Length != Attribute.SliderCount)
    {
      Sirenix.Utilities.Editor.SirenixEditorGUI.ErrorMessageBox("The number of labels does not match the number of sliders!");
      return;
    }

    values = (T[])Property.ValueEntry.WeakSmartValue;

    if (values.Length != Attribute.SliderCount)
      UpdateSliderCount(Attribute.SliderCount);

    EditorGUI.indentLevel++;

    bool useLabels = Attribute.Labels != null && Attribute.Labels.Length > 0;

    for (int i = 0; i < values.Length; i++)
    {
      T oldValue = values[i];
      EditorGUI.BeginChangeCheck();
      values[i] = DrawGUIInternal(useLabels ? Attribute.Labels[i] : i.ToString(), i);

      if (EditorGUI.EndChangeCheck())
      {
        AdjustSlidersInternal(i, oldValue);
        break;
      }
    }

    EditorGUI.indentLevel--;

    Property.ValueEntry.WeakSmartValue = values;
  }

  private void UpdateSliderCount(int sliderCount)
  {
    Array.Resize(ref values, sliderCount);
  }
}
