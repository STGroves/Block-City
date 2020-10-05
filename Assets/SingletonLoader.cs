using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonLoader : SerializedMonoBehaviour
{
  [OdinSerialize] ISingleton[] Singletons;
}
