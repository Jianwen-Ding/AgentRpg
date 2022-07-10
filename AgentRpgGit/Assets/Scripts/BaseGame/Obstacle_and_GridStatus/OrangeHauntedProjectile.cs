using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeHauntedProjectile : BaseStatus
{
    public int xChange;
    public int yChange;
    GridLoad GridAll;
    public override void Start()
    {
        Grid_Info = gameObject.GetComponent<GridControl>();
        GridAll = Camera.main.gameObject.GetComponent<GridLoad>();
        enemyMinusPriority = 0;
        EventAcsess = Camera.main.gameObject.GetComponent<MoveSystem>().EventDisplayer.GetComponent<EventSystem>();
        PreviouslyActiveEvent = false;
        TurnsTillDissapearLeft = 3;
        HasTriggered = false;
        HasSetUp = false;
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if ((int)Grid_Info.GridCoordinate.y + y >= 0 && (int)Grid_Info.GridCoordinate.y + y < GridAll.YWidthPublic && (int)Grid_Info.GridCoordinate.x + x >= 0 && (int)Grid_Info.GridCoordinate.x + x < GridAll.XWidthPublic && GridAll.AllGrids[(int)Grid_Info.GridCoordinate.y + y][(int)Grid_Info.GridCoordinate.x + x].GetComponent<GridControl>().StatusIndex == 10)
                {
                    xChange = -x;
                    yChange = -y;
                }
               
            }
        }
    }
    public override void ObjectTrigger()
    {
        if (Grid_Info.CharacterOn != null && Grid_Info.CharacterOn.GetComponent<CharacterBase>().IsEnemy == false)
        {
            CharacterBase Character_Info = Grid_Info.CharacterOn.GetComponent<CharacterBase>();
            Character_Info.Health -= 20;
            gameObject.GetComponent<GridControl>().StatusIndex = 0;
            gameObject.GetComponent<ObstacleInsert>().WipeStatus();
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

            if ((int)Grid_Info.GridCoordinate.y + yChange >= 0 && (int)Grid_Info.GridCoordinate.y + yChange < GridAll.YWidthPublic && (int)Grid_Info.GridCoordinate.x + xChange >= 0 && (int)Grid_Info.GridCoordinate.x + xChange < GridAll.XWidthPublic)
            {
                if(GridAll.AllGrids[(int)Grid_Info.GridCoordinate.y + yChange][(int)Grid_Info.GridCoordinate.x + xChange].GetComponent<GridControl>().StatusIndex != 11 && !(xChange == 0 && yChange == 0))
                {
                    GridAll.AllGrids[(int)Grid_Info.GridCoordinate.y + yChange][(int)Grid_Info.GridCoordinate.x + xChange].GetComponent<ObstacleInsert>().WipeStatus();
                    GridAll.AllGrids[(int)Grid_Info.GridCoordinate.y + yChange][(int)Grid_Info.GridCoordinate.x + xChange].GetComponent<GridControl>().StatusIndex = 11;
                }
                else if (GridAll.AllGrids[(int)Grid_Info.GridCoordinate.y + yChange][(int)Grid_Info.GridCoordinate.x + xChange].GetComponent<OrangeHauntedProjectile>() != null)
                {
                    GridAll.AllGrids[(int)Grid_Info.GridCoordinate.y + yChange][(int)Grid_Info.GridCoordinate.x + xChange].GetComponent<OrangeHauntedProjectile>().xChange = xChange;
                    GridAll.AllGrids[(int)Grid_Info.GridCoordinate.y + yChange][(int)Grid_Info.GridCoordinate.x + xChange].GetComponent<OrangeHauntedProjectile>().yChange = yChange;
                    gameObject.GetComponent<GridControl>().StatusIndex = 0;
                    gameObject.GetComponent<ObstacleInsert>().WipeStatus();
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
        if (Grid_Info.CharacterOn.GetComponent<CharacterBase>().IsEnemy == false)
        {
            EventAcsess.QueEvent(gameObject, 0, Grid_Info.CharacterOn.name + " took 20 points of damage through an orange spirit", 6);
            HasTriggered = false;
        }

    }
}
