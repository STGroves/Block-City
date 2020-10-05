using UnityEngine;
using System.Collections;
using System;
using Sirenix.Serialization;
using Sirenix.OdinInspector;

public class NewBehaviourScript1 : SerializedMonoBehaviour
{
  [Serializable]
  public class Test
  {
    [OdinSerialize] public Type type;
    [OdinSerialize, ShowDrawerChain] public object value;
  }

  [OdinSerialize] public Test[] tests;
}
