using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    public Camera cam;
    public SkillManager skill;
    public GameObject player;
    public CombatPoolManager pool;
    public Enemyy target;
    public Enemyy enemies;
    void Awake()
    {
        if(instance == null)
            instance = this;
    }
}
