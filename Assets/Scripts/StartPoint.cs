using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;
    void Start()
    {
        GameManager.Instance.player.transform.position = this.transform.position;
        GameManager.Instance.mainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, GameManager.Instance.mainCamera.transform.position.z);

    }
}
