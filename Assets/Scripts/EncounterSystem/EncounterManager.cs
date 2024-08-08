using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public List<MapData> mapDataList;

    public void EnterCombat(string enemyName)
    {
        for(int i =0;  i < mapDataList.Count; i++)
        {
            if (mapDataList[i].mapName == GameManager.Instance.currentMapName)
            {
                for(int j =0; j < mapDataList[i].enemyData.Count; j++)
                {
                    if (mapDataList[i].enemyData[j].enemyName == enemyName)
                    {
                        GameManager.Instance.enemyData = mapDataList[i].enemyData[j];
                        GameManager.Instance.LoadScene();
                        return;
                    }
                }
            }
        }
    }
    public bool Encounter()
    {
        for(int i =0; i < mapDataList.Count; i++)
        {
            if (mapDataList[i].mapName == GameManager.Instance.currentMapName)
            {
                MapData tempData = mapDataList[i];
                for(int j =0; j < tempData.enemyData.Count; j++)
                {
                    if (Random.Range(0f,1f) < tempData.encounterPer[j])
                    {
                        EnterCombat(mapDataList[i].enemyData[j].enemyName);
                        return true;
                    }
                }
                return false;
            }
        }
        return false;
    }
}
