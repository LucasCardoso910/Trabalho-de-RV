using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR.InteractionSystem;

public class spaceShipVelocity : MonoBehaviour
{
    public GameObject spaceShip;
    public float speed;
    //public LinearMapping pointer;
    void Update()
    {
        move_rocket();
    }

    void Start()
    {
        speed = 0;
    }

    void move_rocket(){
        //speed = changeSpeed();
        spaceShip.transform.position = new Vector3 (
            spaceShip.transform.position.x,
            spaceShip.transform.position.y + speed * Time.deltaTime,
            spaceShip.transform.position.z
        );
    }

    /*float changeSpeed()
    {
        return (pointer.value) * 100;
    }*/
}