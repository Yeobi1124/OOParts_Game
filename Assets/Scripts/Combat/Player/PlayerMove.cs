using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float onCastingSpeed; // isCasting 상태에서 최대 이동속도
    public float jumpPower;
    public float downJumpTime;
    public PlayerStatus player;

    public bool isJump; //점프중인가? 
    bool onSkyGround; //공중발판의 위인가?

    Rigidbody2D rigid;
    public Animator anim;
    SpriteRenderer spriteRenderer;

    //PlatformScript skyGround; // skyGround 플랫폼 스크립트 가져옴 현재 필요없음

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //skyGround = FindObjectOfType<PlatformScript>(); 현재 필요없음
        spriteRenderer = GetComponent<SpriteRenderer>();
        isJump = false;
    }

    private void Start()
    {
        player.col = GetComponent<Collider2D>();
    }
    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h == 0f)
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isCharging", false);
        }
        else
        {
            anim.SetBool("isRun", true);
            anim.SetBool("isCharging", false);
        }

        if (CombatManager.instance.skill.isCasting)
        {
            anim.SetBool("isCharging", true);
        }

        // 이동 속도 설정
        float moveSpeed = CombatManager.instance.skill.isCasting ? onCastingSpeed : maxSpeed; // 캐스팅 중일 경우 캐스팅 속도, 아니면 기본 속도
        //rigid.velocity = new Vector2(h * moveSpeed, rigid.velocity.y);
        transform.Translate(new Vector2(h * moveSpeed * Time.fixedDeltaTime, 0));

        // 캐릭터 방향 설정
        if (h > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (h < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void Update()
    {
        if (!isJump)
        {
            
            if (Input.GetKey(KeyCode.DownArrow) && onSkyGround) // 아랫점프
            {
                if (Input.GetButtonDown("Jump"))
                {
                    anim.SetBool("isJump", true);
                    StartCoroutine(DownJump());
                    return;
                }
            }
            if (Input.GetButtonDown("Jump")) // 공중점프
            {
                anim.SetBool("isJump", true);
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                isJump = true;
            }
        }
    }

    private IEnumerator DownJump()
    {
        player.col.enabled = false;
        //대기 후 콜라이더를 다시 활성화
        yield return new WaitForSeconds(downJumpTime);
        player.col.enabled = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SkyGround"))
        {
            Vector2 raycastOrigin = new Vector2(transform.position.x, transform.position.y - player.col.bounds.extents.y - 0.00001f);
            RaycastHit2D[] hitArray = Physics2D.RaycastAll(raycastOrigin, Vector2.down, 0.1f);
            // 슈퍼 점프 방지. 스프라이트 아랫쪽에 발판이 있는 경우에만 if문 실행

            foreach(RaycastHit2D hit in hitArray)
            {
                if (hit.collider != null && hit.collider.CompareTag("SkyGround"))
                {
                    anim.SetBool("isJump", false);
                    isJump = false;
                    onSkyGround = true;
                }
            }
            
 
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJump", false);
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

