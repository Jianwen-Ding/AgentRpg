using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartshotPoision : BaseCharacterStatus
{
    public override void Start()
    {
        Character_Info = gameObject.GetComponent<CharacterBase>();
        GridInfo = Camera.main.gameObject.GetComponent<GridLoad>();
        EventAcsess = Camera.main.gameObject.GetComponent<MoveSystem>().EventDisplayer.GetComponent<EventSystem>();
        PreviouslyActiveEvent = false;
        TurnsTillDissapearLeft = 4;
        HasTriggered = false;
        HasSetUp = false;
    }
    public override void ObjectTrigger()
    {
        Character_Info.Health -= 3;
    }
    public override void SetUp()
    {
        EventAcsess.QueEvent(gameObject, 0, gameObject.GetComponent<BaseCharacterStatus>().GetType().Name + " was effected by poision", 6);
        HasTriggered = false;

    }
}
