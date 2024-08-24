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
        talkData.Add(1000, new string[] { "현재까지 완성된 건 여기까지 입니다.", "플레이 해주셔서 감사합니다." });
        talkData.Add(2000, new string[] { "" });
        //signPosts
        talkData.Add(3000, new string[] { "← : 악령의 숲(미구현)\n↑ : 외곽 오두막\n→ : 끝없는 설원(위험)" });
        talkData.Add(4000, new string[] { "↑ : 오두막\n↓ : 세 갈래 길" });
        talkData.Add(5000, new string[] { "← : 세 갈래 길\n→ : 마을로 향하는 길(미구현)" });
        //Objects
        talkData.Add(6000, new string[] { "\"순수하던 때로 돌아가고파\"", "라고 적혀져 있다." });
        talkData.Add(7000, new string[] { "포근해 보이는 침대다." });
        talkData.Add(8000, new string[] { "평범해 보이는 사과 바구니다." });
        talkData.Add(9000, new string[] { "평범해 보이는 올리브 바구니다." });
        talkData.Add(10000, new string[] { "욕조물이 차갑게 식어있다." });

        //questId
        //10
        talkData.Add(8010, new string[] { "사과 바구니 안에서 특별해 보이는 사과를 꺼냈다.", "청사과를 얻었다."});
        //20
        talkData.Add(1020, new string[] { "대사1", "대사2", "대사3" });
        talkData.Add(1021, new string[] { "대사4", "대사5", "대사6" });
        talkData.Add(1022, new string[] { "대사7", "대사8", "대사9" });
        //30
        talkData.Add(1030, new string[] { "대사10" });

        
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
