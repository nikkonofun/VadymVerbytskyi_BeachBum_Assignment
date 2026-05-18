# BeachBum Assignment (Vadym Verbytskyi)

War card game implementation using Unity/C#

## Technology stack

- Unity `2022.3.21f1`
- C#
- Zenject (DI)
- UniTask (async)
- DOTween (animations)
- Newtonsoft.Json (client-server communication)
- Unity Test Framework (tests)

## Getting Started

1. Install Unity Hub and Unity Editor `2022.3.21f1`.
2. Open the project folder in Unity Hub:
   - `BeachBum_Assignment_VadymVerbytskyi`
3. Open `Assets/Scenes/MetagameScene.unity`.
4. Press Play in the Unity Editor.

## Configs

Assets/Resources/Configs/AnimationConfig - animation timings
Assets/Resources/Configs/FakeNetworkConfig - allows to simulate bad network on fake network layer
Assets/Resources/Configs/RetryPolicyConfig - reteies setup for commands

## High level architecture

- `Assets/Scripts/Client` — UI, input handling, animations, app state, and request/response flow.
- `Assets/Scripts/Server` — match logic and turn processing.
- `Assets/Scripts/Shared` — DTOs, common models, and a fake network layer used by the client.

## Key Features
- Clean Architecture by Design — the project is intentionally split into Client, Server, and Shared layers, making responsibilities explicit and the codebase easy to navigate.
- Client-Server Gameplay in a Single Unity App — gameplay flow is modeled as real request/response interactions instead of tightly coupled local calls.
- Realistic Network Simulation — FakeNetwork supports configurable delay, packet loss, failures, and timeouts to test resilience under unstable conditions.
- Reliability-Focused Request Pipeline — retry policy and timeout handling are built in, so transient failures are handled as a first-class concern.
- Deterministic Server-Side Match Logic — core game progression is encapsulated in dedicated server modules (MatchInitializer, MatchProcessor) for predictable behavior.
- Event-Driven Turn Processing — turn results are delivered through a turn-events feed, cleanly separating game logic from presentation.
- Polished Gameplay Presentation — turn animations, match-end sequences, notifications, and UI highlights create clear and readable player feedback.
- Dependency Injection with Zenject — DI keeps modules loosely coupled, easier to maintain, and straightforward to test.
- Modern Async and Animation Tooling — UniTask and DOTween are used to keep async workflows and visual transitions performant and maintainable.
- Multi-Layer Test Coverage — the project includes EditMode, PlayMode, and server logic tests, demonstrating focus on correctness across logic and UX layers.
