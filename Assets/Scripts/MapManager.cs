using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public StartPoint[] points;

    private void Start()
    {
        points = GetComponentsInChildren<StartPoint>();
    }
}
