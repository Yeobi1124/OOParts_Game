using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCMove
{
    public bool CanMove; // npc �������� ����

    public string[] direction; // npc�� ������ ����

    [Range(1, 5)]
    [Tooltip("5�� �� ���� �� ������")]
    public int frequency; // ��ĭ �̵��ϴµ� �ɸ��� �ð�(��)

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

                yield return new WaitUntil(() => queue.Count < 2); // **ť�� 2 �̻��̸� ���������� �����
                base.Move(npc.direction[i], npc.frequency);

                if (i == npc.direction.Length - 1)
                {
                    i = -1;
                }
            }

        }
    }
}
