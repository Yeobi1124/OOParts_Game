using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    Dictionary<int, QuestData> questList;

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("firstQuest", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("SecondQuest", new int[] { 1000, 2000 }));
        questList.Add(30, new QuestData("QuestFinished", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public void CheckQuest(int id)
    {
        if(id == questList[questId].npcId[questActionIndex]) // 퀘스트에서 올바른 진행 순서의 npc인가를 체크
            questActionIndex++;
        if (questActionIndex == questList[questId].npcId.Length )
            NextQuest();
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }
}
