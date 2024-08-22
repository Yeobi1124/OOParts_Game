using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    [Header("플레이어 정보")]
    public int maxHealth; // 최대 체력
    public int health; // 현재 체력
    public bool isHitted;

    [Header("인스턴스")]
    public DataExchangeManager exchangeManager;
    public SpriteRenderer sprite;
    public Collider2D col;

    public Material origShader;
    public Material hitShader;
    
    private void Awake()
    {
        exchangeManager = FindObjectOfType<DataExchangeManager>().GetComponent<DataExchangeManager>(); // 데이터 교환 매니저를 플레이어정보 스크립트에서 직접 인스턴스화
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        isHitted = false;

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
        if (other.CompareTag("Bullet") && other.gameObject.GetComponent<Bullet>().isEnemy && !isHitted) // 피격판정 // 스프라이트
        {
            isHitted = true;
            OnDamaged(other.gameObject.GetComponent<Bullet>().damage);
            other.gameObject.SetActive(false);
        }
    }

    public void OnDamaged(int damage)
    {
        //1.데미지 입음
        health -= damage;
        StartCoroutine(OnDamaged());
    }

    // 피격 코루틴
    IEnumerator OnDamaged()
    {
        //2. 스프라이트 색 변경 3. 무적 판정 4.혹시 디버프 기능을 넣는다. 하면 디버프까지
        sprite.material = hitShader;
        yield return new WaitForSeconds(0.1f);
        sprite.material = origShader;
        isHitted = false;
    }
}
