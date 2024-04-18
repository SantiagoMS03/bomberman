using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomPlacementsOnTilemap : MonoBehaviour
{
    public Tilemap availableSpotsTilemap; // Assign your Tilemap in the Unity Inspector
    public GameObject objectToPlacePrefab; // Assign the GameObject prefab you wish to place
    public Transform Parent_Spawn; // Have all of the GameObjects stored in one space in the Hierarchy
    public int numberOfObjectsToPlace = 5; // The number of GameObjects you want to place

    void Start()
    {
        PlaceObjectsRandomly();
    }

    void PlaceObjectsRandomly()
    {
        List<Vector3Int> availablePositions = new List<Vector3Int>();

        // Loop through all the tiles in the Tilemap to find available spots
        BoundsInt bounds = availableSpotsTilemap.cellBounds;
        TileBase[] allTiles = availableSpotsTilemap.GetTilesBlock(bounds);

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int localPosition = new Vector3Int(x, y, (int)availableSpotsTilemap.transform.position.z);
                TileBase tile = availableSpotsTilemap.GetTile(localPosition);
                if (tile != null) // If there's a tile, it's an available spot
                {
                    availablePositions.Add(localPosition);
                }
            }
        }

        // Randomly select positions to place objects
        for (int i = 0; i < numberOfObjectsToPlace; i++)
        {
            if (availablePositions.Count == 0)
            {
                Debug.LogWarning("Not enough available spots to place all objects!");
                break;
            }

            int randomIndex = Random.Range(0, availablePositions.Count);
            Vector3Int selectedCellPosition = availablePositions[randomIndex];
            availablePositions.RemoveAt(randomIndex); // Remove to avoid duplicates

            // Convert the cell position to world position for instantiation
            Vector3 worldPosition = availableSpotsTilemap.CellToWorld(selectedCellPosition) + new Vector3(0.5f, 0.5f, 0); // Adjust for centering if necessary

            // Instantiate the GameObject at the selected position
            (Instantiate(objectToPlacePrefab, worldPosition, Quaternion.identity) as GameObject).transform.parent = Parent_Spawn.transform;
        }
    }
}
