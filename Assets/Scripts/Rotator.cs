using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Rotator : MonoBehaviour
{
    public Vector3 axis;
    public float speed;
    public LinearMapping pointer;

    // Update is called once per frame
    void Update()
    {
        speed = changeSpeed();
        transform.Rotate(axis, speed * Time.deltaTime);
    }

    float changeSpeed()
    {
        return (pointer.value)*100;
    }
}
