using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataExchangeManager : MonoBehaviour
{
    public static DataExchangeManager instance;

    [Header("인게임정보")]
    public int questId = 10;
    public int questActionIndex;
    public EnemyData enemyData;

    [Header("플레이어정보")]
    public int playerMaxHealth;
    public int playerHealth; 

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void DataMigrate(int _questId, int _questActionIndex, EnemyData _enemyData) // 스토리씬에 있던 정보를 저장
    {
        questId = _questId;
        questActionIndex = _questActionIndex;
        enemyData = _enemyData;
    }

    public void DataImmigrate() // 전투씬에 있던 정보를 스토리씬의 매니저들에 전달
    {
        GameManager.Instance.questManager.questId = questId;
        GameManager.Instance.questManager.questActionIndex = questActionIndex;
    }
}
