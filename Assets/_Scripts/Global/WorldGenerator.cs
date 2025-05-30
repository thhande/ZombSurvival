using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Pathfinding;
using System.Collections;

public class InfiniteWorldGenerator : MonoBehaviour
{
    public Tilemap groundTilemap;
    public Tilemap obstacleTilemap;

    public TileBase[] groundTiles;
    public TileBase[] obstacleTiles;

    public Transform player;
    public int renderRadius = 65;

    private Vector3Int lastPlayerTilePos;
    private HashSet<Vector3Int> generatedTiles = new HashSet<Vector3Int>();

    void Start()
    {
        if (player == null) player = GameObject.FindAnyObjectByType<PlayerMovement>().transform;
        lastPlayerTilePos = groundTilemap.WorldToCell(player.position);
        GenerateTilesAround(lastPlayerTilePos);
        StartCoroutine(DelayedScan());
    }

    void Update()
    {

        // Vector3Int currentPlayerTilePos = groundTilemap.WorldToCell(player.position);

        // if (Vector3Int.Distance(currentPlayerTilePos, lastPlayerTilePos) >= 1)
        // {
        //     lastPlayerTilePos = currentPlayerTilePos;
        //     GenerateTilesAround(currentPlayerTilePos);
        // }
    }

    void GenerateTilesAround(Vector3Int center)
    {
        for (int x = -renderRadius; x <= renderRadius; x++)
        {
            for (int y = -renderRadius; y <= renderRadius; y++)
            {
                Vector3Int pos = new Vector3Int(center.x + x, center.y + y, 0);
                if (generatedTiles.Contains(pos)) continue;

                generatedTiles.Add(pos);

                float noise = Mathf.PerlinNoise((pos.x + 1000) * 0.1f, (pos.y + 1000) * 0.1f); // +1000 để tránh bị lặp map gần gốc

                if (noise > 0.75f) // Tỉ lệ chướng ngại vật
                {
                    TileBase obstacleTile = obstacleTiles[Random.Range(0, obstacleTiles.Length)];
                    obstacleTilemap.SetTile(pos, obstacleTile);
                }
                else
                {
                    TileBase groundTile = groundTiles[Random.Range(0, groundTiles.Length)];
                    groundTilemap.SetTile(pos, groundTile);
                }

            }
        }
    }
    IEnumerator DelayedScan()
    {
        yield return new WaitForEndOfFrame(); // hoặc yield return null;
        AstarPath.active.Scan();
    }
}
