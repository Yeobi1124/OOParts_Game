using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class Abel : MonoBehaviour
{
    [Header("�⺻ ����")]
    [SerializeField] Enemyy enemy;
    [SerializeField] Collider2D col;
    [Header("���� ���� ����")]
    [SerializeField] float time;
    [SerializeField] bool isRunning = false; // ���� �������ΰ�
    [Header("�÷��̾� �ǰ�")]
    [SerializeField] bool canDamagePlayer = true;
    public int hitDamage;

    private void Awake()
    {
        enemy = GetComponent<Enemyy>();
        col = GetComponent<Collider2D>();
        enemy.health = enemy.maxHealth;
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (!isRunning)
        {
            isRunning = true;
            if (time >= 0 && time < 10)
                StartCoroutine(FirstPattern());
        }
    }

    IEnumerator FirstPattern()
    {
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && !other.GetComponent<Bullet>().isEnemy)
        {
            enemy.health -= other.GetComponent<Bullet>().damage;
        }
        if (other.CompareTag("Player") && canDamagePlayer)
        {
            StartCoroutine(HitDamageCoroutine(other.gameObject.GetComponent<PlayerStatus>()));
        }
        
    }

    IEnumerator HitDamageCoroutine(PlayerStatus player)
    {
        canDamagePlayer = false;
        player.OnDamaged(hitDamage); // �÷��̾� ��ü �ǰ� �Լ� ����
        yield return new WaitForSeconds(2f); // ���� �浹 ����
        canDamagePlayer = true;
    }
}