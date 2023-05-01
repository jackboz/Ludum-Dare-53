using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveDescription", order = 1)]
public class WaveDescription : ScriptableObject
{
    public enum SpawnType
    {
        Front,
        HalfFrontCircle,
        FullCircle,
        SideWays
    }

    public SpawnType spawnType;

    public int NumberOfEnemies1;

    public int NumberOfRangedEnemies;

    public List<float> Enemies1SpeedBeforeSteal;
    public List<float> Enemies1SpeedAfterSteal;

    public List<float> RangedEnemiesSpeed;
    public List<float> RangedEnemiesFireSpeed;
}
