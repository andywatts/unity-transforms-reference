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
            
            var cubePrefab = GetSingleton<CubePrefab>().Value;
            var cube = EntityManager.Instantiate(cubePrefab);
            
            var parentPrefab = GetSingleton<ParentPrefab>().Value;
            var parent = EntityManager.Instantiate(parentPrefab);
            
            EntityManager.AddComponentData(cube, new Parent {Value = cube});
            EntityManager.AddComponentData(cube, new LocalToParent() {});
            var children = EntityManager.AddBuffer<Child>(parent);
            children.Add(new Child {Value = cube});

            var q = quaternion.EulerXYZ(0, 45 * Mathf.Deg2Rad, 0);
            SetComponent(parent, new Rotation {Value = q});
            runOnce = true;
        }

    }
}