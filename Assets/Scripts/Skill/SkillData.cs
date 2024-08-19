using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "skillData")]
public class SkillData : ScriptableObject
{
    public enum TypeInfo { Passvie, CoolDown}

    [Header("필수 정보")]
    public int id;
    TypeInfo skillType;
    [SerializeField] int level; // 레벨이 존재하지 않는 스킬이면, -1.

    [Header("조건적 정보")]
    float coolDown;

    public float GetCoolDown()
    {
        if(skillType == TypeInfo.CoolDown)
        {
            return coolDown;
        }
        else
        {
            return -1;
        }
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetCoolDown(float reduction)
    {
        coolDown *= reduction;
    }

    public void LevelUp()
    {
        level++;
    }
}
