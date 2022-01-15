using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiggedObjectEndStatus : BaseStatus
{
    //Blows up
    public override void Start()
    {
        enemyMinusPriority = 30;
        EventAcsess = Camera.main.gameObject.GetComponent<MoveSystem>().EventDisplayer.GetComponent<EventSystem>();
        PreviouslyActiveEvent = false;
        TurnsTillDissapearLeft = 1;
        HasTriggered = false;
        HasSetUp = false;
    }
    public override void ObjectTrigger()
    {
        if (Grid_Info.CharacterOn != null)
        {
            CharacterBase Character_Info = Grid_Info.CharacterOn.GetComponent<CharacterBase>();
            Character_Info.Health -= Character_Info.DefenseProcessedDamage(50);
            GameObject InWorldText;
            InWorldText = Instantiate(gameObject.GetComponent<ObstacleInsert>().TextFloatObject, new Vector3(Grid_Info.CharacterOn.transform.position.x, Grid_Info.CharacterOn.transform.position.y), Quaternion.identity.normalized);
            InWorldText.GetComponent<FadeOutText>().BeginInitiate(1, "" + Character_Info.DefenseProcessedDamage(50), Color.black, new Vector2(5, 5));
        }
    }
    public override void SetUp()
    {
        EventAcsess.QueEvent(gameObject, 0, Grid_Info.CharacterOn.name + " got hit with an explosion", 6);
        HasTriggered = false;
    }
}
