using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{
    public static PlayerManager Instance;

    public float runSpeed;
    private float applyRunSpeed;

    private bool applyRunFlag = false;
    public GameObject scanObject;

    public bool canMove = true;

    public DialogueManager dialogueManager;

    private void Start()
    {
        queue = new Queue<string>();
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            anim = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

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
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, dirVec, 0.7f, LayerMask.GetMask("NoPassing"));
            if (rayHit.collider != null)
            {
                scanObject = rayHit.collider.gameObject;
            }
            else
                scanObject = null;
        }

        if (Input.GetButtonDown("Jump") && scanObject.GetComponent<ObjectData>() != null)
        {
            canMove = false;
            dialogueManager.Action(scanObject);
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
        }

        anim.SetBool("Walking", false);
        canMove = true;
    }
}

