using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAi : BotAi
{
    //Should be same as Bot AI but without regular move
    public override void Update()
    {
        RangeIn = InOpponentRange((int)CharacterInfo.CharacterLocationIndex.x, (int)CharacterInfo.CharacterLocationIndex.y);
        /*InHitDistance = false;
        for (int x = 0; x < InOpponentShootCheck((int)CharacterInfo.CharacterLocationIndex.x, (int)CharacterInfo.CharacterLocationIndex.y).Length; x++)
        {
            if (-69 != InOpponentShootCheck((int)CharacterInfo.CharacterLocationIndex.x, (int)CharacterInfo.CharacterLocationIndex.y)[x])
            {
                InHitDistance = true;
            }
        }*/
        if (GridControlInfo == null)
        {
            GridControlInfo = new GridControl[GridInfo.AllGrids.Length][];
            for (int i = 0; i < GridInfo.AllGrids.Length; i++)
            {
                GridControlInfo[i] = new GridControl[GridInfo.AllGrids[i].Length];
                for (int z = 0; z < GridInfo.AllGrids[i].Length; z++)
                {
                    GridControlInfo[i][z] = GridInfo.AllGrids[i][z].GetComponent<GridControl>();
                }
            }
        }
        //Intiates the decisonMaking process
        if (MoveSystemInfo.IsDisplayingHappening == false && HasStartedMakingDecison == false && CharacterInfo.IsDead == false)
        {
            int PriorityAdd = 0;
            CurrentMoveGridCreation = 1;
            InitiateMoveGridCreation = true;
            IsMakingDecison = true;
            HasStartedMakingDecison = true;
            for (int x = 0; x < WillMoveTo.Length; x++)
            {
                WillMoveTo[x] = false;
            }
            for (int x = 0; x < SuggestedAction.Length; x++)
            {
                SuggestedAction[x] = "inactive";
            }
            for (int x = 0; x < SuggestedActionLocation.Length; x++)
            {
                SuggestedActionLocation[x] = new Vector2(-69, 69);
            }
            for (int x = 0; x < SuggestedActionPriority.Length; x++)
            {
                SuggestedActionPriority[x] = -69;
            }
            PriorityDeterationSheet = new int[GridInfo.AllGrids[0].Length][];
            for (int x = 0; x < PriorityDeterationSheet.Length; x++)
            {
                PriorityDeterationSheet[x] = new int[GridInfo.AllGrids.Length];
            }
            GridSpreadSheet = new int[GridInfo.AllGrids[0].Length][];
            for (int x = 0; x < GridSpreadSheet.Length; x++)
            {
                GridSpreadSheet[x] = new int[GridInfo.AllGrids.Length];
            }
            PriorityDeterationSheet = new int[GridInfo.AllGrids[0].Length][];
            for (int x = 0; x < PriorityDeterationSheet.Length; x++)
            {
                PriorityDeterationSheet[x] = new int[GridInfo.AllGrids.Length];
                for (int y = 0; y < PriorityDeterationSheet[x].Length; y++)
                {
                    PriorityDeterationSheet[x][y] = -10000;
                }
            }
            GridSpreadSheet[(int)CharacterInfo.CharacterLocationIndex.x][(int)CharacterInfo.CharacterLocationIndex.y] = 1;
            PriorityDeterationSheet[(int)CharacterInfo.CharacterLocationIndex.x][(int)CharacterInfo.CharacterLocationIndex.y] = 0;
            int StartPriorityDeduct = 0;
            if (InOpponentRange((int)CharacterInfo.CharacterLocationIndex.x, (int)CharacterInfo.CharacterLocationIndex.y) == "TooClose")
            {
                SuggestedActionPriority[0] -= TooCloseToPriorityDedeuct;
                StartPriorityDeduct -= TooCloseToPriorityDedeuct;
            }
            if (InOpponentRange((int)CharacterInfo.CharacterLocationIndex.x, (int)CharacterInfo.CharacterLocationIndex.y) == "TooFar")
            {
                SuggestedActionPriority[0] -= TooFarToPriorityDedeuct;
                StartPriorityDeduct -= TooFarToPriorityDedeuct;
            }
            if (GridInfo.AllGrids[(int)CharacterInfo.CharacterLocationIndex.y][(int)CharacterInfo.CharacterLocationIndex.x].GetComponent<BaseStatus>() != null)
            {
                SuggestedActionPriority[0] -= GridInfo.AllGrids[(int)CharacterInfo.CharacterLocationIndex.y][(int)CharacterInfo.CharacterLocationIndex.x].GetComponent<BaseStatus>().enemyMinusPriority;
                StartPriorityDeduct -= GridInfo.AllGrids[(int)CharacterInfo.CharacterLocationIndex.y][(int)CharacterInfo.CharacterLocationIndex.x].GetComponent<BaseStatus>().enemyMinusPriority;
            }
            int[] ShootCheckResutlt = InOpponentShootCheck((int)CharacterInfo.CharacterLocationIndex.x, (int)CharacterInfo.CharacterLocationIndex.y);
            for (int x = 0; x < ShootCheckResutlt.Length; x++)
            {
                if (-69 != ShootCheckResutlt[x])
                {
                    SuggestedActionPriority[0] -= ShootCheckResutlt[x];
                    StartPriorityDeduct -= ShootCheckResutlt[x];
                }
            }
            bool IsInChargeMove = false;
            for (int z = 0; z < Opponents.Length; z++)
            {
                CharacterBase OpponentBase = Opponents[z].gameObject.GetComponent<CharacterBase>();
                for (int t = 0; t < OpponentBase.MovesAllowed.Length; t++)
                {
                    if (OpponentBase.MovesAllowed[t].WillBeUsedForCharging && OpponentBase.MovesAllowed[t].AreaSoonToEffect != null && InMultiAreaGridCheck((int)CharacterInfo.CharacterLocationIndex.x, (int)CharacterInfo.CharacterLocationIndex.y, OpponentBase.MovesAllowed[t].AreaSoonToEffect))
                    {
                        SuggestedActionPriority[0] -= ChargeMoveDedeuct;
                        StartPriorityDeduct -= ChargeMoveDedeuct;
                    }
                }

            }

            for (int i = 0; i < Priorities.Length; i++)
            {
                switch (Priorities[i])
                {
                    case "Shoot":
                        for (int t = 0; t < Opponents.Length; t++)
                        {
                            CharacterBase OpponentInformation = Opponents[t].gameObject.GetComponent<CharacterBase>();
                            GunFunction SelfGun = gameObject.GetComponent<GunFunction>();
                            if (Opponents[t].IsDead == false && PriorityDeterationSheet[(int)CharacterInfo.CharacterLocationIndex.x][(int)CharacterInfo.CharacterLocationIndex.y] + StartPriorityDeduct + ShootAdd > SuggestedActionPriority[0])
                            {
                                if (CurrentAreaShot(new Vector2((int)CharacterInfo.CharacterLocationIndex.x, (int)CharacterInfo.CharacterLocationIndex.y), (int)OpponentInformation.CharacterLocationIndex.x, (int)OpponentInformation.CharacterLocationIndex.y, 0, 1, SelfGun) != -69)
                                {
                                    if (PriorityDeterationSheet[(int)CharacterInfo.CharacterLocationIndex.x][(int)CharacterInfo.CharacterLocationIndex.y] + StartPriorityDeduct + ShootAdd > SuggestedActionPriority[SuggestedActionPriority.Length - 1])
                                    {
                                        PriorityAdd = ShootAdd;
                                    }
                                    InsertIntoList("Shoot", -((CurrentMoveGridCreation - 1) * MovementPriorityDown) + StartPriorityDeduct + ShootAdd, new Vector2(0, 1), false);
                                    if (gameObject.name == "wow")
                                    {
                                        print("NewActionSet______________________________________________");
                                        print("Suggested Action: Shoot");
                                        print("Suggested Priority " + SuggestedActionPriority);
                                        print("Suggested Priority " + SuggestedActionLocation);
                                    }
                                }
                                if (CurrentAreaShot(new Vector2((int)CharacterInfo.CharacterLocationIndex.x, (int)CharacterInfo.CharacterLocationIndex.y), (int)OpponentInformation.CharacterLocationIndex.x, (int)OpponentInformation.CharacterLocationIndex.y, -1, 0, SelfGun) != -69)
                                {
                                    if (PriorityDeterationSheet[(int)CharacterInfo.CharacterLocationIndex.x][(int)CharacterInfo.CharacterLocationIndex.y] + StartPriorityDeduct + ShootAdd > SuggestedActionPriority[SuggestedActionPriority.Length - 1])
                                    {
                                        PriorityAdd = ShootAdd;
                                    }
                                    InsertIntoList("Shoot", -((CurrentMoveGridCreation - 1) * MovementPriorityDown) + StartPriorityDeduct + ShootAdd, new Vector2(-1, 0), false);
                                    if (gameObject.name == "wow")
                                    {
                                        print("NewActionSet______________________________________________");
                                        print("Suggested Action: Shoot");
                                        print("Suggested Priority " + SuggestedActionPriority);
                                        print("Suggested Priority " + SuggestedActionLocation);
                                    }
                                }
                                if (CurrentAreaShot(new Vector2((int)CharacterInfo.CharacterLocationIndex.x, (int)CharacterInfo.CharacterLocationIndex.y), (int)OpponentInformation.CharacterLocationIndex.x, (int)OpponentInformation.CharacterLocationIndex.y, 0, -1, SelfGun) != -69)
                                {
                                    if (PriorityDeterationSheet[(int)CharacterInfo.CharacterLocationIndex.x][(int)CharacterInfo.CharacterLocationIndex.y] + StartPriorityDeduct + ShootAdd > SuggestedActionPriority[SuggestedActionPriority.Length - 1])
                                    {
                                        PriorityAdd = ShootAdd;
                                    }
                                    InsertIntoList("Shoot", -((CurrentMoveGridCreation - 1) * MovementPriorityDown) + StartPriorityDeduct + ShootAdd, new Vector2(0, -1), false);
                                    if (gameObject.name == "wow")
                                    {
                                        print("NewActionSet______________________________________________");
                                        print("Suggested Action: Shoot");
                                        print("Suggested Priority " + SuggestedActionPriority);
                                        print("Suggested Priority " + SuggestedActionLocation);
                                    }
                                }
                                if (CurrentAreaShot(new Vector2((int)CharacterInfo.CharacterLocationIndex.x, (int)CharacterInfo.CharacterLocationIndex.y), (int)OpponentInformation.CharacterLocationIndex.x, (int)OpponentInformation.CharacterLocationIndex.y, 1, 0, SelfGun) != -69)
                                {
                                    if (PriorityDeterationSheet[(int)CharacterInfo.CharacterLocationIndex.x][(int)CharacterInfo.CharacterLocationIndex.y] + StartPriorityDeduct + ShootAdd > SuggestedActionPriority[SuggestedActionPriority.Length - 1])
                                    {
                                        PriorityAdd = ShootAdd;
                                    }
                                    InsertIntoList("Shoot", -((CurrentMoveGridCreation - 1) * MovementPriorityDown) + StartPriorityDeduct + ShootAdd, new Vector2(1, 0), false);
                                    if (gameObject.name == "wow")
                                    {
                                        print("NewActionSet______________________________________________");
                                        print("Suggested Action: Shoot");
                                        print("Suggested Priority " + SuggestedActionPriority);
                                        print("Suggested Priority " + SuggestedActionLocation);
                                    }
                                }
                            }
                        }
                        break;
                    case "SpecialMove":
                        for (int t = 0; t < SpecialMoves.Length; t++)
                        {
                            int[] LocationSend = SpecialMoves[t].CheckIfConditionsApply(new Vector2((int)CharacterInfo.CharacterLocationIndex.x, (int)CharacterInfo.CharacterLocationIndex.y));
                            if (LocationSend[0] != -69 && LocationSend[1] != -69 && LocationSend[2] != 69 && PriorityDeterationSheet[(int)CharacterInfo.CharacterLocationIndex.x][(int)CharacterInfo.CharacterLocationIndex.y] + StartPriorityDeduct + LocationSend[2] > SuggestedActionPriority[0])
                            {
                                if (PriorityDeterationSheet[(int)CharacterInfo.CharacterLocationIndex.x][(int)CharacterInfo.CharacterLocationIndex.y] + StartPriorityDeduct + LocationSend[2] > SuggestedActionPriority[SuggestedActionPriority.Length - 1])

                                {
                                    PriorityAdd = LocationSend[2];
                                }
                                InsertIntoList("SPMove" + t, -((CurrentMoveGridCreation - 1) * MovementPriorityDown) + StartPriorityDeduct + LocationSend[2], new Vector2(LocationSend[0], LocationSend[1]), false);
                            }
                        }
                        break;
                }
            }
            OldActionPriority = PriorityAdd + StartPriorityDeduct;
        }
        else if (IsMakingDecison && CharacterInfo.IsCharging == true)
        {

            CharacterInfo.PushAction(SuggestedActionLocationFinal, SuggestedActionFinal, MoveSystemInfo.TextBoxLoader);
            IsMakingDecison = false;
        }
        if (MoveSystemInfo.IsDisplayingHappening == true)
        {
            HasStartedMakingDecison = false;
        }
        // DEBUG
        /*
        print("______START____________________________________________");
        for(int i = 0; i < GridSpreadSheet.Length; i++)
        {
            string GridShow = "";
            for (int z = 0; z < GridSpreadSheet[i].Length; z++)
            {
                GridShow = GridShow + GridSpreadSheet[i][z] + " // ";
            }
            print(GridShow);
        }
        print("______END____________________________________________");
        */
        if (IsMakingDecison && CharacterInfo.IsCharging == false)
        {
            if (InitiateMoveGridCreation)
            {
                Vector2[] PlaceAreaCurrent = new Vector2[100];
                for (int i = 0; i < PlaceAreaCurrent.Length; i++)
                {
                    PlaceAreaCurrent[i] = new Vector2(-69, -69);
                }
                bool PlaceAreaCurrentEmpty;
                PlaceAreaCurrentEmpty = true;
                int PlaceAreaCurrentAmount = 0;
                for (int i = 0; i < GridSpreadSheet.Length; i++)
                {
                    for (int z = 0; z < GridSpreadSheet[i].Length; z++)
                    {
                        if (GridSpreadSheet[i][z] == CurrentMoveGridCreation)
                        {
                            PlaceAreaCurrent[PlaceAreaCurrentAmount] = new Vector2(i, z);
                            PlaceAreaCurrentAmount++;
                            PlaceAreaCurrentEmpty = false;
                        }
                    }
                }
                if (PlaceAreaCurrentEmpty == false)
                {
                    for (int i = 0; i < PlaceAreaCurrent.Length; i++)
                    {
                        if (PlaceAreaCurrent[i].x != -69 && PlaceAreaCurrent[i].y != -69 && GridInfo.AllGrids[(int)PlaceAreaCurrent[i].y][(int)PlaceAreaCurrent[i].x].GetComponent<GridControl>().CharacterOn != gameObject.GetComponent<CharacterBase>())
                        {
                            int PriorityAdd = 0;
                            int PriorityDeductions = 0;

                            //"OutOfRange"
                            //"InGunRange"
                            //"TooFarInsideRange"
                            //"InQuickSpecialRange"
                            //"InChargeSpecialRange"
                            if (CurrentMoveGridCreation != 1)
                            {
                                if (InOpponentRange((int)PlaceAreaCurrent[i].x, (int)PlaceAreaCurrent[i].y) == "TooClose")
                                {
                                    PriorityDeductions -= TooCloseToPriorityDedeuct;
                                }
                                if (InOpponentRange((int)PlaceAreaCurrent[i].x, (int)PlaceAreaCurrent[i].y) == "TooFar")
                                {
                                    PriorityDeductions -= TooFarToPriorityDedeuct;
                                }
                                int[] ShootCheckStore = InOpponentShootCheck((int)PlaceAreaCurrent[i].x, (int)PlaceAreaCurrent[i].y);
                                for (int x = 0; x < ShootCheckStore.Length; x++)
                                {
                                    if (-69 != ShootCheckStore[x])
                                    {
                                        PriorityDeductions -= ShootCheckStore[x];
                                    }
                                }
                                if (GridInfo.AllGrids[(int)PlaceAreaCurrent[i].y][(int)PlaceAreaCurrent[i].x].GetComponent<BaseStatus>() != null)
                                {
                                    PriorityDeductions -= GridInfo.AllGrids[(int)PlaceAreaCurrent[i].y][(int)PlaceAreaCurrent[i].x].GetComponent<BaseStatus>().enemyMinusPriority;
                                }
                                bool IsInChargeMove = false;
                                for (int z = 0; z < Opponents.Length; z++)
                                {
                                    CharacterBase OpponentBase = Opponents[z].gameObject.GetComponent<CharacterBase>();
                                    for (int t = 0; t < OpponentBase.MovesAllowed.Length; t++)
                                    {
                                        if (OpponentBase.MovesAllowed[t].WillBeUsedForCharging && OpponentBase.MovesAllowed[t].AreaSoonToEffect != null && InMultiAreaGridCheck((int)PlaceAreaCurrent[i].x, (int)PlaceAreaCurrent[i].y, OpponentBase.MovesAllowed[t].AreaSoonToEffect))
                                        {
                                            PriorityDeductions -= ChargeMoveDedeuct;
                                        }
                                    }

                                }
                                for (int z = 0; z < Priorities.Length; z++)
                                {
                                    switch (Priorities[z])
                                    {
                                        //Dont bother applying priority deduction
                                        case "AdjustLocation":
                                            if ((int)PlaceAreaCurrent[i].x >= 0 && (int)PlaceAreaCurrent[i].x < GridInfo.AllGrids[0].Length && (int)PlaceAreaCurrent[i].y >= 0 && (int)PlaceAreaCurrent[i].y < GridInfo.AllGrids.Length)
                                            {
                                                if (PriorityDeterationSheet[(int)PlaceAreaCurrent[i].x][(int)PlaceAreaCurrent[i].y] + PriorityDeductions + MovePriorityAdd > SuggestedActionPriority[0])
                                                {
                                                    InsertIntoList("Move", -((CurrentMoveGridCreation - 1) * MovementPriorityDown) + PriorityDeductions + MovePriorityAdd, new Vector2(PlaceAreaCurrent[i].x, PlaceAreaCurrent[i].y), true);
                                                    if (PriorityDeterationSheet[(int)PlaceAreaCurrent[i].x][(int)PlaceAreaCurrent[i].y] + PriorityDeductions + MovePriorityAdd > SuggestedActionPriority[SuggestedActionPriority.Length - 1])
                                                    {
                                                        PriorityAdd = MovePriorityAdd;
                                                    }
                                                    if (gameObject.name == "wow")
                                                    {
                                                        print("NewLocationSet______________________________________________");
                                                        print("Suggested Action: AdjustLocation");
                                                        print("Suggested Priority " + SuggestedActionPriority);
                                                        print("Suggested Priority " + SuggestedActionLocation);
                                                    }
                                                }
                                            }
                                            break;
                                        case "Shoot":
                                            if ((int)PlaceAreaCurrent[i].x >= 0 && (int)PlaceAreaCurrent[i].x < GridInfo.AllGrids[0].Length && (int)PlaceAreaCurrent[i].y >= 0 && (int)PlaceAreaCurrent[i].y < GridInfo.AllGrids.Length)
                                            {

                                                for (int t = 0; t < Opponents.Length; t++)
                                                {

                                                    CharacterBase OpponentInformation = Opponents[t].gameObject.GetComponent<CharacterBase>();
                                                    GunFunction SelfGun = gameObject.GetComponent<GunFunction>();
                                                    if (Opponents[t].IsDead == false && (CurrentAreaShot(new Vector2((int)PlaceAreaCurrent[i].x, (int)PlaceAreaCurrent[i].y), (int)OpponentInformation.CharacterLocationIndex.x, (int)OpponentInformation.CharacterLocationIndex.y, 0, 1, SelfGun) != -69 || CurrentAreaShot(new Vector2((int)PlaceAreaCurrent[i].x, (int)PlaceAreaCurrent[i].y), (int)OpponentInformation.CharacterLocationIndex.x, (int)OpponentInformation.CharacterLocationIndex.y, -1, 0, SelfGun) != -69 || CurrentAreaShot(new Vector2((int)PlaceAreaCurrent[i].x, (int)PlaceAreaCurrent[i].y), (int)OpponentInformation.CharacterLocationIndex.x, (int)OpponentInformation.CharacterLocationIndex.y, 0, -1, SelfGun) != -69 && CurrentAreaShot(new Vector2((int)PlaceAreaCurrent[i].x, (int)PlaceAreaCurrent[i].y), (int)OpponentInformation.CharacterLocationIndex.x, (int)OpponentInformation.CharacterLocationIndex.y, 1, 0, SelfGun) != -69))
                                                    {
                                                        if (PriorityDeterationSheet[(int)PlaceAreaCurrent[i].x][(int)PlaceAreaCurrent[i].y] + PriorityDeductions + ShootAdd > SuggestedActionPriority[0])
                                                        {
                                                            InsertIntoList("Shoot", -((CurrentMoveGridCreation - 1) * MovementPriorityDown) + PriorityDeductions + ShootAdd, new Vector2(PlaceAreaCurrent[i].x, PlaceAreaCurrent[i].y), true);
                                                            if (PriorityDeterationSheet[(int)PlaceAreaCurrent[i].x][(int)PlaceAreaCurrent[i].y] + PriorityDeductions + ShootAdd > SuggestedActionPriority[SuggestedActionPriority.Length - 1])
                                                            {
                                                                PriorityAdd = ShootAdd;
                                                            }

                                                            if (gameObject.name == "wow")
                                                            {
                                                                print("NewLocationSet______________________________________________");
                                                                print("Suggested Action: Shoot");
                                                                print("Suggested Priority " + SuggestedActionPriority);
                                                                print("Suggested Priority " + SuggestedActionLocation);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case "SpecialMove":
                                            for (int t = 0; t < SpecialMoves.Length; t++)
                                            {
                                                int[] LocationSend = SpecialMoves[t].CheckIfConditionsApply(new Vector2((int)PlaceAreaCurrent[i].x, (int)PlaceAreaCurrent[i].y));
                                                if (LocationSend[0] != -69 && LocationSend[1] != -69 && LocationSend[2] != -69 && PriorityDeterationSheet[(int)PlaceAreaCurrent[i].x][(int)PlaceAreaCurrent[i].y] + PriorityDeductions + LocationSend[2] > SuggestedActionPriority[0])
                                                {
                                                    if (PriorityDeterationSheet[(int)CharacterInfo.CharacterLocationIndex.x][(int)CharacterInfo.CharacterLocationIndex.y] + PriorityDeductions + LocationSend[2] > SuggestedActionPriority[SuggestedActionPriority.Length - 1])
                                                    {
                                                        PriorityAdd = LocationSend[2];
                                                    }
                                                    InsertIntoList("SPMove1", -((CurrentMoveGridCreation - 1) * MovementPriorityDown) + PriorityDeductions + LocationSend[2], new Vector2(PlaceAreaCurrent[i].x, PlaceAreaCurrent[i].y), true);
                                                }
                                            }
                                            break;
                                    }
                                }
                            }

                            //FIX THIS LATER// CURRENTLY NOT WORKING
                            if ((int)PlaceAreaCurrent[i].y >= 0 && (int)PlaceAreaCurrent[i].y < GridInfo.AllGrids.Length && (int)PlaceAreaCurrent[i].x >= 0 && (int)PlaceAreaCurrent[i].x < GridInfo.AllGrids[0].Length && PriorityDeterationSheet[(int)PlaceAreaCurrent[i].x][(int)PlaceAreaCurrent[i].y] + PriorityDeductions + PriorityAdd >= OldActionPriority - ThresholdPriorty)
                            {
                                for (int SpI = 0; SpI < SpecialMoves.Length; SpI++)
                                {
                                    if (SpecialMoves[SpI] != null && SpecialMoves[SpI].willUseForMove == true)
                                    {
                                        GridControl[] FutureAreas;
                                        int[][] OldAreaCanClick;
                                        OldAreaCanClick = SpecialMoves[SpI].NewAreaEffectMove(SpecialMoves[SpI].AreaCanClick, (int)PlaceAreaCurrent[i].x, (int)PlaceAreaCurrent[i].y);
                                        FutureAreas = InMultiAreaGridControl(SpecialMoves[SpI].NewAreaEffectAdjust(OldAreaCanClick, SpecialMoves[SpI].MoveSpacesAllowedAdjust));
                                        for (int z = 0; z < FutureAreas.Length; z++)
                                        {
                                            if (FutureAreas[z] != null && FutureAreas[z].ObstacleIndex == 0 && FutureAreas[z].CharacterOn == false && PriorityDeterationSheet[(int)FutureAreas[z].GridCoordinate.x][(int)FutureAreas[z].GridCoordinate.y] < PriorityDeterationSheet[(int)PlaceAreaCurrent[i].x][(int)PlaceAreaCurrent[i].y] + -MovementPriorityDown + SpecialMoves[SpI].PriorityAdd)
                                            {
                                                GridSpreadSheet[(int)FutureAreas[z].GridCoordinate.x][(int)FutureAreas[z].GridCoordinate.y] = CurrentMoveGridCreation + 1;
                                                if (-MovementPriorityDown + SpecialMoves[SpI].PriorityAdd > 0)
                                                {
                                                    print("wow");
                                                }
                                                PriorityDeterationSheet[(int)FutureAreas[z].GridCoordinate.x][(int)FutureAreas[z].GridCoordinate.y] = PriorityDeterationSheet[(int)PlaceAreaCurrent[i].x][(int)PlaceAreaCurrent[i].y] - MovementPriorityDown + SpecialMoves[SpI].PriorityAdd;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if ((int)PlaceAreaCurrent[i].x >= 0 && (int)PlaceAreaCurrent[i].x < GridInfo.AllGrids.Length && (int)PlaceAreaCurrent[i].y >= 0 && (int)PlaceAreaCurrent[i].y < GridInfo.AllGrids[0].Length)
                                {
                                    GridSpreadSheet[(int)PlaceAreaCurrent[i].x][(int)PlaceAreaCurrent[i].y] = 0;
                                    PriorityDeterationSheet[(int)PlaceAreaCurrent[i].x][(int)PlaceAreaCurrent[i].y] = -1000;
                                }
                            }
                        }
                    }
                    CurrentMoveGridCreation++;
                }
                else
                {
                    InitiateMoveGridCreation = false;
                    bool HasUsedRandomGrid = false;
                    bool hasUsedOffMainMove = false;
                    RandomDraw = UnityEngine.Random.Range(0, 101);
                    for (int z = RangeOfRandomness.Length - 1; z >= 0; z--)
                    {
                        if (RandomDraw < RangeOfRandomness[z] && HasUsedRandomGrid == false && SuggestedAction[z] != "inactive")
                        {
                            HasUsedRandomGrid = true;
                            SuggestedActionFinal = SuggestedAction[z];
                            SuggestedActionLocationFinal = SuggestedActionLocation[z];
                            SuggestedActionPriorityFinal = SuggestedActionPriority[z];
                            WillMoveToFinal = WillMoveTo[z];
                            hasUsedOffMainMove = true;
                        }
                    }
                    if (hasUsedOffMainMove == false && HasUsedRandomGrid == false)
                    {
                        HasUsedRandomGrid = true;
                        SuggestedActionFinal = SuggestedAction[SuggestedAction.Length - 1];
                        SuggestedActionLocationFinal = SuggestedActionLocation[SuggestedActionLocation.Length - 1];
                        SuggestedActionPriorityFinal = SuggestedActionPriority[SuggestedActionPriority.Length - 1];
                        WillMoveToFinal = WillMoveTo[WillMoveTo.Length - 1];

                    }
                    //Place Randomizer here
                    if (WillMoveToFinal == true)
                    {
                        CurrentMoveGridDecronstruction = GridSpreadSheet[(int)SuggestedActionLocationFinal.x][(int)SuggestedActionLocationFinal.y];
                        DeconstrucionMoveLocation = SuggestedActionLocationFinal;
                    }
                }
            }
            else
            {

                if (WillMoveToFinal == true)
                {
                    if (gameObject.name == "wow")
                    {
                        print("NowShowingResults______________________________________");
                        print("Suggested Priorty: " + SuggestedActionPriority);
                        print("Location suggested: (" + DeconstrucionMoveLocation.x + " ," + DeconstrucionMoveLocation.y + ")");
                        print("Action suggested: " + SuggestedAction);
                    }
                    bool HasReachedArea = false;
                    if (CurrentMoveGridDecronstruction < -100)
                    {
                        SuggestedActionFinal = "inactive";
                        WillMoveToFinal = false;
                        HasReachedArea = true;
                    }
                    if (gameObject.name == "wow")
                    {
                        print("");
                    }
                    if (HasReachedArea == false)
                    {
                        for (int SpI = 0; SpI < SpecialMoves.Length; SpI++)
                        {
                            if (SpecialMoves[SpI] != null && SpecialMoves[SpI].willUseForMove == true)
                            {
                                GridControl[] FutureAreas;
                                int[][] OldAreaCanClick;
                                OldAreaCanClick = SpecialMoves[SpI].NewAreaEffectMove(SpecialMoves[SpI].AreaCanClick, (int)DeconstrucionMoveLocation.x, (int)DeconstrucionMoveLocation.y);
                                FutureAreas = InMultiAreaGridControl(SpecialMoves[SpI].NewAreaEffectAdjust(OldAreaCanClick, SpecialMoves[SpI].MoveSpaceBackTrackAdjust));
                                for (int z = 0; z < FutureAreas.Length; z++)
                                {
                                    if (FutureAreas[z] != null && GridSpreadSheet[(int)FutureAreas[z].GridCoordinate.x][(int)FutureAreas[z].GridCoordinate.y] == CurrentMoveGridDecronstruction - 1)
                                    {
                                        if (CurrentMoveGridDecronstruction - 1 == 1)
                                        {
                                            SuggestedActionFinal = "SPMove" + SpI;
                                            SuggestedActionLocationFinal = DeconstrucionMoveLocation;
                                            WillMoveToFinal = false;
                                            HasReachedArea = true;
                                        }
                                        DeconstrucionMoveLocation = new Vector2(FutureAreas[z].GridCoordinate.x, FutureAreas[z].GridCoordinate.y);
                                    }
                                }
                            }
                        }
                    }
                    CurrentMoveGridDecronstruction -= 1;
                }
                else
                {
                    CharacterInfo.PushAction(SuggestedActionLocationFinal, SuggestedActionFinal, MoveSystemInfo.TextBoxLoader);
                    IsMakingDecison = false;
                }
            }
        }
    }
}
