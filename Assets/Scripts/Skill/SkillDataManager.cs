using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SkillDataManager : MonoBehaviour
{
    public List<SkillData> data;
    public Dictionary<int, SkillData> SkillDataDict;

    private void Awake()
    {
        SkillDataDict = new Dictionary<int, SkillData>();
    }

    public void Upgrade(int key)
    {
        if(SkillDataDict.ContainsKey(key))
        {
            SkillData skill = SkillDataDict[key];
            if(skill.GetLevel() == -1)
            {
                return;
            }
            else
            {
                skill.LevelUp();
            }
        }
        else
        {
            for(int i =0; i < data.Count; i++)
            {
                if (data[i].id == key)
                {
                    SkillDataDict.Add(key, data[i]);
                }
            }
        }
    }
}
