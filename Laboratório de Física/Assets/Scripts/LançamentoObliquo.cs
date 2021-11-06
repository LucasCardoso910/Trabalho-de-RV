using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// O que fazer:
// Lançamento de uma partícula;
// Controlar o lançamento com um botão;
// Controlar a angulação inicial do canhão e a velocidade da partícula.

// X = Xo + ( Vo * cos(alpha) ) * t;

// Y = Yo + (Vo * sin(alpha)) * t - (g * t²)/2
public class LançamentoObliquo : MonoBehaviour
{
    public GameObject cannonBall;

    public float Vo;        //! passar para private

    public float alpha;     //! passar para private

    private float g = 9.8f;

    private Rigidbody rigidbody; 
    private DateTime startTime;
    private Vector3 initialPosition;
    private int fire = 0;
    private bool collided = false;
    
    float calculateX(double time)
    {
        double Vo_x = Vo * Math.Cos(alpha);
        double new_x = Vo_x * time;

        return (float) (new_x * fire);
    } 

    float calculateY(double time)
    {
        /*
         *
         * Calculate the current position in the y axis.
         * This is calculated using the equation Y = Vo * t + (a * t^2) / 2
         *
         * In this case, Vo is the initial velocity in the y axis, therefore
         * it is the object initial velocity times the sin angle.
         * Acceleration is the gravity acceleration, with a positive referencial
         * in the opposite direction of the plane.
         * Time is the calculated time in seconds since the cannon ball firing. 
         *
         */
        
        double Vo_y = Vo * Math.Sin(alpha);
        double new_y = (Vo_y * time) - ((g * time * time) / 2);

        return (float) (new_y * fire);
    }

    void update_cannonBall()
    {
        double time = getTime();

        Vector3 currentPosition = cannonBall.transform.position;

        float x = initialPosition.x + calculateX(time); 
        float y = initialPosition.y + calculateY(time);
        float z = initialPosition.z;

        // Check for collision

        cannonBall.transform.position = new Vector3(x, y, z); 
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = DateTime.Now;
        rigidbody = GetComponent<Rigidbody>();
        initialPosition = cannonBall.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!collided) {
            update_cannonBall();
        }
    }

    public void fireButton()
    {
        collided = false;
        startTime = DateTime.Now;
        fire = 1;
    }

    double getTime()
    {
        return (((DateTime.Now - startTime)).TotalMilliseconds) / 1000;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plano")
        {
            collided = true;
        }
    }
}