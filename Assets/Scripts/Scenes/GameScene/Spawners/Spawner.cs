using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [Header("Area")]
    [SerializeField] protected Area _area;
    [SerializeField] protected float _margin;

    [Header ("Repeat")]
    [SerializeField] private float _repeatTime = 27f;
    public float RepeatTime
    {
        get => _repeatTime;
        set
        {
            _repeatTime = value;
            ChangedRepeatTime();
        }
    }

    [SerializeField] private float _timeBeforeStart = 0f;
    [SerializeField] private float _timeUntilDelete = 5f;

    [SerializeField] protected List<SpawnObject> _spawnObjects;

    protected Camera _camera;

    protected Coroutine _repeatCorutine;

    protected virtual void Awake()
    {
        _camera = Camera.main;

        InitPools();
    }

    protected virtual void Start()
    {
        _repeatCorutine = CoroutineManager.s_Instance.StartCoroutine(RepeatSpawn());
    }

    #region -PoolObject-
    protected virtual void InitPools()
    {
        foreach (var spawnObject in _spawnObjects)
        {
            spawnObject.pool = new(
                delegate { return Preload(spawnObject.prefab); },
                GetAction, ReturnAction, Constants.Spawner.PRELOADCOUNT);
        }
    }

    protected GameObject Preload(GameObject prefab) => Instantiate(prefab, transform);
    protected void GetAction(GameObject gameObject) => gameObject.SetActive(true);
    protected void ReturnAction(GameObject gameObject) => gameObject.SetActive(false);
    #endregion

    private IEnumerator RepeatSpawn()
    {
        yield return new WaitForSeconds(_timeBeforeStart);

        while (true)
        {
            SpawnObject();
            yield return new WaitForSeconds(_repeatTime);
        }
    }

    private void ChangedRepeatTime()
    {
        CoroutineManager.s_Instance.StopCoroutine(_repeatCorutine);
        _repeatCorutine = CoroutineManager.s_Instance.StartCoroutine(RepeatSpawn());
    }

    protected IEnumerator DeleteObject(SpawnObject spawnObject, GameObject gameObject)
    {
        yield return new WaitForSeconds(_timeUntilDelete);
        spawnObject.pool.Return(gameObject);
    }

    protected abstract void SpawnObject();

    protected abstract SpawnObject GetObject();

    protected SpawnObject GetProbElement(List<SpawnObject> spawnObjects)
    {
        var total = 0f;
        foreach (var el in spawnObjects)
        {
            total += el.probability;
        }

        var random = Random.value * total;

        var current = 0f;
        foreach (var el in spawnObjects)
        {
            if (current <= random && random < current + el.probability)
            {
                return el;
            }
            current += el.probability;
        }

        return null;
    }
}
