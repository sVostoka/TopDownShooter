using UnityEngine;

public class BoosterSpawn : Spawner
{
    protected override void SpawnObject()
    {
        SpawnObject spawnObject = GetObject();
        if (spawnObject != null)
        {
            GameObject gameObject = spawnObject.pool.GetElement();

            Vector3 bottomLeft = _camera.ScreenToWorldPoint(Vector2.zero);
            Vector3 topRight = _camera.ScreenToWorldPoint(new(Screen.width, Screen.height));

            var (max, min) = _area.GetMaxMin();

            gameObject.transform.position = new(
                    Mathf.Clamp(Random.Range(bottomLeft.x, topRight.x), min.x, max.x),
                    Mathf.Clamp(Random.Range(bottomLeft.y, topRight.y), min.y, max.y)
                );

            StartCoroutine(DeleteObject(spawnObject, gameObject));
        }
    }

    protected override SpawnObject GetObject()
    {
        return GetProbElement(_spawnObjects);
    }
}