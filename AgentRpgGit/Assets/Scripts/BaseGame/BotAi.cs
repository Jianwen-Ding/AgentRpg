using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAi : MonoBehaviour
{
    //CURRENT
    //MAKE IT PARTIALLY RANDOMIZED HEAVILY FAVOURING TOP PRIORITY
    //Randomizer System 1-100 random number to pick from priority from list
    //These Are the thresholds that is used to process the numbers the length of the array is the amount of subsets.
    //Out of 100
    [SerializeField]
    public int[] RangeOfRandomness;
    [SerializeField]
    int CurrentPriorityQue;
    [SerializeField]
    public bool IsMakingDecison;
    [SerializeField]
    bool HasStartedMakingDecison;
    #region MovementGridThings
    //Grid Spread Sheet Shows the arearas from move steps
    public bool InitiateMoveGridCreation;
    public int CurrentMoveGridCreation;
    //Finds the base movement
    public int CurrentMoveGridDecronstruction;
    public Vector2 DeconstrucionMoveLocation;
    // Finds the order of movments
    [SerializeField]
    int OldActionPriority;
    [SerializeField]
    int[][] GridSpreadSheet;
    [SerializeField]
    int[][] PriorityDeterationSheet;
    [SerializeField]
    bool InitiateMoveMentDecison;
    [SerializeField]
    int[][] AbilityToMoveAdjust;
    [SerializeField]
    int IdealDistanceCloseToOpponents;
    [SerializeField]
    int IdealDistanceAwayFromOpponents;
    [SerializeField]
    int[][] AllowedMoveArea;
    #endregion
    #region DecisonMaking
    //Sorting Decisons
    //OPTIONS:
    //Move = "Move"
    //Shoot = "Shoot"
    //SpecialMove = "SPMove" + [Index of special move]
    //Inactive = "inactive"
    // The arrays follow the same amount for range of randomness to coorespond to thresholds
    public string[] SuggestedAction;
    //Priorities
    [SerializeField]
    public int[] SuggestedActionPriority;
    //LocationThatActionWillTake
    [SerializeField]
    public Vector2[] SuggestedActionLocation;
    //Will Move or not
    public bool[] WillMoveTo;
    //FinalDeconstruct
    public string SuggestedActionFinal;
    public int SuggestedActionPriorityFinal;
    public Vector2 SuggestedActionLocationFinal;
    public bool WillMoveToFinal;
    [SerializeField]
    public int MovementPriorityDown;
    //Priority Deduction
    //Deducts Priorty of an action based on a situation
    [SerializeField]
    public int TooCloseToPriorityDedeuct;
    [SerializeField]
    public int TooFarToPriorityDedeuct;
    [SerializeField]
    public int DeductPerBulletDmg;
    [SerializeField]
    public int ChargeMoveDedeuct;
    //This is the Priority add to diffrent actions
    [SerializeField]
    int MovePriorityAdd;
    [SerializeField]
    public int ShootAdd;
    //Priorities Checks if certain things are checked
    //For loop checks priority based on level in the Array
    //"Shoot"
    //"AdjustLocation"
    //"SpecialMove"
    [SerializeField]
    public string[] Priorities;
    #endregion
    //Set Get Components
    [SerializeField]
    CharacterBase CharacterInfo;
    [SerializeField]
    GridLoad GridInfo;
    [SerializeField]
    GridControl[][] GridControlInfo;
    [SerializeField]
    GenericMove[] SpecialMoves= new GenericMove[4];
    [SerializeField]
    MoveSystem MoveSystemInfo;
    public CharacterBase[] Opponents = new CharacterBase[3];    
    public CharacterBase[] Allys = new CharacterBase[3];
    //To Find allys and Opponents
    [SerializeField]
    int AmountOfAllysAdded;
    [SerializeField]
    int AmountOfOpponentsAdded;
    // Debug
    [SerializeField]
    string RangeIn;
    [SerializeField]
    bool InHitDistance;
    //Threshold To allow
    // When moving between points there might be places where they have to go through less advantagous positions to get to an advantageous position. The threshold priority tells the bot how much less can it be until it refuses to take a path. 
    [SerializeField]
    int ThresholdPriorty;
    //Strings
    //InIdealRange
    //TooFar
    //TooClose
    string GoingToShowString;
    //Outputs ==
    //Inserts into list
    //Debug
    public int RandomDraw;
    public void InsertIntoList(string InsertActionType, int InsertPriority, Vector2 insertLocation, bool WillMove)
    {
        //Inserts then sorts everything
        if (SuggestedActionPriority[0] < InsertPriority)
        {
            SuggestedAction[0] = InsertActionType;
            SuggestedActionPriority[0] = InsertPriority;
            SuggestedActionLocation[0] = insertLocation;
            WillMoveTo[0] = WillMove;
        }
        for (int i = 0; i < SuggestedActionPriority.Length; i++)
        {
            for (int z = 0; z < SuggestedActionPriority.Length; z++)
            {
                if (z + 1 < SuggestedActionPriority.Length && SuggestedActionPriority[z] > SuggestedActionPriority[z + 1])
                {
                    int tempPriority = SuggestedActionPriority[z];
                    string tempString = SuggestedAction[z];
                    Vector2 tempLocation = SuggestedActionLocation[z];
                    bool tempBool = WillMoveTo[z];
                    SuggestedActionPriority[z] = SuggestedActionPriority[z + 1];
                    SuggestedAction[z] = SuggestedAction[z + 1];
                    SuggestedActionLocation[z] = SuggestedActionLocation[z + 1];
                    WillMoveTo[z] = WillMoveTo[z + 1];
                    SuggestedActionPriority[z + 1] = tempPriority;
                    SuggestedAction[z + 1] = tempString;
                    SuggestedActionLocation[z + 1] = tempLocation;
                    WillMoveTo[z + 1] = tempBool;
                }
            }
        }
    }
    public string InOpponentRange(int x, int y)
    {
        GoingToShowString = "TooFar";
        for (int i = 0; i < Opponents.Length; i++)
        {
            if(Opponents[i].IsDead == false)
            {
                if (Opponents[i].CharacterLocationIndex.x + IdealDistanceCloseToOpponents >= x && Opponents[i].CharacterLocationIndex.x - IdealDistanceCloseToOpponents <= x && Opponents[i].CharacterLocationIndex.y + IdealDistanceCloseToOpponents >= y && Opponents[i].CharacterLocationIndex.y - IdealDistanceCloseToOpponents <= y)
                {
                    if (Opponents[i].CharacterLocationIndex.x + IdealDistanceAwayFromOpponents >= x && Opponents[i].CharacterLocationIndex.x - IdealDistanceAwayFromOpponents <= x && Opponents[i].CharacterLocationIndex.y + IdealDistanceAwayFromOpponents >= y && Opponents[i].CharacterLocationIndex.y - IdealDistanceAwayFromOpponents <= y)
                    {
                        GoingToShowString = "TooClose";
                    }
                    else if (GoingToShowString != "TooClose")
                    {
                        GoingToShowString = "InIdealRange";
                    }
                }
                else if (GoingToShowString != "TooClose" && GoingToShowString != "InIdealRange")
                {
                    GoingToShowString = "TooFar";
                }
            }
           
        }
        return GoingToShowString;
    }
    public GridControl[] InMultiAreaGridControl(int[][] CheckAreaProvided)
    {
        GridControl[] ReturnVar = new GridControl[100];
        int CurrentAt = 0;
        if(CheckAreaProvided != null)
        {
            for (int i = 0; i < CheckAreaProvided.Length; i++)
            {
                for (int XCheck = 0; XCheck <= CheckAreaProvided[i][2] - CheckAreaProvided[i][0]; XCheck++)
                {
                    for (int YCheck = 0; YCheck <= CheckAreaProvided[i][3] - CheckAreaProvided[i][1]; YCheck++)
                    {
                        if (YCheck + CheckAreaProvided[i][1] < GridInfo.AllGrids.Length && YCheck + CheckAreaProvided[i][1] >= 0 && XCheck + CheckAreaProvided[i][0] < GridInfo.AllGrids[0].Length && XCheck + CheckAreaProvided[i][0] >= 0)
                        {
                            ReturnVar[CurrentAt] = GridInfo.AllGrids[YCheck + CheckAreaProvided[i][1]][XCheck + CheckAreaProvided[i][0]].GetComponent<GridControl>();
                            CurrentAt++;
                        }
                    }
                }

            }
        }
        else
        {
            print("ERROR");
            print("ERROR");
            print("ERROR");
        }
        return ReturnVar;
    }
    public Vector2[] InMultiAreaLocation(int[][] CheckAreaProvided)
    {
        Vector2[] ReturnVar = new Vector2[100];
        int CurrentAt = 0;
        for (int i = 0; i < CheckAreaProvided.Length; i++)
        {
            for (int XCheck = 0; XCheck <= CheckAreaProvided[i][2] - CheckAreaProvided[i][0]; XCheck++)
            {
                for (int YCheck = 0; YCheck <= CheckAreaProvided[i][3] - CheckAreaProvided[i][1]; YCheck++)
                {
                    if (YCheck + CheckAreaProvided[i][1] < GridInfo.AllGrids.Length && YCheck + CheckAreaProvided[i][1] >= 0 && XCheck + CheckAreaProvided[i][0] < GridInfo.AllGrids[i].Length && XCheck + CheckAreaProvided[i][0] >= 0)
                    {
                        ReturnVar[CurrentAt] = new Vector2(XCheck + CheckAreaProvided[i][0], YCheck + CheckAreaProvided[i][1]);
                        CurrentAt++;
                    }
                }
            }

        }
        return ReturnVar;
    }
    public bool InMultiAreaGridCheck(int x, int y,int[][] MultiAreaGridCheck)
    {
        for (int i = 0; i < MultiAreaGridCheck.Length; i++)
        {
            if(x <= MultiAreaGridCheck[i][2] && x >= MultiAreaGridCheck[i][0] && y <= MultiAreaGridCheck[i][3] && y >= MultiAreaGridCheck[i][1])
            {
                return true;
            }
           
        }
        return false;
    }
    //For In Oppponent Shoot Check, returns distance -69 is default
    public int CurrentAreaShot(Vector2 AreaStart, int x, int y, int VelocityX, int VelocityY, GunFunction OpponentGun)
    {
        if (gameObject.name == "wow" && (int)CharacterInfo.CharacterLocationIndex.x == 6 && (int)CharacterInfo.CharacterLocationIndex.y == 4 && VelocityX == -1 && x == 0 && y == 4 && OpponentGun == gameObject.GetComponent<GunFunction>())
        {
            print("wow");
        }
        int DoesItHitDistance = -69;
        OpponentGun.ShootAbility(AreaStart, new Vector2(VelocityX, VelocityY), CharacterInfo.IsEnemy, OpponentGun.DistanceSquaresAllowed, OpponentGun.CanPenentrateObstacle, OpponentGun.CanPenentrateCharacters, OpponentGun.DamageRatio, OpponentGun.DamageFallOff, OpponentGun.bulletAddition, null, null);
        Vector2[][] GunPlaces = OpponentGun.ShootAbility(AreaStart, new Vector2(VelocityX, VelocityY), CharacterInfo.IsEnemy, OpponentGun.DistanceSquaresAllowed, OpponentGun.CanPenentrateObstacle, OpponentGun.CanPenentrateCharacters, OpponentGun.DamageRatio, OpponentGun.DamageFallOff, OpponentGun.bulletAddition, null, null);
        
        if (GunPlaces != null)
        {
            for (int i = 0; i < GunPlaces.Length; i++)
            {
                for (int z = 0; z < GunPlaces[i].Length; z++)
                {
                    if (x == GunPlaces[i][z].x && y == GunPlaces[i][z].y)
                    {
                        DoesItHitDistance = z;
                    }
                }
            }
        }
        return DoesItHitDistance;
    }
    //Gives out damage deduction, -69 is default
    public int[] InOpponentShootCheck(int CurrentX, int CurrentY)
    {

        if (CharacterInfo.name == "wow" && CurrentX == 3 && CurrentY == 0)
        {
            //print("wow");
        }
        // -69 is default
        int[] HasBeenHitDmg = new int[Opponents.Length];
        for(int x = 0; x < HasBeenHitDmg.Length; x++)
        {
            HasBeenHitDmg[x] = -69;
        }
        for(int i = 0; i < Opponents.Length; i++)
        {
            if (Opponents[i].IsDead == false)
            {
                GunFunction OpponentGun = Opponents[i].gameObject.GetComponent<GunFunction>();
                if (CurrentAreaShot(Opponents[i].gameObject.GetComponent<CharacterBase>().CharacterLocationIndex, CurrentX, CurrentY, 0, 1, OpponentGun) != -69)
                {
                    HasBeenHitDmg[i] = (int)(DeductPerBulletDmg * (Math.Pow(OpponentGun.DamageFallOff, (CurrentAreaShot(Opponents[i].gameObject.GetComponent<CharacterBase>().CharacterLocationIndex, CurrentX, CurrentY, 0, 1, OpponentGun))) * Opponents[i].gameObject.GetComponent<CharacterBase>().ExpressedDamage * OpponentGun.DamageRatio));
                }
                else if (CurrentAreaShot(Opponents[i].gameObject.GetComponent<CharacterBase>().CharacterLocationIndex, CurrentX, CurrentY, -1, 0, OpponentGun) != -69)
                {
                    HasBeenHitDmg[i] = (int)(DeductPerBulletDmg * (Math.Pow(OpponentGun.DamageFallOff, (CurrentAreaShot(Opponents[i].gameObject.GetComponent<CharacterBase>().CharacterLocationIndex, CurrentX, CurrentY, -1, 0, OpponentGun))) * Opponents[i].gameObject.GetComponent<CharacterBase>().ExpressedDamage * OpponentGun.DamageRatio));
                }
                else if (CurrentAreaShot(Opponents[i].gameObject.GetComponent<CharacterBase>().CharacterLocationIndex, CurrentX, CurrentY, 0, -1, OpponentGun) != -69)
                {
                    HasBeenHitDmg[i] = (int)(DeductPerBulletDmg * (Math.Pow(OpponentGun.DamageFallOff, (CurrentAreaShot(Opponents[i].gameObject.GetComponent<CharacterBase>().CharacterLocationIndex, CurrentX, CurrentY, 0, -1, OpponentGun))) * Opponents[i].gameObject.GetComponent<CharacterBase>().ExpressedDamage * OpponentGun.DamageRatio));

                }
                else if (CurrentAreaShot(Opponents[i].gameObject.GetComponent<CharacterBase>().CharacterLocationIndex, CurrentX, CurrentY, 1, 0, OpponentGun) != -69)
                {
                    HasBeenHitDmg[i] = (int)(DeductPerBulletDmg * (Math.Pow(OpponentGun.DamageFallOff, (CurrentAreaShot(Opponents[i].gameObject.GetComponent<CharacterBase>().CharacterLocationIndex, CurrentX, CurrentY, 1, 0, OpponentGun))) * Opponents[i].gameObject.GetComponent<CharacterBase>().ExpressedDamage * OpponentGun.DamageRatio));

                }
            }
            
        }
        return HasBeenHitDmg;
    }
    
    void Start()
    {
        SuggestedActionPriority = new int[RangeOfRandomness.Length + 1];
        SuggestedAction = new string[RangeOfRandomness.Length + 1];
        SuggestedActionLocation = new Vector2[RangeOfRandomness.Length + 1];
        WillMoveTo = new bool[RangeOfRandomness.Length + 1];
        HasStartedMakingDecison = false;
        GridInfo = Camera.main.gameObject.GetComponent<GridLoad>();
        MoveSystemInfo = Camera.main.gameObject.GetComponent<MoveSystem>();
        CharacterInfo = gameObject.GetComponent<CharacterBase>();
        SpecialMoves = CharacterInfo.MovesAllowed;
        AmountOfAllysAdded = 0;
        AmountOfOpponentsAdded = 0;
        
        for (int x = 0; x < MoveSystemInfo.CharacterOnField.Length; x++)
        {
            CharacterBase CurrentBase = MoveSystemInfo.CharacterOnField[x].GetComponent<CharacterBase>();
            if (CurrentBase.IsEnemy)
            {
                Allys[AmountOfAllysAdded] = CurrentBase;
                AmountOfAllysAdded += 1;
                
            }
            else if(Opponents.Length > AmountOfOpponentsAdded)
            {
                Opponents[AmountOfOpponentsAdded] = CurrentBase;
                AmountOfOpponentsAdded += 1;
            }
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
                PriorityDeterationSheet[x][y] = -100;
            }
        }
    }

    // Update is called once per frame
    void Update()
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
            if (GridInfo.AllGrids[(int)CharacterInfo.CharacterLocationIndex.y][(int)CharacterInfo.CharacterLocationIndex.x].GetComponent<BaseStatus>() != null )
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
                        for(int t = 0; t < SpecialMoves.Length; t++)
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
                for(int i = 0; i < PlaceAreaCurrent.Length; i++)
                {
                    PlaceAreaCurrent[i] = new Vector2(-69, -69);
                }
                bool PlaceAreaCurrentEmpty;
                PlaceAreaCurrentEmpty = true;
                int PlaceAreaCurrentAmount = 0;
                for(int i = 0; i < GridSpreadSheet.Length; i++)
                {
                    for (int z = 0; z < GridSpreadSheet[i].Length; z++)
                    {
                        if(GridSpreadSheet[i][z] == CurrentMoveGridCreation)
                        {
                            PlaceAreaCurrent[PlaceAreaCurrentAmount] = new Vector2( i, z);
                            PlaceAreaCurrentAmount++;
                            PlaceAreaCurrentEmpty = false;
                        }
                    }
                }
                if(PlaceAreaCurrentEmpty == false)
                {
                    for (int i = 0; i < PlaceAreaCurrent.Length; i++)
                    {
                        if(PlaceAreaCurrent[i].x != -69 && PlaceAreaCurrent[i].y != -69 && GridInfo.AllGrids[(int)PlaceAreaCurrent[i].y][(int)PlaceAreaCurrent[i].x].GetComponent<GridControl>().CharacterOn != gameObject.GetComponent<CharacterBase>())
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
                                if(InOpponentRange((int)PlaceAreaCurrent[i].x, (int)PlaceAreaCurrent[i].y) == "TooClose")
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
                                        if(OpponentBase.MovesAllowed[t].WillBeUsedForCharging && OpponentBase.MovesAllowed[t].AreaSoonToEffect != null && InMultiAreaGridCheck((int)PlaceAreaCurrent[i].x, (int)PlaceAreaCurrent[i].y, OpponentBase.MovesAllowed[t].AreaSoonToEffect))
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
                                                    if (Opponents[t].IsDead == false && (CurrentAreaShot(new Vector2((int)PlaceAreaCurrent[i].x, (int)PlaceAreaCurrent[i].y),(int)OpponentInformation.CharacterLocationIndex.x, (int)OpponentInformation.CharacterLocationIndex.y, 0, 1, SelfGun) != -69|| CurrentAreaShot(new Vector2((int)PlaceAreaCurrent[i].x, (int)PlaceAreaCurrent[i].y), (int)OpponentInformation.CharacterLocationIndex.x, (int)OpponentInformation.CharacterLocationIndex.y, -1, 0, SelfGun) != -69 || CurrentAreaShot(new Vector2((int)PlaceAreaCurrent[i].x, (int)PlaceAreaCurrent[i].y), (int)OpponentInformation.CharacterLocationIndex.x, (int)OpponentInformation.CharacterLocationIndex.y, 0, -1, SelfGun) != -69 && CurrentAreaShot(new Vector2((int)PlaceAreaCurrent[i].x, (int)PlaceAreaCurrent[i].y), (int)OpponentInformation.CharacterLocationIndex.x, (int)OpponentInformation.CharacterLocationIndex.y, 1, 0, SelfGun) != -69))
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
                            if ((int)PlaceAreaCurrent[i].y >= 0 && (int)PlaceAreaCurrent[i].y < GridInfo.AllGrids.Length && (int)PlaceAreaCurrent[i].x >= 0 && (int)PlaceAreaCurrent[i].x < GridInfo.AllGrids[0].Length && PriorityDeterationSheet[(int)PlaceAreaCurrent[i].x][(int)PlaceAreaCurrent[i].y] + PriorityDeductions + PriorityAdd>= OldActionPriority - ThresholdPriorty)
                            {
                                GridControl[] AroundArea;
                                int[][] AroundMovement = new int[1][];
                                AroundMovement[0] = new int[4];
                                AroundMovement[0][0] = (int)PlaceAreaCurrent[i].x - 1;
                                AroundMovement[0][1] = (int)PlaceAreaCurrent[i].y - 1;
                                AroundMovement[0][2] = (int)PlaceAreaCurrent[i].x + 1;
                                AroundMovement[0][3] = (int)PlaceAreaCurrent[i].y + 1;
                                AroundArea = InMultiAreaGridControl(AroundMovement);
                                for (int z = 0; z < AroundArea.Length; z++)
                                {
                                    if (AroundArea[z] != null && AroundArea[z].ObstacleIndex == 0 && AroundArea[z].CharacterOn == false && PriorityDeterationSheet[(int)AroundArea[z].GridCoordinate.x][(int)AroundArea[z].GridCoordinate.y] < PriorityDeterationSheet[(int)PlaceAreaCurrent[i].x][(int)PlaceAreaCurrent[i].y] - MovementPriorityDown)
                                    {
                                        GridSpreadSheet[(int)AroundArea[z].GridCoordinate.x][(int)AroundArea[z].GridCoordinate.y] = CurrentMoveGridCreation + 1;
                                        if (-MovementPriorityDown > 0)
                                        {
                                            print("wow");
                                        }
                                        PriorityDeterationSheet[(int)AroundArea[z].GridCoordinate.x][(int)AroundArea[z].GridCoordinate.y] = PriorityDeterationSheet[(int)PlaceAreaCurrent[i].x][(int)PlaceAreaCurrent[i].y] - MovementPriorityDown;
                                    }
                                }
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
                                                if(- MovementPriorityDown + SpecialMoves[SpI].PriorityAdd > 0)
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
                    for(int z = RangeOfRandomness.Length - 1; z >= 0 ; z--)
                    {
                        if(RandomDraw < RangeOfRandomness[z]&& HasUsedRandomGrid == false && SuggestedAction[z] != "inactive")
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
                
                if(WillMoveToFinal == true)
                {
                    if(gameObject.name == "wow")
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

                        GridControl[] AroundArea;
                    int[][] AroundMovement = new int[1][];
                    AroundMovement[0] = new int[4];
                    AroundMovement[0][0] = (int)DeconstrucionMoveLocation.x - 1;
                    AroundMovement[0][1] = (int)DeconstrucionMoveLocation.y - 1;
                    AroundMovement[0][2] = (int)DeconstrucionMoveLocation.x + 1;
                    AroundMovement[0][3] = (int)DeconstrucionMoveLocation.y + 1;
                    AroundArea = InMultiAreaGridControl(AroundMovement);
                    if(HasReachedArea == false)
                    {
                        for (int z = 0; z < AroundArea.Length; z++)
                        {
                            if (AroundArea[z] != null && GridSpreadSheet[(int)AroundArea[z].GridCoordinate.x][(int)AroundArea[z].GridCoordinate.y] == CurrentMoveGridDecronstruction - 1)
                            {
                                if (GridSpreadSheet[(int)AroundArea[z].GridCoordinate.x][(int)AroundArea[z].GridCoordinate.y] == 1)
                                {
                                    SuggestedActionFinal = "Move";
                                    SuggestedActionLocationFinal = DeconstrucionMoveLocation;
                                    WillMoveToFinal = false;
                                    HasReachedArea = true;
                                }
                                DeconstrucionMoveLocation = new Vector2(AroundArea[z].GridCoordinate.x, AroundArea[z].GridCoordinate.y);
                            }
                        }
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
