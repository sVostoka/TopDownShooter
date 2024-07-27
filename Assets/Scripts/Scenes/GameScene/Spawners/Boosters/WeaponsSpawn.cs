using System.Collections.Generic;

public class WeaponSpawn : BoosterSpawn
{
    private Player _player;

    protected override SpawnObject GetObject()
    {
        _player ??= FindAnyObjectByType<Player>();

        if (_player.Weapon != null && _spawnObjects != null)
        {
            List<SpawnObject> spawnObjects = new(_spawnObjects);

            foreach (var el in spawnObjects)
            {
                if (el.prefab.GetComponent<WeaponBooster>().Weapon.GetType() == _player.Weapon.GetType())
                {
                    spawnObjects.Remove(el);
                    return GetProbElement(spawnObjects);
                }
            }
        }

        return GetProbElement(_spawnObjects);
    }
}
