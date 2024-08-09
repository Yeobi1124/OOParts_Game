using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

/*
Player 호출 개선 - public으로 불러서 넣는 건 구조상 안 좋아 보임.

- player 불러오는 부분 GameManager instance 등으로 개선


스킬 범위 표시 개선 - float형 변수로 거리 계산은 가시성이 떨어짐. 플레이어, 개발자 모두에게

- 스킬 범위를 float형 변수가 아닌, 자식 오브젝트로 Circle을 두고, OnPointerDown 등의 이벤트 트리거로 처리하여 개선
*/

public class Laser : MonoBehaviour
{
    public GameObject laser;
    public GameObject player;
    public float range; 

    bool isDragging;
    Camera cam;
    Vector2 castPos;
    GameObject beam;
    GameObject magicCircle;

    private void Awake() {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        beam = laser.transform.GetChild(0).GameObject();
        magicCircle = laser.transform.GetChild(1).GameObject();
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            Debug.Log("Button Down");
        }

        if(Input.GetMouseButtonDown(0) && Vector2.Distance(player.transform.position, cam.ScreenToWorldPoint(Input.mousePosition)) < range){
            Debug.Log("Skill Cast");
            isDragging = true;

            Cast();
        }

        if(Input.GetMouseButton(0) && isDragging){}

        if(Input.GetMouseButtonUp(0) && isDragging){
            isDragging = false;

            StartCoroutine(Shoot());
        }

    }

    private void Cast(){
        castPos = cam.ScreenToWorldPoint(Input.mousePosition);

        magicCircle.SetActive(true);
        magicCircle.transform.position = castPos;
    }

    IEnumerator Shoot(){
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        beam.SetActive(true);
        beam.transform.position = castPos;
        beam.transform.rotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector3.up, mousePos - castPos).eulerAngles.z + 90f); //빔 각도 설정
        beam.transform.Translate(beam.transform.right * beam.transform.localScale.x/2, Space.World);

        yield return new WaitForSeconds(0.5f);

        beam.SetActive(false);
        magicCircle.SetActive(false);
    }
}
