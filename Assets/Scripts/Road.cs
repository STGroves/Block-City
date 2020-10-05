using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class Road : SerializedMonoBehaviour
{
  [OdinSerialize] public Junction[] Junctions { get; private set; }
}

public class Junction
{
  [OdinSerialize, CombinedSlider(2, Labels = new string[] { "In", "Out" }, Max = 2, Min = 0)] int[] laneCount;
}

public class Lane
{

}