using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "skillData")]
public class SkillData : ScriptableObject
{
    public enum TypeInfo { Passvie, CoolDown}

    [Header("�ʼ� ����")]
    public int id;
    TypeInfo skillType;
    [SerializeField] int level; // ������ �������� �ʴ� ��ų�̸�, -1.

    [Header("������ ����")]
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
