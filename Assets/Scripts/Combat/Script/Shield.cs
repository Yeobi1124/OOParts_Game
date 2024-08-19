using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float ReleaseGaugeFillSpeed;  // 게이지 차는 배율

    private void OnTriggerEnter2D(Collider2D collision) // 1.방출게이지 증가( 불렛 데미지 * 배율) 2. 불렛 비활성화
    {
        if (collision.CompareTag("Bullet") && collision.GetComponent<Bullet>().isEnemy)
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            CombatManager.instance.skill.defenseReleaseGauge += bullet.damage * ReleaseGaugeFillSpeed;
            collision.gameObject.SetActive(false);
        }
    }
}
