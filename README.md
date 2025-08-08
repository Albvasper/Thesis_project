![App Screenshot](https://img.itch.zone/aW1nLzIyNjA4MjQzLnBuZw==/original/dB%2FQfA.png)

# 🎮 Advanced RTS Game with Intelligent AI Systems

[![Unity](https://img.shields.io/badge/Unity-2022.3%20LTS-black.svg?style=for-the-badge&logo=unity)](https://unity.com/)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Status](https://img.shields.io/badge/Status-Production%20Ready-brightgreen?style=for-the-badge)](https://github.com/Albvasper/Thesis_project)

> **An RTS game featuring a 6-layer behavior tree AI, advanced unit management, and real-time strategic gameplay built in Unity.**

## Project Overview

This project demonstrates advanced game programming skills through an RTS game implementation featuring intelligent enemy AI, complex unit coordination, and player interaction systems. Built with performance and scalability in mind, the game maintains 60+ FPS with 100+ active units.

### Key Technical Achievements
- **6-Layer Behavior Tree AI** managing strategic decisions across defense, economy, and offense
- **Advanced State Pattern** implementation for unit behaviors with seamless transitions
- **Multi-Modal Player Controls** supporting complex RTS interactions (multi-select unit system, etc.)
- **Real-Time Building System** with grid-based placement and NavMesh integration
- **Performance-Optimized Architecture** maintaining stable framerate with complex systems

## Technical Stack

| **Category** | **Technology** | **Purpose** |
|--------------|----------------|-------------|
| **Engine** | Unity 2022.3 LTS | Core game development platform |
| **Language** | C# | Primary programming language |
| **AI System** | Custom Behavior Trees | Strategic enemy AI decision-making |
| **Pathfinding** | Unity NavMesh | Unit movement and navigation |
| **Architecture** | State Pattern, Singleton | Clean code organization |

## Core Systems Architecture

### Behavior Tree AI System
```
Root (Sequence Node)
├── Layer 1: Strategic Priorities
│   ├── CheckIfUnderAttack → Defensive Response
│   ├── CheckUnitSpaces → Population Management  
│   └── CheckResourceNeeds → Economic Planning
├── Layer 2-6: Tactical Execution
│   ├── Resource Allocation Decisions
│   ├── Unit Production Coordination
│   └── Strategic Building Placement
```

### Unit Management Hierarchy
- **Base Unit Class**: Health, selection, combat mechanics
- **Mobile Units**: NavMesh movement, state-based behaviors
- **Stationary Units**: Resource generation, unit production
- **Specialized Units**: Intern (player), EnemyRecolectors (AI)

## Game Features

### **Intelligent Enemy AI**
- **Strategic Decision Making**: 20+ specialized behavior tree nodes
- **Resource Management**: Balances economy, military, and infrastructure
- **Adaptive Responses**: Reacts dynamically to player actions
- **Multi-Priority Coordination**: Handles defense, offense, and economy simultaneously

### **Advanced Player Controls**
- **Multi-Selection**: Single click, Ctrl+click, drag-box selection
- **Context Commands**: Smart right-click behavior based on target type
- **Real-Time Feedback**: Dynamic UI updates and unit status displays
- **Building System**: Grid-snapped placement with rotation controls

### **Performance Features**
- **Optimized Update Cycles**: Efficient behavior tree evaluation
- **Memory Management**: Proper object cleanup and resource handling
- **Scalable Architecture**: Supports unlimited unit types and AI behaviors
- **Stable Framerate**: 60+ FPS with complex systems and large unit counts

## Project Structure

```
Assets/
├── Scripts/
│   ├── Base classes/
│   │   ├── Enemy AI/           # Behavior tree implementation
│   │   └── Units/              # Unit hierarchy and behaviors
│   │       └── StateMachine/   # State pattern implementations
│   ├── Game system/            # Game system support scripts
│   ├── NavMesh Components/     # Unity's nav mesh system
│   ├── Player/ 
│   │   ├── BuildManager.cs     # Real-time building system
│   │   ├── UnitController.cs   # Unit selection handling
│   │   ├── CamController.cs    # Player camera input handling
│   │   ├── EmailManager.cs     # In-game UI for messages
│   │   ├── Player.cs           # Player controller and info storage 
│   │   └── ShopManager.cs      # Implementation for buying units
│   ├── Resource/               # Classes for game world resources
│   ├── Units/                  # All unit classes
│   └── GUI/                    # Interface and HUD systems
├── Prefabs/                    # Unit and building prefabs
├── Scenes/                     # Game levels and menus
└── Materials/                  # Visual assets
```

## Key Scripts

| Script                                                                                                     | Description                                                                 |
|------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------|
| [`EnemyAI.cs`](https://github.com/Albvasper/Thesis_project/blob/c76518aa31a3dd2826167692c95369ebf55f31da/Thesis_project/Assets/Scripts/Base%20classes/Enemy%20AI/EnemyAI.cs)       | Enemy AI that uses a 6 layer behavior tree                      |
| [`BehaviorTree.cs`](https://github.com/Albvasper/Thesis_project/blob/c76518aa31a3dd2826167692c95369ebf55f31da/Thesis_project/Assets/Scripts/Base%20classes/Enemy%20AI/BehaviorTree/BehaviorTree.cs)       | Core behavior tree system that runs enemy AI decisions                     |
| [`Node.cs`](https://github.com/Albvasper/Thesis_project/blob/c76518aa31a3dd2826167692c95369ebf55f31da/Thesis_project/Assets/Scripts/Base%20classes/Enemy%20AI/BehaviorTree/Nodes/Node.cs)                       | Base class for actions, conditions, and composite AI nodes                 |
| [`Unit.cs`](https://github.com/Albvasper/Thesis_project/blob/c76518aa31a3dd2826167692c95369ebf55f31da/Thesis_project/Assets/Scripts/Base%20classes/Unit/Unit.cs)                    | Base unit logic with movement, task assignment, and coroutine FSM          |
| [`BuildManager.cs`](https://github.com/Albvasper/Thesis_project/blob/c76518aa31a3dd2826167692c95369ebf55f31da/Thesis_project/Assets/Scripts/Player/BuildManager.cs) | Manages building placement, snapping, and resource cost validation         |
| [`Player.cs`](https://github.com/Albvasper/Thesis_project/blob/c76518aa31a3dd2826167692c95369ebf55f31da/Thesis_project/Assets/Scripts/Player/Player.cs)   | Oversees global player state, task flow, and win/loss tracking               |
| [`State.cs`](https://github.com/Albvasper/Thesis_project/blob/c76518aa31a3dd2826167692c95369ebf55f31da/Thesis_project/Assets/Scripts/Base%20classes/Unit/StateMachine/State.cs)             | Finite state machine implementation       |



## Performance Metrics

- **AI Processing**: 6-layer decision tree evaluated every frame
- **Unit Capacity**: 100+ active units with complex behaviors
- **Framerate**: Stable 60+ FPS during intensive gameplay
- **Memory**: Efficient resource management with proper cleanup
- **Response Time**: Real-time player input with immediate feedback

## Installation & Setup

### Prerequisites
- Unity 2022.3 LTS or later
- Visual Studio 2019/2022 or VS Code
- Git for version control

### Quick Start
```bash
# Clone the repository
git clone https://github.com/Albvasper/Thesis_project.git

# Open in Unity
# File → Open Project → Select the project folder
# Thesis_project/Thesis_project/

# Play in Editor
# Open MainScene and press Play button
```


## Play Game
[![Download on Itch.io (Key: RTSGAME)](https://img.shields.io/badge/Download-itch.io-FA5C5C?style=for-the-badge&logo=itch.io&logoColor=white)](https://albvasper.itch.io/industry-simulator)


## Screenshots

![App Screenshot](https://img.itch.zone/aW1hZ2UvOTY3OTkzLzIyNjA4MTk1LnBuZw==/original/5O29fl.png)

![App Screenshot](https://img.itch.zone/aW1hZ2UvOTY3OTkzLzIyNjA4MTk5LnBuZw==/original/kcwfnI.png)

![App Screenshot](https://img.itch.zone/aW1hZ2UvOTY3OTkzLzIyNjA4MTk2LnBuZw==/original/l6pZ6t.png)


## Technical Highlights

### Advanced Programming Concepts
- **Design Patterns**: State, Singleton, Abstract Factory implementations
- **OOP Principles**: Clean inheritance hierarchies and polymorphism
- **Performance Optimization**: Efficient algorithms and memory management
- **System Architecture**: Modular design with clear separation of concerns

### Unity Expertise
- **AI Programming**: Custom behavior tree framework
- **NavMesh Integration**: Advanced pathfinding and navigation
- **UI Systems**: Dynamic interface with real-time updates
- **Input Handling**: Complex RTS control schemes

### Problem-Solving Skills
- **Complex System Integration**: Multiple systems working seamlessly together
- **Performance Under Load**: Maintaining stability with intensive operations  
- **Scalable Architecture**: Design supports easy expansion and modification
- **Real-World Application**: Production-ready code with proper error handling

## Contact

**Alberto Vásquez** - Game Programmer  
 [albert.vp09@gmail.com]  
 [https://codebyalberto.framer.website/]  

