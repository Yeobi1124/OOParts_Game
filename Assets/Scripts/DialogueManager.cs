using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    public GameObject namePanel;

    public TypeEffect talk;
    public Text nameText;
    public GameObject menuSet;

    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;

    private void Start()
    {
        talkPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            if (menuSet.activeSelf)
                menuSet.SetActive(false);
            else
                menuSet.SetActive(true);
        }
    }
    public void Action(GameObject scanObj)
    {
        isAction = true;
        scanObject = scanObj;
        ObjectData objectData = scanObject.GetComponent<ObjectData>();
        Talk(objectData.id, objectData.isNpc);
        talkPanel.SetActive(isAction);
    }

    void Talk(int npcId, bool isNpc)
    {
        GameManager.Instance.player.canMove = false;
        int QuestTalkIndex = 0;
        string talkData = "";
        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            QuestTalkIndex = GameManager.Instance.questManager.GetQuestTalkIndex(npcId);
            talkData = talkManager.GetTalk(npcId + QuestTalkIndex, talkIndex);

        }

        if (talkData == null)
        {
            isAction = false;
            GameManager.Instance.player.canMove = true;
            talkIndex = 0;
            GameManager.Instance.questManager.CheckQuest(npcId);
            return;
        }
        if (isNpc)
        {
            talk.SetMsg(talkData);
            namePanel.SetActive(true);
            nameText.text = talkManager.nameData[npcId];
        }
        else
        {
            talk.SetMsg(talkData);
            namePanel.SetActive(false);
        }

        isAction = true;
        talkIndex++;
    }
}


