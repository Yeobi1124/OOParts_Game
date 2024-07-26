using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public string characterName;
    public BoxCollider2D boxCollider;
    public LayerMask layerMask; // 충돌한 콜라이더가 무슨 레이어 인지 return 

    public float speed;
    protected Vector3 vector;
    protected Vector3 dirVec;

    public int walkCount;
    protected int currentWalkCount;
    private bool notCoroutine = false;

    public Animator anim;

    public Queue<string> queue;

    public void Move(string dir, int frequency = 5)
    {
        queue.Enqueue(dir);
        if (!notCoroutine)
        {
            notCoroutine = true;
            StartCoroutine(MoveCoroutine(dir, frequency));
        }

    }

    IEnumerator MoveCoroutine(string dir, int frequency)
    {
        while (queue.Count != 0)
        {
            switch (frequency)
            {
                case 1:
                    yield return new WaitForSeconds(4f);
                    break;
                case 2:
                    yield return new WaitForSeconds(3f);
                    break;
                case 3:
                    yield return new WaitForSeconds(2f);
                    break;
                case 4:
                    yield return new WaitForSeconds(1f);
                    break;
                case 5:
                    break;
            }

            string direction = queue.Dequeue();
            vector.Set(0, 0, vector.z);
            switch (direction)
            {
                case "Up":
                    vector.y = 1f;
                    break;
                case "Down":
                    vector.y = -1f;
                    break;
                case "Right":
                    vector.x = 1f;
                    break;
                case "Left":
                    vector.x = -1f;
                    break;
            }

            anim.SetFloat("DirX", vector.x);
            anim.SetFloat("DirY", vector.y);
            while (true)
            {
                bool checkCollisionFlag = CheckCollision();
                if (checkCollisionFlag)
                {
                    anim.SetBool("Walking", false);
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    break;
                }
            }


            anim.SetBool("Walking", true);

            boxCollider.offset = new Vector2(vector.x = 0.7f * speed * walkCount, vector.y * 0.7f * speed * walkCount);
            while (currentWalkCount < walkCount)
            {

                transform.Translate(vector.x * speed, vector.y * speed, 0);
                currentWalkCount++;
                if (currentWalkCount == 12)
                    boxCollider.offset = Vector2.zero;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
            if (frequency != 5)
                anim.SetBool("Walking", false);
        }
        anim.SetBool("Walking", false);
        notCoroutine = false;

    }

    protected bool CheckCollision()
    {
        RaycastHit2D hit;

        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);

        boxCollider.enabled = false; // 자기 자신은 충돌 활성화 끄기
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true;

        if (hit.transform != null)
            return true;
        return false;
    }

}