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

    // Start is called before the first frame update
    void Start()
    {
        rb = dude.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb1 = dude1.GetComponent<Rigidbody>();
        rb1.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPosition()
    {
        dude.transform.position = new Vector3(40, 18, 14);
        dude1.transform.position = new Vector3(30, 18, 14);
        dude.transform.eulerAngles = new Vector3(0, -90, 90);
        dude1.transform.eulerAngles = new Vector3(-90, 0, 90);

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
        DisableGravity();
        SetPosition();
    }

    public void changeDrag()
    {
        if (ehVacuo == false)
        {
            ehVacuo = true;
        }
        else if (ehVacuo == true)
        {
            ehVacuo = false;
        }

        if (ehVacuo == false)
        {
            rb.drag = 0.2F;
            rb1.drag = 2;
        }
        else {
            rb.drag = 0;
            rb1.drag = 0;
        }
    }
}