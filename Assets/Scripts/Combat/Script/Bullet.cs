using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/*
BulletData
- id
- 데미지
- 생성 위치
- 이동 속도
- 이동 방식 (아마 콜백함수로? 구현 가능성 검토 필요)
	- 직진 (예시)
		- 방향을 Unit Vector로 받고 이동하는 함수
	- 추적 (예시)
		- Bullet - 플레이어 방향 Unit Vector를 계산해서 이동하는 함수
*/
public class Bullet : MonoBehaviour
{
    public int id;
    public int damage;
    public Vector2 pos;
    public int speed;

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Border")
            gameObject.SetActive(false);
    }
}
