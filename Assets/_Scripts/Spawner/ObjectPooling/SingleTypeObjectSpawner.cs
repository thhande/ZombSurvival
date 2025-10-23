using UnityEngine;

public abstract class SingleTypeObjectSpawner : ObjectSpawner
{
    public Transform spawnPoint;

    protected virtual void SpawnAtRandomPos() { }

}

