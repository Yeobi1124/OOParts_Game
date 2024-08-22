using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    public enum Type { Small, Medium, Large }

    [Header("Info")]
    public string enemyName;
    public int enemyCode;

    [Header("stats")]
    public Type enemyType;
    public float dmg;
    public float hp;

    
}
