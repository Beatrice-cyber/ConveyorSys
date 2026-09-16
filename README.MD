# Modular Conveyor System

A modular conveyor placement system built in Unity.

## Features

- Three placeable conveyor types: Long, Short and Incline
- Mouse-based conveyor placement
- Automatic snapping between compatible conveyor endpoints
- Connected conveyors form a continuous path
- Box and can products can be spawned onto conveyors
- Products automatically traverse connected conveyor sections
- Windows standalone build

## Controls

- `1` - Select Long Conveyor
- `2` - Select Short Conveyor
- `3` - Select Incline Conveyor
- `Left Mouse Button` - Place conveyor
- `Space` - Finish conveyor placement / enter product mode
- Hover over a conveyor and press `B` - Spawn box
- Hover over a conveyor and press `C` - Spawn can

## Implementation

Each conveyor exposes start and end connection points. During placement,
the preview conveyor searches for available conveyor endpoints within a
snap threshold and aligns its start point with the selected endpoint.

Placed conveyors maintain a reference to the next connected conveyor.
Products use this linked path to move from the end of one conveyor to
the next.

Product movement is implemented using deterministic transform-based
movement rather than Rigidbody physics.

## Running

A Windows standalone build is provided. Extract the complete build
folder and run the `.exe`.
