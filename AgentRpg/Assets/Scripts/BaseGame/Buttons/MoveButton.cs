using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButton : MonoBehaviour
{
    [SerializeField]
    ButtonBase ButtonBaseThing;
    // Update is called once per frame
    void Update()
    {
        if (ButtonBaseThing.ButtonActivate)
        {
            ButtonBaseThing.UIBase.GetComponent<MovementUI>().Scenes = "Move";
            ButtonBaseThing.UIBase.GetComponent<MovementUI>().HasEstablishedScene = false;
        }
    }
}
