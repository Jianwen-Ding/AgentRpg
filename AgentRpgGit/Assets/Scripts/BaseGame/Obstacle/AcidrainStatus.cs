using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidrainStatus : BaseStatus
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
            Character_Info.DefenseMultiplier -= (float)0.075;
        }
    }
    public override void SetUp()
    {
        EventAcsess.QueEvent(gameObject, 0, Grid_Info.CharacterOn.name + "'s defences were weakened by the acidic rain", 6);
        HasTriggered = false;
    }
}
