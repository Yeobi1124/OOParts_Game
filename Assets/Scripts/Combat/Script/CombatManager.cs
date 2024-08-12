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
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
            instance = this;
    }
}
