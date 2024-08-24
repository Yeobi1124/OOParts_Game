using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;


public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public TextMeshProUGUI currentQuest;
    Dictionary<int, QuestData> questList;

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }
    private void LateUpdate()
    {
        currentQuest.text = string.Format("현재 진행중인 퀘스트 : {0}",questList[questId].questName);
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("먹을 것 구하기 \n오두막에서 체력을 채울 것을 찾아보자", new int[] { 8000 }));
        questList.Add(20, new QuestData("여정 \n평원을 지나 마을로 가자.", new int[] { 1000, 1000, 1000}));
        questList.Add(30, new QuestData("결전 \n아벨을 쓰러뜨리자", new int[] { 1000 }));
        questList.Add(40, new QuestData("끝", new int[] { 0 }));
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
