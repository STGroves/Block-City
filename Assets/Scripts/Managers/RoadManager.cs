using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "RoadManager", menuName = "ScriptableObjects/Managers/RoadManager")]
class RoadManager : Singleton<RoadManager>
{
  [OdinSerialize, ValueDropdown("GetLayers")] public string Layer { get; private set; }
  [OdinSerialize, MinValue(0)] public float MinLaneWidth { get; private set; }

  string[] GetLayers()
  {
    return PathManager.Instance.Layers;
  }
}
