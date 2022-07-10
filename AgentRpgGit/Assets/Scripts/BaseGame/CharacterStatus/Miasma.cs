using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miasma : BaseCharacterStatus
{
    public int miasmaDamage = 1;
    public override void Start()
    {
        
        Character_Info = gameObject.GetComponent<CharacterBase>();
        GridInfo = Camera.main.gameObject.GetComponent<GridLoad>();
        EventAcsess = Camera.main.gameObject.GetComponent<MoveSystem>().EventDisplayer.GetComponent<EventSystem>();
        PreviouslyActiveEvent = false;
        TurnsTillDissapearLeft = 5;
        HasTriggered = false;
        HasSetUp = false;
    }
    public override void ObjectTrigger()
    {
        Character_Info.Health -= miasmaDamage;
    }
    public override void SetUp()
    {
        EventAcsess.QueEvent(gameObject, 0, "The miasma does " + miasmaDamage + " points of damage to " + Character_Info.name, 6);
        HasTriggered = false;

    }
}
