![fsm_example.gif](Example%2Ffsm_example.gif)
#### A lightweight, declarative Finite State Machine (FSM) library for Unity, built to simplify state management across AI, gameplay, and UI systems.

## 🚀 Advantages
**UnityBlock.FSM** was created to be:

| Principle      | What It Means                                                    |
|----------------|------------------------------------------------------------------|
| 🧩 **Modular**  | States are self-contained and only interact via context.        |
| 🧼 **Clean**    | Transition logic stays outside of state `Update()` methods.     |
| ⚡ **Lightweight** | Minimal boilerplate and no dependency on MonoBehaviours.     |
| 🧠 **Flexible** | Adaptable to AI, gameplay, UI, or simulation needs.             |


## 📦 Installation
To add this library to your Unity project, include the following line in your manifest.json:
```json
"unityblock.fsm" : "https://github.com/vstrucovski/UnityBlock.FSM.git"
```
Or clone the repo into your Unity Packages/ or Assets/Plugins/ folder.

<br>

## 🚀 Quick Start
### 1. Define Your States
Inherit from BaseState and override methods as needed:
```csharp
public class IdleState : BaseState
{
    public override void OnEnter() => Debug.Log("Entered Idle");

    public override void Update()
    {
        if (Context.Get<bool>("isJumpReady"))
        {
            Debug.Log("Ready to jump");
        }
    }

    public override void OnExit() => Debug.Log("Exiting Idle");
}
```

### 2. Set Up the State Machine
```csharp
var context = new SharedContext();
context.Set("characterName", "Robot_01");

var brain = new BaseStateMachine(context);
brain.AddState(new IdleState());
brain.AddState(new JumpState());

// Add transitions
brain.AddTransition<IdleState, JumpState>(() => context.Get<bool>("shouldJump"));
brain.AddDelayedTransition<JumpState, IdleState>(1.5f);

// Start from a specific state
brain.Enter<IdleState>();
```

### 3. Don't forget to run it in your Update loop
```csharp
void Update()
{
    brain.Update();
}
```

<br>

## Misc
### 1. SharedContext Design
Yes — it's a `Dictionary<string, object>` under the hood. And yes, that's by design.
The goal is to provide a **universal communication layer** between states without:

- Coupling states through constructors or interfaces
- Creating rigid hierarchies or state-specific payload classes
- Cluttering state logic with cross-references

This design makes prototyping fast and keeps the API surface minimal.
```csharp
context.Set("health", 100);
int hp = context.Get<int>("health");
```
🔐 Safer Access (Optional)
```csharp
public static class Ctx
{
    public const string Health = "health";
}
```

<br>

### 2. Logging (Optional)
Add a logger component to your GameObject and then set it to your state machine
```csharp
brain.SetLogger(GetComponent<StateMachineLogger>());
```
