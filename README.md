# Memory Snapshot

[![License Badge](https://img.shields.io/github/license/andrewmichaeljones/memorysnapshot)](/LICENSE.md)

## Description
This simple tool takes memory snapshots of a connected Unity player at regular intervals.
https://docs.unity3d.com/ScriptReference/Profiling.Memory.Experimental.MemoryProfiler.TakeSnapshot.html

## Usage
- Create and run a development build of your project
- Install the experimental memory profiler package https://docs.unity3d.com/Packages/com.unity.memoryprofiler@0.6/manual/index.html
- Connect the Unity profiler to the running project
- Open the Memory Snapshot editor window Tools -> Memory Snapshot
- Update the output location to "{ProjectPath}/MemoryCaptures"
    - This is where the experimental memory profiler package expects the snapshots to live
- Press "Take Snapshots"
- Open the Window -> Analysis -> Memory Profile and you should see the snapshots to the left

## Adding the Package to a Project
- Open your project manifest file (`MyProject/Packages/manifest.json`).
- Add `"memorysnapshot": "https://github.com/andrewmichaeljones/MemorySnapshot.git"` to the `dependencies` list (replacing package name and repository information).
- Open or focus on Unity Editor to resolve packages.
