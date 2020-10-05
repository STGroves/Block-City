using System;
using UnityEditor;
using UnityEngine;

public class CombinedSliderIntDrawer : CombinedSliderBaseDrawer<int>
{
  protected override void Initialize()
  {
    if (Property.ValueEntry.WeakSmartValue == null)
      Property.ValueEntry.WeakSmartValue = new int[Attribute.SliderCount];
  }

  protected override bool CanDrawTypeInternal(Type type)
  {
    return type == typeof(int[]);
  }

  protected override int DrawGUIInternal(string label, int idx)
  {
    return EditorGUILayout.IntSlider(label, values[idx], (int)Attribute.Min, (int)Attribute.Max);
  }

  protected override void AdjustSlidersInternal(int idx, int oldValue)
  {
    int diff = Mathf.RoundToInt(values[idx] - oldValue);
    int i = values.Length - 1;
    int valid = i;
    int revolutions = 0;

    while (diff != 0 && revolutions <= values.Length)
    {
      if (i == idx)
      {
        i--;

        if (i < 0 && diff != 0)
          i = values.Length - 1;

        revolutions++;

        continue;
      }

      int delta = Mathf.RoundToInt(diff / valid);

      if (values[i] - delta < Attribute.Min)
      {
        diff -= values[i] - (int)Attribute.Min;
        values[i] = (int)Attribute.Min;
      }

      else if (values[i] - delta > Attribute.Max)
      {
        diff -= (int)Attribute.Max - values[i];
        values[i] = (int)Attribute.Max;
      }

      else if (values[i] >= Attribute.Min && values[i] <= Attribute.Max)
      {
        values[i] -= delta;
        diff -= delta;
      }

      int roundedNum = Mathf.RoundToInt(values[i]);

      if (Mathf.Abs(values[i] - roundedNum) <= 0.0025f)
        values[i] = roundedNum;

      i--;

      if (i < 0 && diff != 0)
        i = values.Length - 1;

      revolutions++;
    }
  }
}
