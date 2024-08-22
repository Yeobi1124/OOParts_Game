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
                        //GameManager.Instance.enemyData = mapDataList[i].enemyData[j];
                        GameManager.Instance.LoadScene(mapDataList[i].enemyData[j]);
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
                float perSum = 0;
                MapData tempData = mapDataList[i];
                float randNum = Random.Range(0f, 1f);
                for (int j =0; j < tempData.enemyData.Count; j++)
                {
                    if (randNum < perSum + tempData.encounterPer[j])
                    {
                        EnterCombat(mapDataList[i].enemyData[j].enemyName);
                        return true;
                    }
                    perSum += tempData.encounterPer[j];
                }
                return false;
            }
        }
        return false;
    }
}
