using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class RoadConnector : Connector
{
  [OdinSerialize, MinValue(1)] int laneCount;
}