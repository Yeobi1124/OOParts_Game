using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    PlatformEffector2D platform;

    private void Awake()
    {
        platform = GetComponent<PlatformEffector2D>();
    }

    public void Rotate(float angle)
    {
        platform.rotationalOffset = angle;
    }
}