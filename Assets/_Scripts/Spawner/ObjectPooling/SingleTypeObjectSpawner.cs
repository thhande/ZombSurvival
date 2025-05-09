using UnityEngine;

public abstract class SingleTypeObjectSpawner : MonoBehaviour
{
    public Transform spawnPoint;


    protected virtual void SpawnAtRandomPos() { }

    protected Vector2 GetRandomPosition()
    {
        // Get the bottom left and top right corners of the camera view in world coordinates
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));

        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));


        float padding = 0.5f;


        float spawnX = Random.Range(bottomLeft.x + padding, topRight.x - padding);
        float spawnY = Random.Range(bottomLeft.y + padding, topRight.y - padding);
        return new Vector2(spawnX, spawnY);
    }
}

