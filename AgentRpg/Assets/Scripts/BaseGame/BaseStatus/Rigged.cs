using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigged : BaseCharacterStatus
{
    Vector2 LastLocation;

    // Update is called once per frame
    public override void Start()
    {

        base.Start();
        TurnsTillDissapearLeft = 4;
        LastLocation = new Vector2(0, 0);
    }
    public virtual void SetUp()
    {
        EventAcsess.QueEvent(gameObject, 0, gameObject.GetComponent<BaseCharacterStatus>().GetType().Name + "'s bomb will explode in " + TurnsTillDissapearLeft + " turns", 7);
        HasTriggered = false;

    }
    public override void ObjectTrigger()
    {
    }
    public override void Update()
    {
        if(LastLocation.x != Character_Info.CharacterLocationIndex.x || LastLocation.y != Character_Info.CharacterLocationIndex.y)
        {
            if (TurnsTillDissapearLeft > 3)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        if (Character_Info.CharacterLocationIndex.x + x < GridInfo.XWidthPublic && Character_Info.CharacterLocationIndex.x + x >= 0 && Character_Info.CharacterLocationIndex.y + y < GridInfo.YWidthPublic && Character_Info.CharacterLocationIndex.y + y >= 0)
                        {
                            GridInfo.AllGrids[(int)Character_Info.CharacterLocationIndex.y + y][(int)Character_Info.CharacterLocationIndex.x + x].GetComponent<GridControl>().StatusIndex = 2;
                        }
                    }
                }
            }
            else
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        if (Character_Info.CharacterLocationIndex.x + x < GridInfo.XWidthPublic && Character_Info.CharacterLocationIndex.x + x >= 0 && Character_Info.CharacterLocationIndex.y + y < GridInfo.YWidthPublic && Character_Info.CharacterLocationIndex.y + y >= 0)
                        {
                            GridInfo.AllGrids[(int)Character_Info.CharacterLocationIndex.y + y][(int)Character_Info.CharacterLocationIndex.x + x].GetComponent<GridControl>().StatusIndex = 3;
                        }
                    }
                }
            }

        }
        LastLocation = Character_Info.CharacterLocationIndex;
        if (TurnsTillDissapearLeft <= 0)
        {
            print("yd");
            gameObject.GetComponent<BaseCharacterStatusInsert>().WipeStatus(indexInCharacter);
            gameObject.GetComponent<CharacterBase>().StatusEffects[indexInCharacter] = 0;
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (Character_Info.CharacterLocationIndex.x + x < GridInfo.XWidthPublic && Character_Info.CharacterLocationIndex.x + x >= 0 && Character_Info.CharacterLocationIndex.y + y < GridInfo.YWidthPublic && Character_Info.CharacterLocationIndex.y + y >= 0)
                    {
                        GridInfo.AllGrids[(int)Character_Info.CharacterLocationIndex.y + y][(int)Character_Info.CharacterLocationIndex.x + x].GetComponent<GridControl>().StatusIndex = 4;
                    }
                }
            }

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
            if (TurnsTillDissapearLeft > 3)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        if (Character_Info.CharacterLocationIndex.x + x < GridInfo.XWidthPublic && Character_Info.CharacterLocationIndex.x + x >= 0 && Character_Info.CharacterLocationIndex.y + y < GridInfo.YWidthPublic && Character_Info.CharacterLocationIndex.y + y >= 0)
                        {
                            GridInfo.AllGrids[(int)Character_Info.CharacterLocationIndex.y + y][(int)Character_Info.CharacterLocationIndex.x + x].GetComponent<ObstacleInsert>().WipeStatus();
                            GridInfo.AllGrids[(int)Character_Info.CharacterLocationIndex.y + y][(int)Character_Info.CharacterLocationIndex.x + x].GetComponent<ObstacleInsert>().DirectInsertIntoSelf(2);
                        }
                    }
                }
            }
            else
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        if (Character_Info.CharacterLocationIndex.x + x < GridInfo.XWidthPublic && Character_Info.CharacterLocationIndex.x + x >= 0 && Character_Info.CharacterLocationIndex.y + y < GridInfo.YWidthPublic && Character_Info.CharacterLocationIndex.y + y >= 0)
                        {
                            GridInfo.AllGrids[(int)Character_Info.CharacterLocationIndex.y + y][(int)Character_Info.CharacterLocationIndex.x + x].GetComponent<ObstacleInsert>().WipeStatus();
                            GridInfo.AllGrids[(int)Character_Info.CharacterLocationIndex.y + y][(int)Character_Info.CharacterLocationIndex.x + x].GetComponent<ObstacleInsert>().DirectInsertIntoSelf(3);
                        }
                    }
                }
            }
        }
        
        PreviouslyActiveEvent = EventAcsess.active;
        if (EventAcsess.CheckQue(gameObject, 0) && HasTriggered == false)
        {
            ObjectTrigger();
            HasTriggered = true;
        }
    }
}
