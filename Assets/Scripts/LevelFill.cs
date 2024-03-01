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
        if (unbreakableBlocksList != null && unbreakableBlocksList.Count >= 2 && startArea != null && finishArea != null)
        {
            GenerateBreakableBoxes();
        }
        else
        {
            Debug.LogError("Assign at least two unbreakable blocks, start area, and finish area in the inspector!");
        }
    }

    void GenerateBreakableBoxes()
    {
        Vector3 startAreaPosition = startArea.transform.position;
        Vector3 finishAreaPosition = finishArea.transform.position;

        // Randomly select two directions with equal probability (25% chance for each direction)
        int firstDirection = Random.Range(1, 5);
        int secondDirection;

        // Ensure the second direction is different from the first one
        do
        {
            secondDirection = Random.Range(1, 5);
        } while (secondDirection == firstDirection);

        // Instantiate breakable boxes in the selected directions
        InstantiateBoxInDirection(unbreakableBlocksList[0].transform.position, startAreaPosition, finishAreaPosition, firstDirection);
        InstantiateBoxInDirection(unbreakableBlocksList[unbreakableBlocksList.Count - 1].transform.position, startAreaPosition, finishAreaPosition, secondDirection);
    }

    void InstantiateBoxInDirection(Vector3 unbreakableBlocksPosition, Vector3 startAreaPosition, Vector3 finishAreaPosition, int direction)
    {
        Vector3 boxPosition = unbreakableBlocksPosition;

        switch (direction)
        {
            case 1: // North
                boxPosition += Vector3.forward * 1.0f;
                break;
            case 2: // South
                boxPosition -= Vector3.forward * 1.0f;
                break;
            case 3: // West
                boxPosition -= Vector3.right * 1.0f;
                break;
            case 4: // East
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
