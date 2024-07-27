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

            gameObject.transform.position = new Vector2(
                    Random.Range(bottomLeft.x, topRight.x),
                    Random.Range(bottomLeft.y, topRight.y)
                );

            StartCoroutine(DeleteObject(spawnObject, gameObject));
        }
    }

    protected override SpawnObject GetObject()
    {
        return GetProbElement(_spawnObjects);
    }
}