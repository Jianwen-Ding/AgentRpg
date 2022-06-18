using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveMain : BaseStatus
{
    GridLoad GridAll;
    public override void Start()
    {
        Grid_Info = gameObject.GetComponent<GridControl>();
        GridAll = Camera.main.gameObject.GetComponent<GridLoad>();
        enemyMinusPriority = 0;
        EventAcsess = Camera.main.gameObject.GetComponent<MoveSystem>().EventDisplayer.GetComponent<EventSystem>();
        PreviouslyActiveEvent = false;
        TurnsTillDissapearLeft = 2;
        HasTriggered = false;
        HasSetUp = false;
    }
    public override void ObjectTrigger()
    {
        if (Grid_Info.CharacterOn != null && Grid_Info.CharacterOn.GetComponent<CharacterBase>().IsEnemy == false)
        {
            CharacterBase Character_Info = Grid_Info.CharacterOn.GetComponent<CharacterBase>();
            Character_Info.SpeedMultiplier -= (float)0.05;
            Character_Info.DefenseMultiplier -= (float)0.05;
        }
    }

    public override void Update()
    {
        if (Grid_Info == null)
        {
            Grid_Info = gameObject.GetComponent<GridControl>();
        }
        if (TurnsTillDissapearLeft <= 0)
        {
            gameObject.GetComponent<GridControl>().StatusIndex = 0;
            gameObject.GetComponent<ObstacleInsert>().WipeStatus();
        }
        if (Grid_Info.CharacterOn != null)
        {
            //Help debugging
            if (Grid_Info != null && TurnsTillDissapearLeft > 0 && HasSetUp == false && HasTriggered == false)
            {
                SetUp();
                HasSetUp = true;
            }
        }

        if (EventAcsess.active == false && PreviouslyActiveEvent == true && TurnsTillDissapearLeft > 0)
        {
            if (TurnsTillDissapearLeft == 2)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        if ((int)Grid_Info.GridCoordinate.y + y >= 0 && (int)Grid_Info.GridCoordinate.y + y < GridAll.YWidthPublic && (int)Grid_Info.GridCoordinate.x + x >= 0 && (int)Grid_Info.GridCoordinate.x + x < GridAll.XWidthPublic && GridAll.AllGrids[(int)Grid_Info.GridCoordinate.y + y][(int)Grid_Info.GridCoordinate.x + x].GetComponent<GridControl>().StatusIndex == 0)
                        {
                            GridAll.AllGrids[(int)Grid_Info.GridCoordinate.y + y][(int)Grid_Info.GridCoordinate.x + x].GetComponent<ObstacleInsert>().WipeStatus();
                            GridAll.AllGrids[(int)Grid_Info.GridCoordinate.y + y][(int)Grid_Info.GridCoordinate.x + x].GetComponent<GridControl>().StatusIndex = 9;
                        }

                    }
                }
            }
            TurnsTillDissapearLeft -= 1;
            HasTriggered = false;
            HasSetUp = false;
        }
        PreviouslyActiveEvent = EventAcsess.active;
        if (EventAcsess.CheckQue(gameObject, 0) && HasTriggered == false)
        {
            ObjectTrigger();
            HasTriggered = true;
        }

    }
    public override void SetUp()
    {
        if(Grid_Info.CharacterOn.GetComponent<CharacterBase>().IsEnemy == false)
        {
            EventAcsess.QueEvent(gameObject, 0, Grid_Info.CharacterOn.name + " was caught in the shockwave", 6);
            HasTriggered = false;
        }
       
    }
}
