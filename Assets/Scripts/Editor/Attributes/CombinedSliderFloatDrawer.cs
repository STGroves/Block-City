using Sirenix.OdinInspector.Editor;
using System;
using UnityEditor;
using UnityEngine;

public class CombinedSliderFloatDrawer : CombinedSliderBaseDrawer<float>
{
  protected override bool CanDrawTypeInternal(Type type)
  {
    return type == typeof(float[]);
  }

  protected override float DrawGUIInternal(string label, int idx)
  {
    return EditorGUILayout.Slider(label, values[idx], Attribute.Min, Attribute.Max);
  }

  protected override void AdjustSlidersInternal(int idx, float oldValue)
  {
    float diff = values[idx] - oldValue;
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

      float delta = diff / valid;

      if (values[i] - delta < Attribute.Min)
      {
        diff -= values[i] - Attribute.Min;
        values[i] = Attribute.Min;
      }

      else if (values[i] - delta > Attribute.Max)
      {
        diff -= Attribute.Max - values[i];
        values[i] = Attribute.Max;
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
