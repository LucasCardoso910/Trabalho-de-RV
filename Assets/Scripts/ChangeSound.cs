using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ChangeSound : MonoBehaviour
{
    public LinearMapping pointer;
    public float newVolume;
    public AudioSource asour;

    void Start()
    {
        
    }

    void Update()
    {
        newVolume = changePointerValue();
        asour.volume = newVolume;
    }
    
    float changePointerValue()
    {
        return pointer.value;
    }
}
