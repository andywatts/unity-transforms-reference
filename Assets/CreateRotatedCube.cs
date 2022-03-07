using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRotatedCube : MonoBehaviour
{
    public GameObject prefab;

    void Start()
    {
        var cube = GameObject.Instantiate(prefab);
        // cube.transform.rotation = Quaternion.AngleAxis(45, Vector3.up); // Works
        cube.transform.rotation = Quaternion.Euler(0, 45, 0); // Works
    }

}