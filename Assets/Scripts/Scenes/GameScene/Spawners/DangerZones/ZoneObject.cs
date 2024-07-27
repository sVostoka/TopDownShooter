using System;
using UnityEngine;

[Serializable]
public class ZoneObject
{
    public GameObject prefab;
    public int count;

    public float GetRadius()
    {
        return prefab.transform.localScale.x / 2;
    }
}