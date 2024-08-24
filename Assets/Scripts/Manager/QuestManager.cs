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
        currentQuest.text = string.Format("���� �������� ����Ʈ : {0}",questList[questId].questName);
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("���� �� ���ϱ� \n���θ����� ü���� ä�� ���� ã�ƺ���", new int[] { 8000 }));
        questList.Add(20, new QuestData("���� \n����� ���� ������ ����.", new int[] { 1000, 1000, 1000}));
        questList.Add(30, new QuestData("���� \n�ƺ��� �����߸���", new int[] { 1000 }));
        questList.Add(40, new QuestData("��", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public void CheckQuest(int id)
    {
        if(id == questList[questId].npcId[questActionIndex]) // ����Ʈ���� �ùٸ� ���� ������ npc�ΰ��� üũ
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
