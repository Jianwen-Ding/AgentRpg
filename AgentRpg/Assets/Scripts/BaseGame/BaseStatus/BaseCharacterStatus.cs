using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterStatus : MonoBehaviour
{
    public int TurnsTillDissapearLeft;
    public bool PreviouslyActiveEvent;
    public EventSystem EventAcsess;
    public bool HasTriggered;
    public bool HasSetUp;
    public int indexInCharacter;
    public GridLoad GridInfo;
    public CharacterBase Character_Info;
    // Start is called before the first frame update
    public virtual void Start()
    {
        Character_Info = gameObject.GetComponent<CharacterBase>();
        GridInfo = Camera.main.gameObject.GetComponent<GridLoad>();
        EventAcsess = Camera.main.gameObject.GetComponent<MoveSystem>().EventDisplayer.GetComponent<EventSystem>();
        PreviouslyActiveEvent = false;
        TurnsTillDissapearLeft = 1;
        HasTriggered = false;
        HasSetUp = false;
    }
    public virtual void ObjectTrigger()
    {

    }
    public virtual void SetUp()
    {
        EventAcsess.QueEvent(gameObject, 0, gameObject.GetComponent<BaseCharacterStatus>().GetType().Name + " effects " + gameObject.name, 6);
        HasTriggered = false;

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (TurnsTillDissapearLeft <= 0)
        {
            gameObject.GetComponent<BaseCharacterStatusInsert>().WipeStatus(indexInCharacter);
            
            Character_Info.StatusEffects[indexInCharacter] = 0;

        }
        if (TurnsTillDissapearLeft > 0 && HasSetUp == false)
        {
            SetUp();
            HasSetUp = true;
        }
        if (EventAcsess.active == false && PreviouslyActiveEvent == true && TurnsTillDissapearLeft > 0)
        {
            TurnsTillDissapearLeft -= 1;
            HasSetUp = false;
        }
        PreviouslyActiveEvent = EventAcsess.active;
        if (EventAcsess.CheckQue(gameObject, 0) && HasTriggered == false)
        {
            ObjectTrigger();
            HasTriggered = true;
        }
    }
}
