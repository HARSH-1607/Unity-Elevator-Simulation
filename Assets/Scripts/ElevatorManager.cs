using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    [Header("Connect Your 3 Elevators Here")]
    public Elevator[] elevators; // An array to hold our 3 lift scripts

    // The UI Buttons will call this function and pass in their specific floor number
    public void CallElevator(int requestedFloor)
    {
        Elevator bestElevator = null;
        int lowestCost = int.MaxValue; // Start with an impossibly high number

        foreach (Elevator elevator in elevators)
        {
            // 1. PREVENT DUPLICATES: 
            // If an elevator is already on its way to this floor, or is already there and waiting, ignore the call.
            if (elevator.floorQueue.Contains(requestedFloor) || (elevator.currentFloor == requestedFloor && elevator.floorQueue.Count == 0))
            {
                Debug.Log($"Elevator already at or heading to floor {requestedFloor}. Call ignored.");
                return; 
            }

            // 2. CALCULATE "COST":
            // We determine the "best" elevator by calculating how much work it has to do to get there.
            int distance = Mathf.Abs(elevator.currentFloor - requestedFloor);
            
            // The base cost is just the physical distance
            int cost = distance;

            // Add a penalty if the elevator is already busy with other requests
            cost += (elevator.floorQueue.Count * 2);

            // 3. COMPARE:
            // Is this elevator the cheapest (nearest/most available) one we've checked so far?
            if (cost < lowestCost)
            {
                lowestCost = cost;
                bestElevator = elevator;
            }
        }

        // 4. DISPATCH:
        // Give the job to the winner!
        if (bestElevator != null)
        {
            Debug.Log($"Dispatching Elevator to floor {requestedFloor}");
            bestElevator.AddFloorToQueue(requestedFloor);
        }
    }
}