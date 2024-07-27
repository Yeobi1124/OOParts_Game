using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCMove
{
    public bool NPCmove; // npc �������� ����

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

        StartCoroutine(MoveCoroutine());
    }

    public void SetMove()
    {
        StartCoroutine(MoveCoroutine());
    }

    public void SetNotMove()
    {
        StopAllCoroutines();
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
