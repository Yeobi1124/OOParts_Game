using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    [Header("�÷��̾� ����")]
    public int maxHealth; // �ִ� ü��
    public int health; // ���� ü��
    public bool isHitted;

    [Header("�ν��Ͻ�")]
    public DataExchangeManager exchangeManager;
    public SpriteRenderer sprite;
    public Collider2D col;

    public Material origShader;
    public Material hitShader;
    
    private void Awake()
    {
        exchangeManager = FindObjectOfType<DataExchangeManager>().GetComponent<DataExchangeManager>(); // ������ ��ȯ �Ŵ����� �÷��̾����� ��ũ��Ʈ���� ���� �ν��Ͻ�ȭ
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        isHitted = false;

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
        if (other.CompareTag("Bullet") && other.gameObject.GetComponent<Bullet>().isEnemy && !isHitted) // �ǰ����� // ��������Ʈ
        {
            isHitted = true;
            OnDamaged(other.gameObject.GetComponent<Bullet>().damage);
            other.gameObject.SetActive(false);
        }
        else if(other.CompareTag("UnblockBullet") && other.gameObject.GetComponent<Bullet>().isEnemy && !isHitted)
        {
            isHitted = true;
            OnDamaged(other.gameObject.GetComponent<Bullet>().damage);
        }
    }

    public void OnDamaged(int damage)
    {
        //1.������ ����
        health -= damage;
        StartCoroutine(OnDamaged());
    }

    // �ǰ� �ڷ�ƾ
    IEnumerator OnDamaged()
    {
        //2. ��������Ʈ �� ���� 3. ���� ���� 4.Ȥ�� ����� ����� �ִ´�. �ϸ� ���������
        sprite.material = hitShader;
        yield return new WaitForSeconds(0.1f);
        sprite.material = origShader;
        isHitted = false;
    }
}
