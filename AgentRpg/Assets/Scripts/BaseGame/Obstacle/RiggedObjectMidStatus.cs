using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiggedObjectMidStatus : BaseStatus
{
    //Middle of rig, does very little damage but warns enemy not to go into it
    public override void Start()
    {
        enemyMinusPriority = 10;
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
            Character_Info.Health -= Character_Info.DefenseProcessedDamage(3);
            GameObject InWorldText;
            InWorldText = Instantiate(gameObject.GetComponent<ObstacleInsert>().TextFloatObject, new Vector3(Grid_Info.CharacterOn.transform.position.x, Grid_Info.CharacterOn.transform.position.y), Quaternion.identity.normalized);
            InWorldText.GetComponent<FadeOutText>().BeginInitiate(1, "" + Character_Info.DefenseProcessedDamage(5), Color.black, new Vector2(5, 5));
        }
    }
    public override void SetUp()
    {
        if (Grid_Info.CharacterOn.GetComponent<Rigged>() != null)
        {
            gameObject.GetComponent<GridControl>().StatusIndex = 0;
            gameObject.GetComponent<ObstacleInsert>().WipeStatus();
        }
        else
        {
            EventAcsess.QueEvent(gameObject, 0, Grid_Info.CharacterOn.name + " is burned by the tile", 6);
            HasTriggered = false;
        }
        
    }
}
