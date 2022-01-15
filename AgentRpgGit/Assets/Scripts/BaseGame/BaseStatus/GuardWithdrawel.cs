using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardWithdrawel : BaseCharacterStatus
{
    public override void Start()
    {
        Character_Info = gameObject.GetComponent<CharacterBase>();
        GridInfo = Camera.main.gameObject.GetComponent<GridLoad>();
        EventAcsess = Camera.main.gameObject.GetComponent<MoveSystem>().EventDisplayer.GetComponent<EventSystem>();
        PreviouslyActiveEvent = false;
        TurnsTillDissapearLeft = 2;
        HasTriggered = false;
        HasSetUp = false;
    }
    public override void ObjectTrigger()
    {
        Character_Info.DefenseMultiplier -= (float)0.7;
        gameObject.GetComponent<BaseCharacterStatusInsert>().WipeStatus(indexInCharacter);
        Character_Info.StatusEffects[indexInCharacter] = 0;
    }
    public override void SetUp()
    {
        if(TurnsTillDissapearLeft == 1)
        {
            EventAcsess.QueEvent(gameObject, 0, gameObject.name + " was weakend by the withdrawel from GUARD", 6);
            HasTriggered = false;
        }
    }
}
