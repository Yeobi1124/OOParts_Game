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

    bool isJump; //�������ΰ�? 
    bool onSkyGround; //���߹����� ���ΰ�?

    Rigidbody2D rigid;
    Animator anim;
    Collider2D col;

    //PlatformScript skyGround; // skyGround �÷��� ��ũ��Ʈ ������ ���� �ʿ����
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //skyGround = FindObjectOfType<PlatformScript>(); ���� �ʿ����
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
            
            if (Input.GetKey(KeyCode.DownArrow) && onSkyGround) // �Ʒ�����
            {
                if (Input.GetButtonDown("Jump"))
                {
                    StartCoroutine(DownJump());
                    return;
                }
            }
            if (Input.GetButtonDown("Jump")) // ��������
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                isJump = true;
            }
        }
    }

    private IEnumerator DownJump()
    {
        col.enabled = false;

        //��� �� �ݶ��̴��� �ٽ� Ȱ��ȭ
        yield return new WaitForSeconds(downJumpTime);
        col.enabled = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SkyGround"))
        {
            Vector2 raycastOrigin = transform.position + Vector3.down * 0.50001f;
            RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, 0.1f); // ���� ���� ����. ��������Ʈ �Ʒ��ʿ� ������ �ִ� ��쿡�� if�� ����
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
        if (collision.gameObject.tag == "SkyGround") // ���ǿ��� ������ ��
        {
            onSkyGround = false;
            isJump = true;
        }
    }
}

