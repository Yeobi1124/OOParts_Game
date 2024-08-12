using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public float downJumpTime;

    bool isJump; //점프중인가? 
    bool onSkyGround; //공중발판의 위인가?

    Rigidbody2D rigid;
    Animator anim;
    Collider2D col;

    //PlatformScript skyGround; // skyGround 플랫폼 스크립트 가져옴 현재 필요없음
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //skyGround = FindObjectOfType<PlatformScript>(); 현재 필요없음
        col = GetComponent<Collider2D>();
        isJump = false;
    }
    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if(rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < - maxSpeed)
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
    }

    void Update()
    {
        if (!isJump)
        {
            
            if (Input.GetKey(KeyCode.DownArrow) && onSkyGround) // 아랫점프
            {
                if (Input.GetButtonDown("Jump"))
                {
                    StartCoroutine(DownJump());
                    return;
                }
            }
            if (Input.GetButtonDown("Jump")) // 공중점프
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                isJump = true;
            }
        }
    }

    private IEnumerator DownJump()
    {
        col.enabled = false;

        //대기 후 콜라이더를 다시 활성화
        yield return new WaitForSeconds(downJumpTime);
        col.enabled = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SkyGround"))
        {
            Vector2 raycastOrigin = transform.position + Vector3.down * 0.50001f;
            RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, 0.1f); // 슈퍼 점프 방지. 스프라이트 아랫쪽에 발판이 있는 경우에만 if문 실행
            if (hit.collider != null)
            {
                isJump = false;
                onSkyGround = true;
            }
 
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SkyGround") // 발판에서 내려올 때
        {
            onSkyGround = false;
            isJump = true;
        }
    }
}

