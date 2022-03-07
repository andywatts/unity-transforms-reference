using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct ParentPrefab : IComponentData
{
    public Entity Value;
}
