# Unity Finite State Machine
This implementation of an FSM uses scriptable objects to define modular behaviours, using blackboards for context objects.
It might be a very unorthodox implementation of a state machine, but I believe it has a couple of pros:
- Requires developers to make a mental switch and only focus only on a single state, clearing up confusion and minimizing bugs when linking transitions
- Allows for verbose and modular behaviour definitions for each state which can be changed at runtime
- Allows for automatically management behaviour states using properties
- Definition for initialization and cleanup of both transitions and behaviours
