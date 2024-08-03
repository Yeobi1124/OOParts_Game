using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private PlayerManager thePlayer; // 이벤트 도중에 키입력 처리 방지
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
        MovingObject[] temp = FindObjectsOfType<MovingObject>();// 이 메서드를 쓰기위해 리스트로 씀

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

//리스트를 활용하여 네임 또는 방향 등의 패러미터를 받는 함수를 만들어 npc기능들을 자유롭게 구현가능