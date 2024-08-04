using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public List<MapData> mapDataList;

    public void EnterCombat(string enemyName)
    {
        Debug.Log("8");
        for(int i =0;  i < mapDataList.Count; i++)
        {
            if (mapDataList[i].mapName == GameManager.Instance.currentMapName)
            {
                for(int j =0; j < mapDataList[i].enemyData.Count; j++)
                {
                    if (mapDataList[i].enemyData[j].enemyName == enemyName)
                    {
                        GameManager.Instance.enemyData = mapDataList[i].enemyData[j];
                        Debug.Log("9");
                        GameManager.Instance.LoadScene();
                        return;
                    }
                }
            }
        }
    }
    public bool Encounter()
    {
        Debug.Log("2");
        for(int i =0; i < mapDataList.Count; i++)
        {
            if (mapDataList[i].mapName == GameManager.Instance.currentMapName)
            {
                MapData tempData = mapDataList[i];
                Debug.Log("3");
                for(int j =0; j < tempData.enemyData.Count; j++)
                {
                    if (Random.Range(0f,1f) < tempData.encounterPer[j])
                    {
                        EnterCombat(mapDataList[i].enemyData[j].enemyName);
                        Debug.Log("4");
                        return true;
                    }
                }
                Debug.Log("6");
                return false;
            }
        }
        Debug.Log("7");
        return false;
    }
}
