using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    public Camera cam;
    public SkillManager skill;
    public GameObject player;
    public CombatPoolManager pool;
    public DataExchangeManager exchange; //추가
    public Enemyy target;
    public Enemyy enemies;
    public GameObject border; // 추가
    void Awake()
    {
        if(instance == null)
            instance = this;
        exchange = FindObjectOfType<DataExchangeManager>();
        BgmManager.instance.StopAllCoroutines();
    }

    private void Start()
    {
        int enemyId = exchange.enemyData.enemyCode; //enemyId는 100 200 300 ...
        int xpos = ((exchange.enemyData.enemyCode / 100) - 1) * 50; // x 좌표  0 50 100 ...
        int ypos = (int)Mathf.Floor((float)exchange.enemyData.enemyCode / 1000) * 50; // y좌표 0 50 100 ...
        border.transform.position = new Vector3(xpos, ypos, 0); // 보더 이동
        target = pool.Make(exchange.enemyData.enemyCode, border.transform.position).GetComponent<Enemyy>(); // 적 생성
        player.transform.Translate(border.transform.position); // 플레이어도 이동
        cam.transform.Translate(xpos, ypos, 0); // 카메라도 이동..
        BgmManager.instance.Play(2);
    }
}
