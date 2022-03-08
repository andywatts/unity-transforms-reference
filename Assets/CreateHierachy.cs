using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class CreateHierarchy : SystemBase
{
    private bool runOnce;

    protected override void OnUpdate()
    {
        if (!runOnce)
        {
            var parentPrefab = GetSingleton<ParentPrefab>().Value;
            var parent = EntityManager.Instantiate(parentPrefab);
            
            var childPrefab = GetSingleton<ChildPrefab>().Value;
            var child = EntityManager.Instantiate(childPrefab);
            
            // Add Parent and friends for ParentSystem's updateNewParents
            EntityManager.AddComponentData(child, new Parent {Value = parent});
            EntityManager.AddComponentData(child, new LocalToParent {Value = new float4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1)});
            EntityManager.AddComponentData(child, new LocalToWorld() {Value = new float4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1)});
            
            
            // Set Rotation on parent
            var q = quaternion.EulerXYZ(0, 45 * Mathf.Deg2Rad, 0);
            SetComponent(parent, new Rotation {Value = q});
            
            runOnce = true;
        }
    }
}

            // var children = EntityManager.AddBuffer<Child>(parent);
            // children.Add(new Child {Value = child});
