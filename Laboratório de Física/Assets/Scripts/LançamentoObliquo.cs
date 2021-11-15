using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Valve.VR.InteractionSystem;

public class LançamentoObliquo : MonoBehaviour
{
    public GameObject cannonBall;
    public GameObject cannon;
    public GameObject floor;
    public GameObject tracker;
    public LinearMapping velocityLinearMapping;
    public LinearMapping angleLinearMapping;
    public CircularDrive circularDrive;

    private float initialVelocity;
    private float cannonAngle;
    private float g = 9.8f;
    private DateTime startTime;
    private double previousTime;
    private Vector3 initialPosition;
    private float floorHeight; 
    private int fire = 0;
    private bool collided = true;

    // Start is called before the first frame update
    void Start()
    {
        startTime = DateTime.Now;
        initialPosition = cannonBall.transform.position;
        floorHeight = floor.transform.position.y;

        // linearInit = startPoint.transform.z;
        // linearEnd = endPoint.transform.z;

        initialVelocity = 10;                     //! Remove it when create controllers
        cannonAngle = radToDegrees(-28);               //! Remove it when create controllers
    }

    // Update is called once per frame
    void Update()
    {
        updateCannonAngle();
        if (!collided)
        {
            update_cannonBall();
        }
    }

    /*
     * Updates cannon ball position.
     *
     * It is calculated its position in each frame using basic physics 
     * equations. Its movement can be decompoused in two main equations:
     *
     * the x axis has a constant velocity movement, since there is no force
     * acting on it after the ball launch.
     *
     * the y axis, in other hand, has a single main force acting on the 
     * cannon ball (not considering the drag force, which is considered 
     * irrelevant): the gravity, which has a constant acceleration.
     *
     * So, to calculate the current x and y axises we can use these 
     * equations:
     *
     * x axis: X = Xo + Vo * t;
     * y axis: Y = Yo + Vo * t + (a * t^2) / 2
     *
     * More explanation is made in individual documentation.
     *
     */
    private void update_cannonBall()
    {
        double time = getTime();
        Vector3 previousPosition;

        previousPosition = cannonBall.transform.position;

        float x = initialPosition.x + calculateX(time); 
        float y = initialPosition.y + calculateY(time);
        float z = initialPosition.z;

        testForCollision(y);

        cannonBall.transform.position = new Vector3(x, y, z);
        if ((time - previousTime) > 0.2) {
            // TODO: test to use, instead of tracker, the cannonball itself
            Instantiate(tracker, previousPosition, Quaternion.identity);
            previousTime = time;
        }
    }
    
    /*
     * Calculate the current position in the x axis.
     *  This is calculated using the equation X = Vo * t
     * 
     * In this case, Vo is the initial velocity in the x axis, therefore
     * it is the object initial velocity times the angle's cos.
     * Time is the calculated time in seconds (with milliseconds) since the
     * cannon ball firing.
     *
     */
    private float calculateX(double time)
    {
        double Vo_x = initialVelocity * Math.Cos(cannonAngle);
        double new_x = Vo_x * time;

        return (float) (new_x * fire);
    }

    /*
     * Calculate the current position in the y axis.
     * This is calculated using the equation Y = Vo * t + (a * t^2) / 2
     *
     * In this case, Vo is the initial velocity in the y axis, therefore
     * it is the object initial velocity times the angle's sin.
     * Acceleration is the gravity acceleration, with a positive referencial
     * in the opposite direction of the plane.
     * Time is the calculated time in seconds (with milliseconds) since the 
     * cannon ball firing. 
     *
     */
    private float calculateY(double time)
    {
        double Vo_y = initialVelocity * Math.Sin(cannonAngle);
        double new_y = (Vo_y * time) - ((g * time * time) / 2);

        return (float) (new_y * fire);
    } 

    private void updateCannonAngle()
    {
        if (circularDrive.outAngle > 358){
            circularDrive.outAngle = 358;
            return;
        }
        
        if (circularDrive.outAngle < 2) {
            circularDrive.outAngle = 2;
            return;
        }

        cannonAngle = (float) (angleLinearMapping.value * 2 * Math.PI) / 3f;
        cannonAngle -= (float) (Math.PI / 6);

        cannon.transform.eulerAngles = new Vector3(
            -(radToDegrees(cannonAngle)),
            cannon.transform.eulerAngles.y,
            cannon.transform.eulerAngles.z
        );
        
    }

    private float radToDegrees(float radAngle)
    {
        return (radAngle * 360) / (float) (2 * Math.PI);
    }

    private float degreesToRad(float degAngle)
    {
        return (degAngle * (float) (2 * Math.PI)) / 360;
    }

    /*
     * Calculates the time of execution in the program since the last time
     * the variable startTime was setted. It gets the elapsed time in
     * milliseconds and divides it by 1000, getting the value in seconds, 
     * but with its milliseconds information.
     *
     * This is important so the movement is smooth, with a false sense of 
     * continuous movement, if the time was purely in seconds, it would be a 
     * series of teletransportations. If the time was purely in milliseconds
     * it would be way too fast.
     */
    private double getTime()
    {
        return (((DateTime.Now - startTime)).TotalMilliseconds) / 1000;
    }

    /*
     * Checks if the current height of the object is equal or less than the
     * floor height, if so, stop movement.
     *
     * This is used instead of the collision checker in the Rigidbody
     * component because of constant failures in which the ball just doesn't
     * stop its movement.
     *
     */
    private void testForCollision(float currentHeight)
    {
        // TODO: Remove this hardcoded number
        if ((currentHeight - 0.5) <= floorHeight)
        {
            collided = true;
        }
    }

    /*
     * This function is supposed to be called on the press of a button.
     * It fires the cannon ball by setting again the boolean collided 
     * variable, setting again the startTime and changing the fire value
     * to 1, so the add values to the x and y axis is not 0.
     */
    public void fireButton(bool delete)
    {
        if (delete) {
            GameObject[] trackers;
            trackers = GameObject.FindGameObjectsWithTag("Tracker");
            foreach(GameObject tracker in trackers)
            {
                Destroy(tracker);
            }
        }

        if (collided == true)
        {
            initialVelocity = velocityLinearMapping.value * 10 * cannonBall.transform.localScale.x;
            collided = false;
            startTime = DateTime.Now;
            fire = 1;
            playSound();
            previousTime = getTime();
        }
    }

    private void playSound()
    {
        AudioSource audioData;

        audioData = cannon.GetComponent<AudioSource>();
        audioData.Play(0);
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.name == "Plano")
    //     {
    //         collided = true;
    //     }
    // }
}