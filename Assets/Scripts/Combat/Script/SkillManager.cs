using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
- 스킬 별 스펙
	- 투사체 공격
		- 공격 사거리
		- 공격 데미지
		- 차징 게이지
		- 최대 차징 게이지
		- 차징 속도
	- 배리어
		- 최대 배리어 게이지
		- 현 배리어 게이지
		- 축적된 배리어 방출 게이지
		- 최대 배리어 방출 게이지
		- 배리어 범위
		- 배리어 생성 거리
- 플레이어 입력 반응
	- 좌클릭 시 차징 시작
		- 차징 도중엔 플레이어 이동속도 감소
		- 차징 완료 시 이동 방향으로 투사체 발사
		  (Pool Manager에 BulletData 전달하며 요청)
	- 우클릭 시 플레이어 - 마우스 위치 방향 생성 거리에 배리어 생성
- 축적된 배리어 방출 게이지 최대 도달 시 배리어 방출
	- 방출 범위는 맵 전체

현재 미구현인 파트
- 스킬 강화에 따른 수치, 기능 변화
	- 수치 변화 이벤트 발생 시 저장된 스펙 변화
	- 기능 변화 이벤트 발생 시 기능 추가
*/
public class SkillManager : MonoBehaviour
{
    [Header("Attack Skill")]
    public int attackSpeed; //투사체 속도
    public int attackRange; //공격 사거리
    public int attackDamage; //공격 데미지
    public float attackCharge; //공격 차지 게이지 현재값
    public float attackMaxCharge; //공격 차지 게이지 최댓값
    public float attackChargeSpeed; //공격 차지 게이지 증가 속도
    public bool isCasting; //차징 여부

    [Header("Defense Skill")]
    public float defenseRange; //보호막 범위
    public int defenseDist; //보호막 생성 거리 (플레이어로부터 얼마나 떨어져있는지)
    public float defenseGauge; //보호막 게이지 현재값
    public float defenseMaxGauge; //보호막 게이지 최댓값
    public float defenseGaugeFillSpeed; //보호막 게이지 증가 속도 (자연 회복)
    public float defenseGaugeReduceSpeed; //보호막 게이지 감소 속도 (사용 시 감소)
    public int defenseReleaseGauge; //보호막 축적 게이지
    public int defenseReleaseMaxGauge; //보호막 축적 게이지 최댓값
    public bool isDefending; //보호막 사용 중 인지 여부
    public GameObject shield; //보호막 오브젝트
    public GameObject shieldRelease; //보호막 방출(공격) 시 데미지 범위 관련 오브젝트

    GameObject player; //플레이어 오브젝트
    CombatPoolManager pool;
    Camera cam; //Combat Scene의 Main Camera. 보호막 사용 시 마우스 위치를 불러오기 위해 사용
    

    private void Awake() {
        isCasting = false;
        isDefending = false;

        player = CombatManager.instance.player;
        pool = CombatManager.instance.pool;
        cam = CombatManager.instance.cam;

        defenseGauge = defenseMaxGauge;
    }
    private void Update() {
        //공격 스킬 입력 처리
        if(Input.GetMouseButtonDown(0) && !isDefending){
            AttackCastOn();
        }

        if(Input.GetMouseButton(0) && isCasting){
            AttackCasting();
        }

        if(Input.GetMouseButtonUp(0) && isCasting){
            AttackCastCancel();
        }

        //게이지를 전부 채웠을 시 투사체 발사
        if(attackCharge >= attackMaxCharge && isCasting){
            AttackAct();
        }

        //방어 스킬 입력 처리
        if(Input.GetMouseButtonDown(1) && !isCasting){
            DefenseOn();
        }

        if(Input.GetMouseButton(1) && isDefending){
            DefenseIng();
        }

        if((Input.GetMouseButtonUp(1) && isDefending) || defenseGauge <= 0){
            DefenseCancel();
        }

        //방출 게이지가 전부 채워졌을 시 공격
        if(defenseReleaseGauge >= defenseReleaseMaxGauge){
            StartCoroutine(DefenseRelease());
        }

        //보호막 게이지 회복 및 일부 예외 처리
        defenseGauge += defenseGaugeFillSpeed * Time.deltaTime;
        if(defenseGauge > defenseMaxGauge) defenseGauge = defenseMaxGauge;
        if(defenseGauge < 0) defenseGauge = 0;
    }

    /// <summary>
    /// 공격 스킬 캐스팅 시작 시
    /// </summary>
    private void AttackCastOn(){
        isCasting = true;
    }

    /// <summary>
    /// 공격 스킬 캐스팅 도중
    /// </summary>
    private void AttackCasting(){
        //플레이어 속도 느리게, 차징 게이지 수치, UI 변화, 플레이어 애니메이션
        attackCharge += attackChargeSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 공격 스킬 취소 시
    /// </summary>
    private void AttackCastCancel(){
        isCasting = false;
        attackCharge = 0;
    }
    
    /// <summary>
    /// 차지 게이지가 전부 채워져 공격 스킬 발동 시 pool Manager를 통해 투사체 생성, 투사체 발사
    /// </summary>
    private void AttackAct(){
        Vector2 playerPos = player.transform.position;
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        bool dir = playerPos.x < mousePos.x; //true: 오른쪽, false: 왼쪽

        BulletMove bulletMove = pool.Make(0, playerPos).GetComponent<BulletMove>();
        bulletMove.Set(attackSpeed, dir ? Vector2.right : Vector2.left);
        bulletMove.Act();

        attackCharge = 0;
    }

    /// <summary>
    /// 방어 스킬 시작 시
    /// </summary>
    private void DefenseOn(){
        isDefending = true;

        Vector2 playerPos = player.transform.position;//임시로 이렇게 가져옴. 나중에 GameManager 같은 거 추가해서 instance화 추천
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        shield.SetActive(true);
        shield.transform.localScale = new Vector3(defenseRange, shield.transform.localScale.y, 0);
        shield.transform.position = player.transform.position;
        shield.transform.rotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector3.up, mousePos - playerPos).eulerAngles.z);
        shield.transform.Translate(shield.transform.up * defenseDist, Space.World);
    }

    /// <summary>
    /// 방어 스킬 도중
    /// </summary>
    private void DefenseIng(){
        defenseGauge -= defenseGaugeReduceSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 방어 스킬 취소 시
    /// </summary>
    private void DefenseCancel(){
        isDefending = false;
        shield.SetActive(false);
    }

    /// <summary>
    /// 방어 스킬 방출 시
    /// </summary>
    /// <returns></returns>
    private IEnumerator DefenseRelease(){
        shieldRelease.SetActive(true);

        yield return new WaitForSecondsRealtime(1);

        shieldRelease.SetActive(false);
    }
}
