using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR.InteractionSystem;

public class RotatorRocket : MonoBehaviour
{
    public GameObject rocket;
    public Vector3 axis;
    public double speed;
    private double radius;
    public LinearMapping linear_mapping;

    public double angular_speed;
    private float timeCounter=0;

    void Start()
    {
        float x = rocket.transform.localPosition.x;
        float y = rocket.transform.localPosition.y;
        float z = rocket.transform.localPosition.z;

        radius = Math.Sqrt(Math.Pow(x,2) + Math.Pow(y,2) + Math.Pow(z,2));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(axis, ((float) angular_speed) * Time.deltaTime);
        angular_speed = changeAngularSpeed();
        updateSpeed();
        /*
        timeCounter += time.deltaTime;

        float x = Math.Cos(timeCounter);
        float y = 1;
        float z = Math.Sin(timeCounter);

        transform.position = new Vector3(x, y, z);
        */
    }
    
    void updateSpeed() {
        speed = degreesToRad(angular_speed) * radius;
    }

    double degreesToRad(double degAngle)
    {
        return (degAngle * (2 * Math.PI)) / 360;
    }

    float changeAngularSpeed()
    {
        return linear_mapping.value * 250;
    }
}