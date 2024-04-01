using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnGrid : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int width = 33;
    public int length = 9;
    public int numberOfPrefabsToSpawn;

    private void Start()
    {
        List<Vector3> availablePositions = GenerateAvailablePositions();
        ExcludeUnbreakableBoxPositions(ref availablePositions);
        ExcludePlayerStartPositions(ref availablePositions);

        if (numberOfPrefabsToSpawn > availablePositions.Count)
        {
            Debug.LogWarning($"Requested {numberOfPrefabsToSpawn} prefabs, but only {availablePositions.Count} spots are available after excluding unbreakable and player start areas. Adjusting number to spawn.");
            numberOfPrefabsToSpawn = availablePositions.Count;
        }

        SpawnPrefabs(availablePositions);
    }

    List<Vector3> GenerateAvailablePositions()
    {
        List<Vector3> positions = new List<Vector3>();
        Vector3 startPosition = transform.position - new Vector3(width / 2, length / 2, 0);
        for (int y = 0; y < length; y++)
        {
            for (int x = 0; x < width; x++)
            {
                positions.Add(new Vector3(x + startPosition.x, y + startPosition.y, 0));
            }
        }
        return positions;
    }

    void ExcludeUnbreakableBoxPositions(ref List<Vector3> availablePositions)
    {
        GameObject[] unbreakables = GameObject.FindGameObjectsWithTag("Unbreakable");
        foreach (var unbreakable in unbreakables)
        {
            Vector3 roundedPos = new Vector3(Mathf.RoundToInt(unbreakable.transform.position.x), Mathf.RoundToInt(unbreakable.transform.position.y), 0);
            availablePositions.Remove(roundedPos);
        }
    }

    void ExcludePlayerStartPositions(ref List<Vector3> availablePositions)
    {
        GameObject playerStart = GameObject.FindGameObjectWithTag("PlayerStart");
        if (playerStart != null)
        {
            BoxCollider collider = playerStart.GetComponent<BoxCollider>();
            Vector2 playerStartSize = new Vector2(collider.size.x * playerStart.transform.localScale.x, collider.size.y * playerStart.transform.localScale.y);
            Vector2 playerStartPos = new Vector2(playerStart.transform.position.x, playerStart.transform.position.y);
            Vector2 playerStartBoundsMin = playerStartPos - playerStartSize * 0.5f;
            Vector2 playerStartBoundsMax = playerStartPos + playerStartSize * 0.5f;

            availablePositions.RemoveAll(pos =>
                pos.x >= playerStartBoundsMin.x && pos.x <= playerStartBoundsMax.x &&
                pos.y >= playerStartBoundsMin.y && pos.y <= playerStartBoundsMax.y);
        }
        else
        {
            Debug.LogError("No GameObject with 'PlayerStart' tag found. Please ensure your player start area is tagged correctly.");
        }
    }

    void SpawnPrefabs(List<Vector3> availablePositions)
    {
        for (int i = 0; i < numberOfPrefabsToSpawn; i++)
        {
            int randomIndex = Random.Range(0, availablePositions.Count);
            Vector3 spawnPosition = availablePositions[randomIndex];
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            availablePositions.RemoveAt(randomIndex);
        }
    }
}
