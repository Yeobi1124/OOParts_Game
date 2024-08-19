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
    public DataExchangeManager exchange;
    public Enemyy enemy;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
            instance = this;
        exchange = FindObjectOfType<DataExchangeManager>();
    }

    private void Start()
    {
        int enemyId = exchange.enemyData.enemyCode;
        int xpos = ((exchange.enemyData.enemyCode / 100) -1)* 50 ; // x ��ǥ  0 50 100 ...
        int ypos = (int)Mathf.Floor((float)exchange.enemyData.enemyCode / 1000) * 50; // y��ǥ 0 50 100 ...
        enemy = pool.Make(exchange.enemyData.enemyCode, new Vector2(xpos, ypos)).GetComponent<Enemyy>(); // �� ����
        player.transform.Translate(xpos, ypos, 0); // �÷��̾ �̵�
        cam.transform.Translate(xpos, ypos, 0); // ī�޶� �̵�..
    }
}
