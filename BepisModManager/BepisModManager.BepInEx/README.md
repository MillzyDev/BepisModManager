# BepisModManager.BepInEx

BepisModManager.BepInEx is a library for getting different BepInEx versions, this can be used in your own projects.

```cs
// This will get the latest bleeding edge build of BepInEx
BleedingArtifacts bleeding = BleedingEdge.GetLatestArtifacts();

// Get a specific bleeding edge build of BepInEx
BleedingArtifacts oldBleeding = BleedingEdge.GetArtifactsWithId("520");

// Get the latest GitHub release for BepInEx
ReleaseArtifacts release = Release.GetLatestArtifacts();

// Get a specific release of BepInEx
ReleaseArtifacts oldRelease = Release.GetArtifactsWithVersion("5.4.12");
```
