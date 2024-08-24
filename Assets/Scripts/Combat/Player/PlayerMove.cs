using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float onCastingSpeed; // isCasting ���¿��� �ִ� �̵��ӵ�
    public float jumpPower;
    public float downJumpTime;
    public PlayerStatus player;

    public bool isJump; //�������ΰ�? 
    bool onSkyGround; //���߹����� ���ΰ�?

    Rigidbody2D rigid;
    public Animator anim;
    SpriteRenderer spriteRenderer;

    //PlatformScript skyGround; // skyGround �÷��� ��ũ��Ʈ ������ ���� �ʿ����

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //skyGround = FindObjectOfType<PlatformScript>(); ���� �ʿ����
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

        // �̵� �ӵ� ����
        float moveSpeed = CombatManager.instance.skill.isCasting ? onCastingSpeed : maxSpeed; // ĳ���� ���� ��� ĳ���� �ӵ�, �ƴϸ� �⺻ �ӵ�
        //rigid.velocity = new Vector2(h * moveSpeed, rigid.velocity.y);
        transform.Translate(new Vector2(h * moveSpeed * Time.fixedDeltaTime, 0));

        // ĳ���� ���� ����
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
            
            if (Input.GetKey(KeyCode.DownArrow) && onSkyGround) // �Ʒ�����
            {
                if (Input.GetButtonDown("Jump"))
                {
                    anim.SetBool("isJump", true);
                    StartCoroutine(DownJump());
                    return;
                }
            }
            if (Input.GetButtonDown("Jump")) // ��������
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
        //��� �� �ݶ��̴��� �ٽ� Ȱ��ȭ
        yield return new WaitForSeconds(downJumpTime);
        player.col.enabled = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SkyGround"))
        {
            Vector2 raycastOrigin = new Vector2(transform.position.x, transform.position.y - player.col.bounds.extents.y - 0.00001f);
            RaycastHit2D[] hitArray = Physics2D.RaycastAll(raycastOrigin, Vector2.down, 0.1f);
            // ���� ���� ����. ��������Ʈ �Ʒ��ʿ� ������ �ִ� ��쿡�� if�� ����

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
        if (collision.gameObject.tag == "SkyGround") // ���ǿ��� ������ ��
        {
            onSkyGround = false;
            isJump = true;
        }
    }
}

