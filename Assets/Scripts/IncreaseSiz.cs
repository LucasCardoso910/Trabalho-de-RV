using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSiz : MonoBehaviour
{
    public GameObject foreground;
    
    void start()
    {

    }
    
    void Update()
    {
        esticar();
    }

    public void esticar() 
    {
      foreground.gameObject.transform.localScale += new Vector3(0.1F,0.1F,0F);
    }
}
