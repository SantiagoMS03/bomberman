using UnityEngine;
using System.Collections.Generic;

public class LevelFill : MonoBehaviour
{
    public List<GameObject> unbreakableBlocksList;
    public GameObject startArea;
    public GameObject finishArea;
    public GameObject breakableBoxPrefab;

    void Start()
    {
        if (unbreakableBlocksList != null && unbreakableBlocksList.Count > 0 && startArea != null && finishArea != null)
        {
            GenerateBreakableBoxes();
        }
        else
        {
            Debug.LogError("Assign at least one unbreakable block, start area, and finish area in the inspector!");
        }
    }

    void GenerateBreakableBoxes()
    {
        Vector3 startAreaPosition = startArea.transform.position;
        Vector3 finishAreaPosition = finishArea.transform.position;

        foreach (GameObject unbreakableBlock in unbreakableBlocksList)
        {
            // Guarantee one box in a random direction
            int guaranteedDirection = Random.Range(1, 5);
            InstantiateBoxInDirection(unbreakableBlock.transform.position, startAreaPosition, finishAreaPosition, guaranteedDirection);

            // 80% chance of having two additional boxes in two different cardinal directions
            if (Random.value < 0.8f)
            {
                List<int> possibleDirections = new List<int> { 1, 2, 3, 4 };
                possibleDirections.Remove(guaranteedDirection); // Remove the guaranteed direction

                int firstAdditionalDirection = possibleDirections[Random.Range(0, possibleDirections.Count)];
                InstantiateBoxInDirection(unbreakableBlock.transform.position, startAreaPosition, finishAreaPosition, firstAdditionalDirection);

                possibleDirections.Remove(firstAdditionalDirection);

                if (possibleDirections.Count > 0)
                {
                    int secondAdditionalDirection = possibleDirections[Random.Range(0, possibleDirections.Count)];
                    InstantiateBoxInDirection(unbreakableBlock.transform.position, startAreaPosition, finishAreaPosition, secondAdditionalDirection);
                }
            }
        }
    }

    void InstantiateBoxInDirection(Vector3 unbreakableBlocksPosition, Vector3 startAreaPosition, Vector3 finishAreaPosition, int direction)
    {
        Vector3 boxPosition = unbreakableBlocksPosition;

        // Adjusted vector operations for up and down directions
        switch (direction)
        {
            case 1: // North (Up)
                boxPosition += Vector3.up * 1.0f;
                break;
            case 2: // South (Down)
                boxPosition -= Vector3.up * 1.0f;
                break;
            case 3: // West (Left)
                boxPosition -= Vector3.right * 1.0f;
                break;
            case 4: // East (Right)
                boxPosition += Vector3.right * 1.0f;
                break;
        }

        // Check if breakable boxes will interfere with the start or finish areas
        if (!IsInterferingWithArea(boxPosition, startAreaPosition, finishAreaPosition))
        {
            // Instantiate breakable boxes at the calculated position
            Instantiate(breakableBoxPrefab, boxPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Breakable boxes would interfere with the start or finish areas. Adjust positions.");
        }
    }

    bool IsInterferingWithArea(Vector3 boxPosition, Vector3 startAreaPosition, Vector3 finishAreaPosition)
    {
        // Define a tolerance value to avoid interference due to float precision
        float tolerance = 0.1f;

        // Check if the box position is within the tolerance range of start or finish areas
        return Vector3.Distance(boxPosition, startAreaPosition) < tolerance ||
               Vector3.Distance(boxPosition, finishAreaPosition) < tolerance;
    }
}
