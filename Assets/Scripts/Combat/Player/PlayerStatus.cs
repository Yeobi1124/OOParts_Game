using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    [Header("플레이어 정보")]
    public int maxHealth; // 최대 체력
    public int health; // 현재 체력

    [Header("인스턴스")]
    public DataExchangeManager exchangeManager;
    public SpriteRenderer sprite;
    public Collider2D col;

    private void Awake()
    {
        exchangeManager = FindObjectOfType<DataExchangeManager>().GetComponent<DataExchangeManager>(); // 데이터 교환 매니저를 플레이어정보 스크립트에서 직접 인스턴스화
        sprite = FindObjectOfType<SpriteRenderer>();
        col = FindObjectOfType<Collider2D>();

        maxHealth = exchangeManager.playerMaxHealth;
        health = exchangeManager.playerHealth; // 이니셜라이제이션
    }

    private void Update()
    {
        if(health <= 0)
        {
            //게임오버 처리
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet" && other.gameObject.GetComponent<Bullet>().isEnemy) // 피격판정 // 스프라이트
        {
            StartCoroutine(OnDamaged(other.gameObject.GetComponent<Bullet>()));
            
            other.gameObject.SetActive(false);
        }
    }

    // 피격 코루틴
    IEnumerator OnDamaged(Bullet bullet)
    {
        // 1. 데미지 입음 2. 스프라이트 색 변경(수정해야함) 3. 무적 판정 4.혹시 디버프 기능을 넣는다. 하면 디버프까지
        health -= bullet.damage;
        sprite.color = Color.red;
        col.enabled = false;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        col.enabled = true;
    }
}
