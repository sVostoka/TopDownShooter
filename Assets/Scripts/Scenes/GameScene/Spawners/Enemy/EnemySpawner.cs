using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class EnemySpawner : Spawner
{
    [Header("Area")]
    [SerializeField] private Area _area;
    [SerializeField] private float _margin;

    [Header ("Decline Time")]
    [SerializeField] private float _minRepeatTime;
    [SerializeField] private float _declineTime;
    [SerializeField] private float _declineValue;

    private Coroutine _declineCorutine;

    protected override void Awake()
    {
        base.Awake();

        foreach (var spawnObject in _spawnObjects)
        {
            spawnObject.pool.AttachLinkAll(InitEnemies);
        }
    }

    protected override void Start()
    {
        base.Start();

        _declineCorutine = CoroutineManager.s_Instance.StartCoroutine(DeclineRepeatTime());
    }

    protected override void InitPools()
    {
        foreach (var spawnObject in _spawnObjects)
        {
            spawnObject.pool = new(
                delegate { return Preload(spawnObject.prefab); },
                GetAction, ReturnAction, Constants.Spawner.PRELOADCOUNT,
                delegate { return Add(spawnObject.prefab, ref spawnObject.pool); });
        }
    }

    private GameObject Add(GameObject prefab, ref PoolBase<GameObject> pool)
    {
        GameObject enemy = Instantiate(prefab, transform);
        enemy.GetComponent<Enemy>().Init(ref pool);
        return enemy;
    }

    private void InitEnemies(GameObject enemy, PoolBase<GameObject> pool)
    {
        enemy.GetComponent<Enemy>().Init(ref pool);
    }

    protected override void SpawnObject()
    {
        SpawnObject spawnObject = GetObject();
        if(spawnObject != null)
        {
            GameObject gameObject = spawnObject.pool.GetElement();

            Vector3 cameraBottomLeft = _camera.ScreenToWorldPoint(Vector2.zero);
            Vector3 cameraTopRight = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            var (areaTopRight, areaBottomLeft) = _area.GetMaxMin();

            List<Side> sides = new List<Side>() { Side.Top, Side.Right, Side.Bottom, Side.Left };

            if (areaBottomLeft.x + _margin >= cameraBottomLeft.x - _margin) sides.Remove(Side.Left);
            if (areaTopRight.y - _margin <= cameraTopRight.y + _margin) sides.Remove(Side.Top);
            if (areaTopRight.x - _margin <= cameraTopRight.x + _margin) sides.Remove(Side.Right);
            if (areaBottomLeft.y + _margin >= cameraBottomLeft.y - _margin) sides.Remove(Side.Bottom);

            Vector2 position = new();

            switch (sides[Random.Range(0, sides.Count)])
            {
                case Side.Top:
                    position.x = Random.Range(areaBottomLeft.x + _margin, areaTopRight.x - _margin);
                    position.y = Random.Range(cameraTopRight.y + _margin, areaTopRight.y - _margin);
                    break;
                case Side.Right:
                    position.x = Random.Range(cameraTopRight.x + _margin, areaTopRight.x - _margin);
                    position.y = Random.Range(areaBottomLeft.y + _margin, areaTopRight.y - _margin);
                    break;
                case Side.Bottom:
                    position.x = Random.Range(areaBottomLeft.x + _margin, areaTopRight.x - _margin);
                    position.y = Random.Range(areaBottomLeft.y + _margin, cameraBottomLeft.y - _margin);
                    break;
                case Side.Left:
                    position.x = Random.Range(areaBottomLeft.x + _margin, cameraBottomLeft.x - _margin);
                    position.y = Random.Range(areaBottomLeft.y + _margin, areaTopRight.y - _margin);
                    break;
            }

            gameObject.transform.position = position;

            StartCoroutine(DeleteObject(spawnObject, gameObject));
        }
    }

    protected override SpawnObject GetObject()
    {
        return GetProbElement(_spawnObjects);
    }

    private IEnumerator DeclineRepeatTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(_declineTime);

            RepeatTime -= _declineValue;

            if (RepeatTime == _minRepeatTime)
            {
                CoroutineManager.s_Instance.StopCoroutine(_declineCorutine);
            }
        }
    }
}
