using System.Collections.Generic;
using UnityEngine;
using TMPro; // Required to use TextMeshPro UI elements

public class Elevator : MonoBehaviour
{
    [Header("Elevator Settings")]
    public float speed = 3f; // How fast the elevator moves
    public int currentFloor = 0; // 0 = Ground, 1 = First, etc.
    
    [Header("UI References")]
    public TextMeshProUGUI floorDisplay; // The UI text showing the current floor

    [Header("Floor Positions")]
    // We will drag empty GameObjects here to tell the elevator where the exact Y heights are
    public Transform[] floorWaypoints; 

    // A list to act as our request queue
    public List<int> floorQueue = new List<int>(); 
    
    private bool isMoving = false;
    private int targetFloor;

    void Start()
    {
        // Update the UI right when the game starts
        UpdateDisplay();
    }

    void Update()
    {
        // 1. Check if we need to move
        // If we are NOT currently moving, but there is a floor in our queue, start moving!
        if (!isMoving && floorQueue.Count > 0)
        {
            targetFloor = floorQueue[0]; // Set our target to the first item in the list
            isMoving = true;
        }

        // 2. Handle the smooth movement
        if (isMoving)
        {
            Transform targetTransform = floorWaypoints[targetFloor];
            
            // Vector2.MoveTowards moves our elevator toward the target smoothly based on speed and time
            transform.position = Vector2.MoveTowards(
                transform.position, 
                new Vector2(transform.position.x, targetTransform.position.y), 
                speed * Time.deltaTime
            );

            // 3. Check if we have arrived
            // We use Mathf.Abs to check the distance. If it's less than 0.01, we are basically there.
            if (Mathf.Abs(transform.position.y - targetTransform.position.y) < 0.01f)
            {
                // Snap it perfectly into place
                transform.position = new Vector2(transform.position.x, targetTransform.position.y); 
                
                currentFloor = targetFloor; // Update our current floor memory
                isMoving = false; // We stopped moving
                floorQueue.RemoveAt(0); // Remove this floor from the queue since we finished the job
                
                UpdateDisplay(); // Update the UI text
            }
        }
    }

    // The ElevatorManager will call this function to give this elevator a job
    public void AddFloorToQueue(int floorNumber)
    {
        // Only add it if it's not already the very last thing we were told to do
        if (floorQueue.Count == 0 || floorQueue[floorQueue.Count - 1] != floorNumber) 
        {
            floorQueue.Add(floorNumber);
        }
    }

    // Updates the TextMeshPro UI
    private void UpdateDisplay()
    {
        if (floorDisplay != null)
        {
            // If it's floor 0, show "G" for Ground, otherwise show the number
            floorDisplay.text = currentFloor == 0 ? "G" : currentFloor.ToString();
        }
    }
}