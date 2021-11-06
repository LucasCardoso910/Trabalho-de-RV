using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySemVacuo : MonoBehaviour
{
    public GameObject dude;
    private Rigidbody rb;
    public GameObject dude1;
    private Rigidbody rb1;

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
        dude.transform.position = new Vector3(-31, 9 , 14);
        dude1.transform.position = new Vector3(-35,9,14);
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
}
