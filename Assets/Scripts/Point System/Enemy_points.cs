using UnityEngine;
using TMPro; // Import this namespace for TextMeshPro

public class Enemy : MonoBehaviour
{
    public int pointsValue = 100; // Points to add when destroyed by an explosion

    // Reference to your score TextMeshPro UI element
    // Make sure to assign this in the Inspector
    public ScoreDisplay scoreText;
    public GameObject Delete_this;
    private ExitDoor_Control Door;
    private void Start()
    {
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<ScoreDisplay>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Explosion"))
        {
            scoreText.AddPoints(pointsValue);
            if (ExitDoor_Control.DoorSwpawned)
            {
                Door = GameObject.FindWithTag("Door").GetComponent<ExitDoor_Control>();
                Door.Invoke("Awake", 0.5f);
            }
            Destroy(Delete_this); // Destroy enemy
        }
    }
}
