using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    [Header("�÷��̾� ����")]
    public int maxHealth; // �ִ� ü��
    public int health; // ���� ü��

    [Header("�ν��Ͻ�")]
    public DataExchangeManager exchangeManager;
    public SpriteRenderer sprite;
    public Collider2D col;

    private void Awake()
    {
        exchangeManager = FindObjectOfType<DataExchangeManager>().GetComponent<DataExchangeManager>(); // ������ ��ȯ �Ŵ����� �÷��̾����� ��ũ��Ʈ���� ���� �ν��Ͻ�ȭ
        sprite = FindObjectOfType<SpriteRenderer>();
        col = FindObjectOfType<Collider2D>();

        maxHealth = exchangeManager.playerMaxHealth;
        health = exchangeManager.playerHealth; // �̴ϼȶ������̼�
    }

    private void Update()
    {
        if(health <= 0)
        {
            //���ӿ��� ó��
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet" && other.gameObject.GetComponent<Bullet>().isEnemy) // �ǰ����� // ��������Ʈ
        {
            StartCoroutine(OnDamaged(other.gameObject.GetComponent<Bullet>()));
            
            other.gameObject.SetActive(false);
        }
    }

    // �ǰ� �ڷ�ƾ
    IEnumerator OnDamaged(Bullet bullet)
    {
        // 1. ������ ���� 2. ��������Ʈ �� ����(�����ؾ���) 3. ���� ���� 4.Ȥ�� ����� ����� �ִ´�. �ϸ� ���������
        health -= bullet.damage;
        sprite.color = Color.red;
        col.enabled = false;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        col.enabled = true;
    }
}
