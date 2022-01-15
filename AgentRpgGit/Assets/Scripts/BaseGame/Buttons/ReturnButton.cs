using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : MonoBehaviour
{
    [SerializeField]
    ButtonBase ButtonBaseThing;
    // Update is called once per frame
    void Update()
    {
        if (ButtonBaseThing.ButtonActivate)
        {
            ButtonBaseThing.UIBase.GetComponent<MovementUI>().Scenes = "main";
            ButtonBaseThing.UIBase.GetComponent<MovementUI>().HasEstablishedScene = false;
        }
    }
}
