using System;
using UnityEngine;

[Serializable]
public class SpawnObject
{
    public GameObject prefab;
    public float probability;

    [NonSerialized] public PoolBase<GameObject> pool;
}