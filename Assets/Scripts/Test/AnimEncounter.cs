using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEncounter : MonoBehaviour
{
    Test test;

    private void Start()
    {
        test = FindObjectOfType<Test>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player"
            && GameManager.Instance.questManager.questId == 20)
        {
            test.AnimEvent();
        }
    }
}
