using UnityEngine;

public abstract class SingleTypeObjectSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    protected virtual void SpawnAtRandomPos() { }

    protected Vector2 GetRandomPosition()
    {
        float spawnX = Random.Range(-8.88f, 8.88f);
        float spawnY = Random.Range(-5.004f, 5.00f);
        return new Vector2(spawnX, spawnY);
    }
}

