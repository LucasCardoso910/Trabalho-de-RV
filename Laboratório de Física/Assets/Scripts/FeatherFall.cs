using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float velocity = 5;
    public float factor = 5;
    public GameObject feather;
    public GameObject floor;

    private Rigidbody teste;
    private DateTime startTime;
    private Vector3 initialPosition;
    private float floorHeight;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startTime = DateTime.Now;
        initialPosition = feather.transform.position;

        floorHeight = floor.transform.position.y + (floor.transform.localScale.y / 2);
    }

    // Update is called once per frame
    void Update()
    {
        double time = getTime();

        Vector3 currentPosition = feather.transform.position;
        float x = initialPosition.x + (float) (factor * Math.Sin((time) / 500));
        float y = initialPosition.y - calculateHeight((float) time);
        float z = initialPosition.z;

        if (y <= floorHeight) {
            y = floorHeight;
            x = currentPosition.x;
            z = currentPosition.z;
        }

        feather.transform.position = new Vector3(x, y, z);
    }

    float calculateHeight(float t) {
        return (t * velocity) / 1000.0f;
    } 

    double getTime() {
        return ((DateTime.Now - startTime).TotalMilliseconds);
    }
}
