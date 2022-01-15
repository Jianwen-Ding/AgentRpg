using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBase : MonoBehaviour
{
    public bool ButtonActivate;
    
    public GameObject UIBase;
    public void ActivateButton()
    {
        if (Time.timeScale != 0)
        {
            ButtonActivate = true;
        }
        
        
    }
}
