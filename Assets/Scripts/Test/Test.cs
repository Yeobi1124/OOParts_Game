using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public ItemData[] items;

    [SerializeField] NPCManager abel;


    public Collider2D animEncounter;
    public GameObject point;
   
    private void Start()
    {
        //���� ��Ȱ��ȭ
        abel.gameObject.SetActive(false);
        //�κ��丮 ������ �߰�
        GameManager.Instance.inventoryManager.Add(items[1]);
        GameManager.Instance.inventoryManager.Add(items[2]);
        if(GameManager.Instance.questManager.questId == 30 || GameManager.Instance.questManager.questId == 40)
        {
            Debug.Log("1");
            abel.gameObject.SetActive(true);
            abel.gameObject.transform.Translate(2f, -6f, 0);
            abel.anim.SetFloat("DirY", 0);
            abel.anim.SetFloat("DirX", -1);
        }
    }

    private void LateUpdate()
    {
        if(GameManager.Instance.questManager.questId == 20 && !GameManager.Instance.exchangeManager.apple)
        {
            GameManager.Instance.inventoryManager.Add(items[0]);
            GameManager.Instance.exchangeManager.apple = true;
        }
        if (GameManager.Instance.questManager.questId == 40 && !GameManager.Instance.exchangeManager.boss)
        {
            GameManager.Instance.encounterManager.EnterCombat("Abel");
            GameManager.Instance.exchangeManager.boss = true;
        }
    }

    public void AnimEvent()
    {
        if (!GameManager.Instance.exchangeManager.anim)
        {
            GameManager.Instance.exchangeManager.anim = true;
            StartCoroutine(AnimCoroutine());
        }
    }

    IEnumerator AnimCoroutine()
    {
        //���̵��� & �ƿ� ����
        GameManager.Instance.fadeManager.FadeOut();
        yield return new WaitForSeconds(GameManager.Instance.fadeManager.fadeDuration);
        GameManager.Instance.player.transform.position = point.transform.position;//�÷��̾� �̵�
        abel.gameObject.SetActive(true);//������Ʈ Ȱ��ȭ
        abel.anim.SetFloat("DirY", -1f);
        abel.anim.SetFloat("DirX", 1f);
        yield return new WaitForSeconds(GameManager.Instance.fadeManager.fadeDuration);
        GameManager.Instance.fadeManager.FadeIn();
        
        //�ִϸ��̼� ���� (npc�̵� & ��ȭ)
        GameManager.Instance.player.canMove = false;
        for(int i =0; i <4; i++)
            GameManager.Instance.orderManager.Move("�ƺ�", "Down");
        yield return new WaitForSeconds(1f);
        for (int i =0; i <4; i++)
        {
            GameManager.Instance.dialogueManager.Action(abel.gameObject);
            yield return new WaitForSeconds(1f);
        }
        GameManager.Instance.orderManager.Move("�ƺ�", "Right");
        GameManager.Instance.orderManager.Move("�ƺ�", "Right");
        GameManager.Instance.orderManager.Move("�ƺ�", "Down");
        GameManager.Instance.orderManager.Move("�ƺ�", "Down");
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 4; i++)
        {
            GameManager.Instance.dialogueManager.Action(abel.gameObject);
            yield return new WaitForSeconds(1f);
        }
        abel.anim.SetFloat("DirY", 0);
        abel.anim.SetFloat("DirX", -1);
        for (int i = 0; i < 4; i++)
        {
            GameManager.Instance.dialogueManager.Action(abel.gameObject);
            yield return new WaitForSeconds(1f);
        }

        //���̵��� & �ƿ� ����
        GameManager.Instance.fadeManager.FadeOut();
        yield return new WaitForSeconds(GameManager.Instance.fadeManager.fadeDuration);
        GameManager.Instance.player.transform.position = point.transform.position;
        yield return new WaitForSeconds(GameManager.Instance.fadeManager.fadeDuration);
        GameManager.Instance.fadeManager.FadeIn();

        GameManager.Instance.player.canMove = true;
    }
}
