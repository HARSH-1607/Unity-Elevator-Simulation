# 🛗 Unity Elevator Simulation

A 2D elevator simulation built in **Unity** using the **Universal Render Pipeline (URP)**. The project simulates a multi-elevator system with smart dispatching, queue-based floor requests, and a real-time UI display.

## ✨ Features

- **3 Independent Elevators** — Each elevator operates on its own with smooth vertical movement between floors.
- **Smart Dispatching** — The `ElevatorManager` uses a cost-based algorithm to assign the best elevator for each request, considering distance and current workload.
- **Queue System** — Each elevator maintains its own request queue, processing floors in order.
- **Duplicate Prevention** — Avoids redundant calls if an elevator is already heading to or idle at the requested floor.
- **Real-Time Floor Display** — Each elevator shows its current floor on a TextMeshPro UI element (Ground floor displayed as "G").

## 🏗️ Project Structure

```
Assets/
├── Scenes/
│   └── ElevatorTest.unity      # Main simulation scene
├── Scripts/
│   ├── Elevator.cs             # Individual elevator logic (movement, queue, UI)
│   └── ElevatorManager.cs      # Central dispatcher (cost-based elevator selection)
├── Settings/                   # URP render pipeline settings
└── TextMesh Pro/               # TMP assets for UI text
```

## 🎮 How It Works

1. **Floor buttons** trigger `ElevatorManager.CallElevator(floorNumber)`.
2. The manager evaluates all elevators using a **cost function**:
   - `cost = distance + (queueSize × 2)`
3. The elevator with the **lowest cost** is dispatched.
4. The elevator **smoothly moves** to each queued floor using waypoint transforms.
5. The **UI display** updates when the elevator arrives at a floor.

## 🚀 Getting Started

### Prerequisites
- **Unity 6** (or compatible version with URP)
- **TextMeshPro** package (included)

### Setup
1. Clone this repository:
   ```bash
   git clone https://github.com/HARSH-1607/Unity-Elevator-Simulation.git
   ```
2. Open the project in Unity.
3. Open the scene: `Assets/Scenes/ElevatorTest.unity`
4. Press **Play** to run the simulation.

## 📄 License

This project is open source and available for educational purposes.
