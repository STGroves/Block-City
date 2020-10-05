using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "PathManager", menuName = "ScriptableObjects/Managers/PathManager")]
public class PathManager : Singleton<PathManager>
{
  [OdinSerialize] public string[] Layers { get; private set; }

}