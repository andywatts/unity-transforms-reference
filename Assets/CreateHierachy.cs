using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class CreateHierachy : SystemBase
{
    private bool runOnce;

    protected override void OnUpdate()
    {
        if (!runOnce)
        {
            Debug.Log("Create parent and cube");
            
            var parentPrefab = GetSingleton<ParentPrefab>().Value;
            var parent = EntityManager.Instantiate(parentPrefab);
            
            var cubePrefab = GetSingleton<CubePrefab>().Value;
            var cube = EntityManager.Instantiate(cubePrefab);
            EntityManager.AddComponentData(cube, new Parent {Value = cube});
            EntityManager.AddComponentData(cube, new LocalToParent {Value = new float4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1)});
                
                
            var children = EntityManager.AddBuffer<Child>(parent);
            children.Add(new Child {Value = cube});

            var q = quaternion.EulerXYZ(0, 45 * Mathf.Deg2Rad, 0);
            SetComponent(parent, new Rotation {Value = q});
            runOnce = true;
            
            
            // BUGS
            // No rotation unless LocalToParent
            // stackOverflow with LocalToParent
            
            
        }

    }
}