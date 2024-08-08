using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MovingObject
{

    public float runSpeed;
    private float applyRunSpeed;

    private bool applyRunFlag = false;
    public GameObject scanObject;

    public bool canMove = true;

    public DialogueManager dialogueManager;

    private void Start()
    {
        queue = new Queue<string>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        base.anim.SetFloat("DirY", -1f);
        base.anim.SetFloat("DirX", 1f);
        dirVec = Vector3.down;
        

    }
    private void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
        if (dirVec != null)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, dirVec, 0.7f, LayerMask.GetMask("NoPassing"));
            
                if (hit.Length > 1)
                {
                    scanObject = hit[1].collider.gameObject;
                }
                else
                {
                    scanObject=null;
                }
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (scanObject != null)
            {
                ObjectData objectData = scanObject.GetComponent<ObjectData>();
                if (objectData != null && scanObject.GetComponent<NPCManager>() != null)
                {
                    if (!scanObject.GetComponent<NPCManager>().npc.CanMove)
                    {
                        canMove = false;
                        dialogueManager.Action(scanObject);
                    }
                }
            }
        }

    }

    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            anim.SetBool("Walking", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
                vector.y = 0;

            anim.SetFloat("DirX", vector.x);
            anim.SetFloat("DirY", vector.y);

            if (vector.x == 1)
                dirVec = Vector3.right;
            else if (vector.x == -1)
                dirVec = Vector3.left;
            else if (vector.y == 1)
                dirVec = Vector3.up;
            else
                dirVec = Vector3.down;

            bool checkCollisionFlag = base.CheckCollision();
            if (checkCollisionFlag)
            {
                break;
            }

            boxCollider.offset = new Vector2(vector.x * 0.7f * speed * walkCount, vector.y * 0.7f * speed * walkCount);

            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }

                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag)
                    currentWalkCount++;
                currentWalkCount++;
                if (currentWalkCount == 12)
                    boxCollider.offset = Vector2.zero;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
            GameManager.Instance.encounterManager.Encounter();
        }

        anim.SetBool("Walking", false);
        canMove = true;
    }

}

