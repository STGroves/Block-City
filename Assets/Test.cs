using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

public class Test : SerializedScriptableObject
{
  [TabGroup("Test")]
  public int hi;
  //[TabGroup("Test B")]
  public int hi2;
}