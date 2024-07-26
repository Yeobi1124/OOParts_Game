using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private PlayerManager thePlayer; // �̺�Ʈ ���߿� Ű�Է� ó�� ����
    private List<MovingObject> characters;

    private void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    public void PreLoadCharacter()
    {
        characters = ToList();
    }

    public List<MovingObject> ToList()
    {
        List<MovingObject> tempList = new List<MovingObject>();
        MovingObject[] temp = FindObjectsOfType<MovingObject>();// �� �޼��带 �������� ����Ʈ�� ��

        for (int i = 0; i < temp.Length; i++)
        {
            tempList.Add(temp[i]);
        }

        return tempList;
    }

    public void Move(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].Move(_dir);
            }
        }
    }

    public void Turn(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].anim.SetFloat("DirY", 0f);
                characters[i].anim.SetFloat("DirX", 0f);
                switch (_dir)
                {

                    case "Up":
                        characters[i].anim.SetFloat("DirY", 1f);
                        break;
                    case "Down":
                        characters[i].anim.SetFloat("DirY", -1f);
                        break;
                    case "Left":
                        characters[i].anim.SetFloat("DirX", -1f);
                        break;
                    case "Right":
                        characters[i].anim.SetFloat("DirX", 1f);
                        break;
                }
            }
        }
    }

}

//����Ʈ�� Ȱ���Ͽ� ���� �Ǵ� ���� ���� �з����͸� �޴� �Լ��� ����� npc��ɵ��� �����Ӱ� ��������