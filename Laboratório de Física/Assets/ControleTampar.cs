using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleTampar : MonoBehaviour
{
    public Animator anim;
    private bool controler;

    // Update is called once per frame
    void Update()
    {
        if (controler)
        {
            anim.SetBool("controleTampa", false);
        }
        else
        {
            anim.SetBool("controleTampa", true);
        }
    }

    public void SwitchControler()
    {
        if (controler)
        {
            controler = false;
        }
        else
        {
            controler = true;
        }
    }
}
