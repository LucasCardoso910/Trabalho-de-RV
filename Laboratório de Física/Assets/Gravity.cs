using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public GameObject dude;
    private Rigidbody rb;
    public GameObject dude1;
    private Rigidbody rb1;
    public bool ehVacuo = true;
    public bool cooldown_change_drag = false;
    public bool cooldown_reset_position = false;
    public bool cooldown_enable_gravity = false;

    public Vector3 pos_dude;
    public Vector3 pos_dude1;
    public Vector3 angle_dude;
    public Vector3 angle_dude1;



    // Start is called before the first frame update
    void Start()
    {
        rb = dude.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb1 = dude1.GetComponent<Rigidbody>();
        rb1.useGravity = false;

        pos_dude = dude.transform.position;
        pos_dude1 = dude1.transform.position;
        angle_dude = dude.transform.eulerAngles;
        angle_dude1 = dude1.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPosition()
    {
            dude.transform.position = pos_dude;
            dude1.transform.position = pos_dude1; 
            dude.transform.eulerAngles = angle_dude;
            dude1.transform.eulerAngles = angle_dude1;
    }

    public void EnableGravity()
    {
            rb.useGravity = true;
            rb1.useGravity = true;
    }

    public void DisableGravity()
    {
        rb.useGravity = false;
        rb1.useGravity = false;

    }

    public void ResetPosition()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb1.velocity = new Vector3(0, 0, 0);
        DisableGravity();
        SetPosition();
    }

    public void ResetCooldown_change_drag(){
        cooldown_change_drag = false;
    }

    public void changeDrag()
    {
        if (cooldown_change_drag == false){
            if (ehVacuo)
            {
                ehVacuo = false;
                rb.drag = 0.2F;
                rb1.drag = 2;
            }
            else
            {
                ehVacuo = true;
                rb.drag = 0;
                rb1.drag = 0;
            }

            Invoke("ResetCooldown_change_drag", 5.0f);
            cooldown_change_drag = true;
        }
    }
}