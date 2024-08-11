using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataExchangeManager : MonoBehaviour
{
    public static DataExchangeManager instance;
    int questId;
    int questActionIndex;
    EnemyData enemyData;

    private void Awake()
    {
        if(instance == null)
        {
            questId = 10;
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void DataMigrate(int _questId, int _questActionIndex, EnemyData _enemyData) // ���丮���� �ִ� ������ ����
    {
        questId = _questId;
        questActionIndex = _questActionIndex;
        enemyData = _enemyData;
    }

    public void DataImmigrate() // �������� �ִ� ������ ���丮���� �Ŵ����鿡 ����
    {
        GameManager.Instance.questManager.questId = questId;
        GameManager.Instance.questManager.questActionIndex = questActionIndex;
    }
}
