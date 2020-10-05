using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Singleton<T> : SerializedScriptableObject, ISingleton where T : UnityEngine.Object
{
  static T instance;

  public static T Instance
  {
    get
    {
      if (instance == null)
        instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();

      return instance;
    }

    protected set
    {
      instance = null;
    }
  }

  [Button]
  protected void ClearInstance()
  {
    Instance = null;
  }
}
