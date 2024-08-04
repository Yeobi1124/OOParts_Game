using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "MapData")]
public class MapData : ScriptableObject
{
    public string mapName;

    public List<EnemyData> enemyData;
    public List<float> encounterPer;

    //public Dictionary<EnemyData, float> encounterData;

    /*private void Awake()
    {
        encounterData = new Dictionary<EnemyData, float>();
        for(int i =0; i <enemyData.Count; i++)
        {
            encounterData.Add(enemyData[i], encounterPer[i]);
        }
    }*/
}
