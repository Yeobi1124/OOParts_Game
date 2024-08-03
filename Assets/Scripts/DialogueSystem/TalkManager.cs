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
        talkData.Add(1000, new string[] { "Hello", "World!" });
        talkData.Add(2000, new string[] { "Hello", "World!" });

        talkData.Add(1010, new string[] { "�Ʒ� ģ������ �λ縦 �ϰ� ��." });
        talkData.Add(2011, new string[] { "ģ��", "�ȳ�" });

        talkData.Add(1020, new string[] { "���ϰ� �Ծ�.", "���� �Ʒ� ģ������ �� �� �� ���� �ɸ� ����Ʈ�� ���� �ž�" });
        talkData.Add(2021, new string[] { "��� ����Ʈ�� �Ϸ�Ǿ����ϴ�." });

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
