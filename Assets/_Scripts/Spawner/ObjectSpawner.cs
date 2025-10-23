using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MMono
{

    const float Xbound = 50.5f;
    const float Ybound = 50.3f;
    [SerializeField] private LayerMask obstacleLayer;
    protected override void Awake()
    {
        base.Awake();
        obstacleLayer = LayerMask.GetMask("Obstacles");
    }

    protected Vector2 GetRandomPositionAroundCamera()
    {
        // Get the bottom left and top right corners of the camera view in world coordinates
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        float padding = 0.5f;
        float spawnX = Random.Range(bottomLeft.x + padding, topRight.x - padding);
        float spawnY = Random.Range(bottomLeft.y + padding, topRight.y - padding);
        Vector2 pos = new Vector2(spawnX, spawnY);
        if (SpawnOnObstacle(pos)) return GetRandomPositionAroundCamera();

        else return pos;
    }

    protected Vector2 GetRandomPositionAroundWorld()
    {
        float spawnX = Random.Range(Xbound, -Xbound);
        float spawnY = Random.Range(Ybound, -Ybound);
        Vector2 pos = new Vector2(spawnX, spawnY);
        if (SpawnOnObstacle(pos)) return GetRandomPositionAroundWorld();
        return pos;
    }

    private bool SpawnOnObstacle(Vector2 pos)
    {
        Collider2D hit = Physics2D.OverlapCircle(pos, 1.5f, obstacleLayer);
        return hit != null;
    }

}
