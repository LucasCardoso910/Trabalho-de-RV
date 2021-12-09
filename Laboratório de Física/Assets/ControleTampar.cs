using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleTampar : MonoBehaviour
{
    public Animator anim;
    private bool controler;
    private bool cooldown_anim = false;

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

    public void ResetCooldown(){
        cooldown_anim = false;
    }
    public void SwitchControler()
    {
        if(cooldown_anim == false){
            if (controler)
            {
                controler = false;
            }
            else
            {
                controler = true;
            }
        
        Invoke("ResetCooldown", 2.0f);
        cooldown_anim = true;
        }
    }
}
