using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> TalkData;

    private void Awake()
    {
        TalkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        TalkData.Add(1000, new string[] { "Hello", "World!" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == TalkData[id].Length)
        {
            return null;
        }
        else
            return TalkData[id][talkIndex];
    }
}
