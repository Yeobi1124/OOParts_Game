using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCMove
{
    public bool CanMove; // npc 움직임의 여부

    public string[] direction; // npc가 움직일 방향

    [Range(1, 5)]
    [Tooltip("5로 갈 수록 더 빨라짐")]
    public int frequency; // 한칸 이동하는데 걸리는 시간(빈도)

}

public class NPCManager : MovingObject
{
    [SerializeField]
    public NPCMove npc;

    private void Start()
    {
        queue = new Queue<string>();
        base.anim.SetFloat("DirY", -1f);
        base.anim.SetFloat("DirX", 1f);
    }

    public void SetMove()
    {
        StartCoroutine(MoveCoroutine());
        npc.CanMove = true;
    }

    public void SetNotMove()
    {
        StopAllCoroutines();
        npc.CanMove = false;
    }

    IEnumerator MoveCoroutine()
    {
        if (npc.direction.Length != 0)
        {
            for (int i = 0; i < npc.direction.Length; i++)
            {

                yield return new WaitUntil(() => queue.Count < 2); // **큐가 2 이상이면 빠질때까지 대기함
                base.Move(npc.direction[i], npc.frequency);

                if (i == npc.direction.Length - 1)
                {
                    i = -1;
                }
            }

        }
    }
}
