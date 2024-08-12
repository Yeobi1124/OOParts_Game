using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPoolManager : MonoBehaviour
{
    [TextArea(1, 10)]
    public string notes;
    Dictionary<int, GameObject> prefabData;
    Dictionary<int, List<GameObject>> pools;

    private void Awake() {
        Init();
    }

    private void Init(){
        prefabData = new Dictionary<int, GameObject>();
        pools = new Dictionary<int, List<GameObject>>();
        
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs");

        foreach(GameObject prefab in prefabs){
            int id = prefab.gameObject.GetComponent<PoolObject>().id;
            prefabData.Add(id, prefab);
            pools.Add(id, new List<GameObject>());
        }
    }

    /// <summary>
    /// prefab의 id 기반으로 투사체를 생성/활성화 시킴.
    /// </summary>
    /// <param name="id">prefab id</param>
    /// <returns>호출한 prefab의 GameObject</returns>
    public GameObject Make(int id, Vector2 pos)
    {
        GameObject bullet = null;

        // 선택한 풀의 놀고 있는 (비활성화 된) 게임오브젝트 접근
        foreach(GameObject prefab in pools[id]){
            if(!prefab.activeSelf){
                // 발견하면 select 변수에 할당
                bullet = prefab;
                bullet.SetActive(true);
                break;
            }
        }

        // 못찾으면
        if(!bullet){
            //새롭게 생성하고 bullet 변수에 할당. Instantiate : 해당 오브젝트 Scene에 복제, 옆에 transform으로 자신에게 상속도 시킴.
            bullet = Instantiate(prefabData[id], transform);
            pools[id].Add(bullet);
        }

        bullet.transform.position = pos;

        return bullet;
    }
}
