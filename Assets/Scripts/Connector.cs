using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;

[ShowOdinSerializedPropertiesInInspector, Serializable]
public class Connector
{
  [OdinSerialize, ReadOnly] protected Connector connector;
}