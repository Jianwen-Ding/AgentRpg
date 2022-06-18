using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageStatus : BaseCharacterStatus
{
    //Assumes its kelly
    CharacterBase characterCaughtBy;
    float beforeHealth;
    float DamageWillGet;
    public override void Start()
    {
        characterCaughtBy = GameObject.Find("Kelly").GetComponent<CharacterBase>();
        Character_Info = gameObject.GetComponent<CharacterBase>();
        GridInfo = Camera.main.gameObject.GetComponent<GridLoad>();
        EventAcsess = Camera.main.gameObject.GetComponent<MoveSystem>().EventDisplayer.GetComponent<EventSystem>();
        PreviouslyActiveEvent = false;
        TurnsTillDissapearLeft = 4;
        HasTriggered = false;
        HasSetUp = false;
        beforeHealth = characterCaughtBy.Health;
    }
    public override void SetUp()
    {
        DamageWillGet = (int)(beforeHealth - characterCaughtBy.Health);
        if(DamageWillGet > 0)
        {
            EventAcsess.QueEvent(gameObject, 0, "The " + DamageWillGet + " points of damage taken by " + characterCaughtBy.name + " was reflected onto " + Character_Info.name , 6);
            HasTriggered = false;

        }
        beforeHealth = characterCaughtBy.Health;
    }
    public override void ObjectTrigger()
    {
        Character_Info.Health -= DamageWillGet;
    }
}
