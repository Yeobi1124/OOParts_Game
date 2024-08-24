using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    public Camera cam;
    public SkillManager skill;
    public GameObject player;
    public CombatPoolManager pool;
    public DataExchangeManager exchange; //�߰�
    public Enemyy target;
    public Enemyy enemies;
    public GameObject border; // �߰�
    void Awake()
    {
        if(instance == null)
            instance = this;
        exchange = FindObjectOfType<DataExchangeManager>();
        BgmManager.instance.StopAllCoroutines();
    }

    private void Start()
    {
        int enemyId = exchange.enemyData.enemyCode; //enemyId�� 100 200 300 ...
        int xpos = ((exchange.enemyData.enemyCode / 100) - 1) * 50; // x ��ǥ  0 50 100 ...
        int ypos = (int)Mathf.Floor((float)exchange.enemyData.enemyCode / 1000) * 50; // y��ǥ 0 50 100 ...
        border.transform.position = new Vector3(xpos, ypos, 0); // ���� �̵�
        target = pool.Make(exchange.enemyData.enemyCode, border.transform.position).GetComponent<Enemyy>(); // �� ����
        player.transform.Translate(border.transform.position); // �÷��̾ �̵�
        cam.transform.Translate(xpos, ypos, 0); // ī�޶� �̵�..
        BgmManager.instance.Play(2);

        EventManager.Instance.AddEventListner(CombatEventType.Win, (CombatEventType type, Component Sender, object param) => {
            Debug.Log("Player Win");
            SceneManager.LoadScene(1);
        });

        EventManager.Instance.AddEventListner(CombatEventType.Lose, (CombatEventType type, Component Sender, object param) => {
            Debug.Log("Player Lose");
            SceneManager.LoadScene("Story");
        });
    }
}
