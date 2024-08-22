using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float ReleaseGaugeFillSpeed;  // ������ ���� ����

    private void OnTriggerEnter2D(Collider2D collision) // 1.��������� ����( �ҷ� ������ * ����) 2. �ҷ� ��Ȱ��ȭ
    {
        if (collision.CompareTag("Bullet") && collision.GetComponent<Bullet>().isEnemy)
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            CombatManager.instance.skill.defenseReleaseGauge += bullet.damage * ReleaseGaugeFillSpeed;
            collision.gameObject.SetActive(false);
        }
    }
}
