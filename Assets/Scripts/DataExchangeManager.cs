using JetBrains.Annotations;
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
    public bool combat;
    public string map;

    [Header("플레이어정보")]
    public int playerMaxHealth;
    public int playerHealth;
    public float playerX;
    public float playerY;

    [Header("이벤트 정보")]
    public bool apple;
    public bool anim;
    public bool boss;

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
        playerX = GameManager.Instance.player.transform.position.x;
        playerY = GameManager.Instance.player.transform.position.y;
        map = GameManager.Instance.currentMapName;
        questId = _questId;
        questActionIndex = _questActionIndex;
        enemyData = _enemyData;
        combat = true;
    }

    public void DataImmigrate() // 전투씬에 있던 정보를 스토리씬의 매니저들에 전달
    {
        if (combat)
        {
            GameManager.Instance.questManager.questId = questId;
            GameManager.Instance.questManager.questActionIndex = questActionIndex;
            GameManager.Instance.player.transform.position = new Vector3(playerX,playerY ,0);
            GameManager.Instance.currentMapName = map;
            combat = false;
        }
        else
        {
            return;
        }
    }
}
