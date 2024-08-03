using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setmove : MonoBehaviour
{
    public NPCManager[] npcManagers;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetMoveCoroutine());
    }

    IEnumerator SetMoveCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < npcManagers.Length; i++)
        {
            npcManagers[i].SetMove();
        }
    }

}
