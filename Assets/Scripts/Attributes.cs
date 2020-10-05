using System;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class CombinedSlider : Attribute
{
  public float Min { get; set; }
  public float Max { get; set; }
  public string[] Labels { get; set; }

  public int SliderCount { get; private set; }

  public CombinedSlider(int sliderCount)
  {
    if (sliderCount <= 1)
      sliderCount = 2;

    SliderCount = sliderCount;
  }
}