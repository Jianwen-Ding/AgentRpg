using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistyStatus : BaseStatus
{

    //Beggining of rig, does very little damage
    public override void Start()
    {
        enemyMinusPriority = 10;
        EventAcsess = Camera.main.gameObject.GetComponent<MoveSystem>().EventDisplayer.GetComponent<EventSystem>();
        PreviouslyActiveEvent = false;
        TurnsTillDissapearLeft = 3;
        HasTriggered = false;
        HasSetUp = false;
    }
    public override void ObjectTrigger()
    {
        if (Grid_Info.CharacterOn != null)
        {
            CharacterBase Character_Info = Grid_Info.CharacterOn.GetComponent<CharacterBase>();
            Character_Info.SpeedMultiplier -= (float)0.075;
        }
    }
    public override void SetUp()
    {
            EventAcsess.QueEvent(gameObject, 0, Grid_Info.CharacterOn.name + " is slowed by the mist", 6);
            HasTriggered = false;
    }
}
