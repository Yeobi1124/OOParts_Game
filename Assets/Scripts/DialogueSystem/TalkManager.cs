using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    public Dictionary<int, string> nameData;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        nameData = new Dictionary<int, string>();
        NPCManager[] npcData = FindObjectsOfType<NPCManager>();
        for(int i =0; i <npcData.Length; i++)
        {
            if (npcData[i].GetComponent<ObjectData>() == null)
                continue;
            else
            {
                ObjectData objData = npcData[i].GetComponent<ObjectData>();
                if (objData.isNpc)
                {
                    nameData.Add(objData.id, npcData[i].characterName);
                }
                else
                {
                    continue;
                }
            }
        }
        GenerateData();
    }

    void GenerateData()
    {
        //Npcs
        talkData.Add(1000, new string[] { "������� �ϼ��� �� ������� �Դϴ�.", "�÷��� ���ּż� �����մϴ�." });
        talkData.Add(2000, new string[] { "" });
        //signPosts
        talkData.Add(3000, new string[] { "�� : �Ƿ��� ��(�̱���)\n�� : �ܰ� ���θ�\n�� : ������ ����(����)" });
        talkData.Add(4000, new string[] { "�� : ���θ�\n�� : �� ���� ��" });
        talkData.Add(5000, new string[] { "�� : �� ���� ��\n�� : ������ ���ϴ� ��(�̱���)" });
        //Objects
        talkData.Add(6000, new string[] { "\"�����ϴ� ���� ���ư�����\"", "��� ������ �ִ�." });
        talkData.Add(7000, new string[] { "������ ���̴� ħ���." });
        talkData.Add(8000, new string[] { "����� ���̴� ��� �ٱ��ϴ�." });
        talkData.Add(9000, new string[] { "����� ���̴� �ø��� �ٱ��ϴ�." });
        talkData.Add(10000, new string[] { "�������� ������ �ľ��ִ�." });

        //questId
        //10
        talkData.Add(8010, new string[] { "��� �ٱ��� �ȿ��� Ư���� ���̴� ����� ���´�.", "û����� �����."});
        //20
        talkData.Add(1020, new string[] { "���1", "���2", "���3" });
        talkData.Add(1021, new string[] { "���4", "���5", "���6" });
        talkData.Add(1022, new string[] { "���7", "���8", "���9" });
        //30
        talkData.Add(1030, new string[] { "���10" });

        
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if(!talkData.ContainsKey(id - id % 10))
            {
                return GetTalk(id - id % 100, talkIndex);
            }
            else
                return GetTalk(id-id%10, talkIndex);
        }
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
            return talkData[id][talkIndex];
    }
}
