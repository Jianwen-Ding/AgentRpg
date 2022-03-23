using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMove : MonoBehaviour
{
    #region Varibles
    [SerializeField]
    public bool HasUsedCharge = false;
    // Start is called before the first frame update
    //Area That Can Be selected
    // double array uses [number] [x1,x2,width,height] 
    //Width and height are not the actual width and height, they are the lower right ends of the square. 
    // Sorry future me its a naming issue
    public int[][] AreaCanClick = new int[5][];
    //AreaCanClickSelectionParameters
    // Other
    public MouseFollow MouseFollowingUI;
    public GameObject MoveSprite;
    public GameObject MoveSpriteSecondary;
    public Vector2 AdjustSprite;
    public GridLoad Gridinfo;
    public CharacterBase Character_Info;
    public GameObject HitUiSprite;
    GameObject[] CharacterGatherer = new GameObject[6];
    int CharactersGathered;
    public GameObject[] Effects = new GameObject[20];
    public int EffectAmount;
    //Used for Charging
    public int[][] AreaSoonToEffect;
    public bool WillBeUsedForCharging;
    public MoveSystem MoveDecison;
    //Mostly Used for Ai
    public BotAi BotAiCheckIfApply;
    // no longer in use public bool HasUsedCharge = false;
    // double array uses [number] [x1,x2,width,height] 
    public int PriorityAdd = 0;
    public bool IsForOpponent;
    //PURELY FOR DEBUG__________________________________
    public int[] AreaSelection = new int[4];
    //Area To Select
    public int[][] AreaCanSelect = new int[5][];
    #region AreaSelectGridParameters
    public int AreaSelectionSquareX0 = 0;
    public int AreaSelectionSquareY0 = 0;
    public int AreaSelectionSquareWidth0 = 0;
    public int AreaSelectionSquareHeight0 = 0;
    public int AreaSelectionSquareX1 = -69;
    public int AreaSelectionSquareY1 = -69;
    public int AreaSelectionSquareWidth1 = -69;
    public int AreaSelectionSquareHeight1 = -69;
    public int AreaSelectionSquareX2 = -69;
    public int AreaSelectionSquareY2 = -69;
    public int AreaSelectionSquareWidth2 = -69;
    public int AreaSelectionSquareHeight2 = -69;
    public int AreaSelectionSquareX3 = -69;
    public int AreaSelectionSquareY3 = -69;
    public int AreaSelectionSquareWidth3 = -69;
    public int AreaSelectionSquareHeight3 = -69;
    public int AreaSelectionSquareX4 = -69;
    public int AreaSelectionSquareY4 = -69;
    public int AreaSelectionSquareWidth4 = -69;
    public int AreaSelectionSquareHeight4 = -69;
    #endregion
    //If uses Bullet Function
    public bool UsesBulletFunction;
    //Which Places an enemy can be Moved
    public bool willUseForMove;
    public int[][] MoveSpacesAllowedAdjust = new int[5][];
    public int[][] MoveSpacesAllowed;
    public int[][] MoveSpaceBackTrackAllowed;
    public int[][] MoveSpaceBackTrackAdjust;
    #region AreaMoveGridParameters
    public int AreaSelectMoveSquareX0 = -69;
    public int AreaSelectMoveSquareY0 = -69;
    public int AreaSelectMoveSquareWidth0 = -69;
    public int AreaSelectMoveSquareHeight0 = -69;
    public int AreaSelectMoveSquareX1 = -69;
    public int AreaSelectMoveSquareY1 = -69;
    public int AreaSelectMoveSquareWidth1 = -69;
    public int AreaSelectMoveSquareHeight1 = -69;
    public int AreaSelectMoveSquareX2 = -69;
    public int AreaSelectMoveSquareY2 = -69;
    public int AreaSelectMoveSquareWidth2 = -69;
    public int AreaSelectMoveSquareHeight2 = -69;
    public int AreaSelectMoveSquareX3 = -69;
    public int AreaSelectMoveSquareY3 = -69;
    public int AreaSelectMoveSquareWidth3 = -69;
    public int AreaSelectMoveSquareHeight3 = -69;
    public int AreaSelectMoveSquareX4 = -69;
    public int AreaSelectMoveSquareY4 = -69;
    public int AreaSelectMoveSquareWidth4 = -69;
    public int AreaSelectMoveSquareHeight4 = -69;
    #endregion
    //Which Grids Can Be Effected
    public bool willUseForGridEffect;
    public int[][] PlayerSpacesAllowedAdjust = new int[5][];
    public int[][] PlayerSpacesAllowed;
    #region AreaEffectGridParameters
    public int WillUseForSquareX0 = -69;
    public int WillUseForSquareY0 = -69;
    public int WillUseForSquareWidth0 = -69;
    public int WillUseForSquareHeight0 = -69;
    public int WillUseForSquareX1 = -69;
    public int WillUseForSquareY1 = -69;
    public int WillUseForSquareWidth1 = -69;
    public int WillUseForSquareHeight1 = -69;
    public int WillUseForSquareX2 = -69;
    public int WillUseForSquareY2 = -69;
    public int WillUseForSquareWidth2 = -69;
    public int WillUseForSquareHeight2 = -69;
    public int WillUseForSquareX3 = -69;
    public int WillUseForSquareY3 = -69;
    public int WillUseForSquareWidth3 = -69;
    public int WillUseForSquareHeight3 = -69;
    public int WillUseForSquareX4 = -69;
    public int WillUseForSquareY4 = -69;
    public int WillUseForSquareWidth4 = -69;
    public int WillUseForSquareHeight4 = -69;
    #endregion
    //MOST LIKELY WONT BE USED
    //Which Places it can move a Enemy
    public bool willUseForEnemyMove;
    public int[][] EnemyMoveAllowedAdjust = new int[5][];
    public int[][] EnemyMoveSpacesAllowed;
    #region AreaAdjustGridParameters
    public int EnemyMoveSquareX0 = 0;
    public int EnemyMoveSquareY0 = 0;
    public int EnemyMoveSquareWidth0 = 0;
    public int EnemyMoveSquareHeight0 = 0;
    public int EnemyMoveSquareX1 = -69;
    public int EnemyMoveSquareY1 = -69;
    public int EnemyMoveSquareWidth1 = -69;
    public int EnemyMoveSquareHeight1 = -69;
    public int EnemyMoveSquareX2 = -69;
    public int EnemyMoveSquareY2 = -69;
    public int EnemyMoveSquareWidth2 = -69;
    public int EnemyMoveSquareHeight2 = -69;
    public int EnemyMoveSquareX3 = -69;
    public int EnemyMoveSquareY3 = -69;
    public int EnemyMoveSquareWidth3 = -69;
    public int EnemyMoveSquareHeight3 = -69;
    public int EnemyMoveSquareX4 = -69;
    public int EnemyMoveSquareY4 = -69;
    public int EnemyMoveSquareWidth4 = -69;
    public int EnemyMoveSquareHeight4 = -69;
    #endregion
    //Which Places It can move a Ally
    public bool willUseForAllyMove;
    public int[][] AllyMoveAllowedAdjust = new int[5][];
    public int[][] AllyMoveSpacesAllowed;
    #region AreaAdjustGridParameters
    public int AllyMoveSquareX0 = 0;
    public int AllyMoveSquareY0 = 0;
    public int AllyMoveSquareWidth0 = 0;
    public int AllyMoveSquareHeight0 = 0;
    public int AllyMoveSquareX1 = -69;
    public int AllyMoveSquareY1 = -69;
    public int AllyMoveSquareWidth1 = -69;
    public int AllyMoveSquareHeight1 = -69;
    public int AllyMoveSquareX2 = -69;
    public int AllyMoveSquareY2 = -69;
    public int AllyMoveSquareWidth2 = -69;
    public int AllyMoveSquareHeight2 = -69;
    public int AllyMoveSquareX3 = -69;
    public int AllyMoveSquareY3 = -69;
    public int AllyMoveSquareWidth3 = -69;
    public int AllyMoveSquareHeight3 = -69;
    public int AllyMoveSquareX4 = -69;
    public int AllyMoveSquareY4 = -69;
    public int AllyMoveSquareWidth4 = -69;
    public int AllyMoveSquareHeight4 = -69;
    #endregion
    #endregion
    //Used in order to adjust values
    public virtual  void SetAdjust()
    {
    }
    public virtual void Start()
    {
        BotAiCheckIfApply = gameObject.GetComponent<BotAi>();
        MoveDecison = Camera.main.gameObject.GetComponent<MoveSystem>();
        MouseFollowingUI = GameObject.Find("MouseObject").GetComponent<MouseFollow>();
        SetAdjust();
        for (int i = 0; i < EnemyMoveAllowedAdjust.Length; i++)
        {
            EnemyMoveAllowedAdjust[i] = new int[4];
        }
        for (int i = 0; i < AllyMoveAllowedAdjust.Length; i++)
        {
            AllyMoveAllowedAdjust[i] = new int[4];
        }
        for (int i = 0; i < MoveSpacesAllowedAdjust.Length; i++)
        {
            MoveSpacesAllowedAdjust[i] = new int[4];
        }
        for (int i = 0; i < PlayerSpacesAllowedAdjust.Length; i++)
        {
            PlayerSpacesAllowedAdjust[i] = new int[4];
        }
        for (int i = 0; i < AreaCanClick.Length; i++)
        {
            AreaCanClick[i] = new int[4];
        }
        // Set Area Click
        AreaCanClick[0][0] = AreaSelectionSquareX0;
        AreaCanClick[0][1] = AreaSelectionSquareY0;
        AreaCanClick[0][2] = AreaSelectionSquareWidth0;
        AreaCanClick[0][3] = AreaSelectionSquareHeight0;
        AreaCanClick[1][0] = AreaSelectionSquareX1;
        AreaCanClick[1][1] = AreaSelectionSquareY1;
        AreaCanClick[1][2] = AreaSelectionSquareWidth1;
        AreaCanClick[1][3] = AreaSelectionSquareHeight1;
        AreaCanClick[2][0] = AreaSelectionSquareX2;
        AreaCanClick[2][1] = AreaSelectionSquareY2;
        AreaCanClick[2][2] = AreaSelectionSquareWidth2;
        AreaCanClick[2][3] = AreaSelectionSquareHeight2;
        AreaCanClick[3][0] = AreaSelectionSquareX3;
        AreaCanClick[3][1] = AreaSelectionSquareY3;
        AreaCanClick[3][2] = AreaSelectionSquareWidth3;
        AreaCanClick[3][3] = AreaSelectionSquareHeight3;
        AreaCanClick[4][0] = AreaSelectionSquareX4;
        AreaCanClick[4][1] = AreaSelectionSquareY4;
        AreaCanClick[4][2] = AreaSelectionSquareWidth4;
        AreaCanClick[4][3] = AreaSelectionSquareHeight4;
        // Set Area Can Move
        MoveSpacesAllowedAdjust[0][0] = AreaSelectMoveSquareX0;
        MoveSpacesAllowedAdjust[0][1] = AreaSelectMoveSquareY0;
        MoveSpacesAllowedAdjust[0][2] = AreaSelectMoveSquareWidth0;
        MoveSpacesAllowedAdjust[0][3] = AreaSelectMoveSquareHeight0;
        MoveSpacesAllowedAdjust[1][0] = AreaSelectMoveSquareX1;
        MoveSpacesAllowedAdjust[1][1] = AreaSelectMoveSquareY1;
        MoveSpacesAllowedAdjust[1][2] = AreaSelectMoveSquareWidth1;
        MoveSpacesAllowedAdjust[1][3] = AreaSelectMoveSquareHeight1;
        MoveSpacesAllowedAdjust[2][0] = AreaSelectMoveSquareX2;
        MoveSpacesAllowedAdjust[2][1] = AreaSelectMoveSquareY2;
        MoveSpacesAllowedAdjust[2][2] = AreaSelectMoveSquareWidth2;
        MoveSpacesAllowedAdjust[2][3] = AreaSelectMoveSquareHeight2;
        MoveSpacesAllowedAdjust[3][0] = AreaSelectMoveSquareX3;
        MoveSpacesAllowedAdjust[3][1] = AreaSelectMoveSquareY3;
        MoveSpacesAllowedAdjust[3][2] = AreaSelectMoveSquareWidth3;
        MoveSpacesAllowedAdjust[3][3] = AreaSelectMoveSquareHeight3;
        MoveSpacesAllowedAdjust[4][0] = AreaSelectMoveSquareX4;
        MoveSpacesAllowedAdjust[4][1] = AreaSelectMoveSquareY4;
        MoveSpacesAllowedAdjust[4][2] = AreaSelectMoveSquareWidth4;
        MoveSpacesAllowedAdjust[4][3] = AreaSelectMoveSquareHeight4;
        // Set Area Can Effect
        PlayerSpacesAllowedAdjust[0][0] = WillUseForSquareX0;
        PlayerSpacesAllowedAdjust[0][1] = WillUseForSquareY0;
        PlayerSpacesAllowedAdjust[0][2] = WillUseForSquareWidth0;
        PlayerSpacesAllowedAdjust[0][3] = WillUseForSquareHeight0;
        PlayerSpacesAllowedAdjust[1][0] = WillUseForSquareX1;
        PlayerSpacesAllowedAdjust[1][1] = WillUseForSquareY1;
        PlayerSpacesAllowedAdjust[1][2] = WillUseForSquareWidth1;
        PlayerSpacesAllowedAdjust[1][3] = WillUseForSquareHeight1;
        PlayerSpacesAllowedAdjust[2][0] = WillUseForSquareX2;
        PlayerSpacesAllowedAdjust[2][1] = WillUseForSquareY2;
        PlayerSpacesAllowedAdjust[2][2] = WillUseForSquareWidth2;
        PlayerSpacesAllowedAdjust[2][3] = WillUseForSquareHeight2;
        PlayerSpacesAllowedAdjust[3][0] = WillUseForSquareX3;
        PlayerSpacesAllowedAdjust[3][1] = WillUseForSquareY3;
        PlayerSpacesAllowedAdjust[3][2] = WillUseForSquareWidth3;
        PlayerSpacesAllowedAdjust[3][3] = WillUseForSquareHeight3;
        PlayerSpacesAllowedAdjust[4][0] = WillUseForSquareX4;
        PlayerSpacesAllowedAdjust[4][1] = WillUseForSquareY4;
        PlayerSpacesAllowedAdjust[4][2] = WillUseForSquareWidth4;
        PlayerSpacesAllowedAdjust[4][3] = WillUseForSquareHeight4;
        // Set Area Can Adjust Enemy
        EnemyMoveAllowedAdjust[0][0] = EnemyMoveSquareX0;
        EnemyMoveAllowedAdjust[0][1] = EnemyMoveSquareY0;
        EnemyMoveAllowedAdjust[0][2] = EnemyMoveSquareWidth0;
        EnemyMoveAllowedAdjust[0][3] = EnemyMoveSquareHeight0;
        EnemyMoveAllowedAdjust[1][0] = EnemyMoveSquareX1;
        EnemyMoveAllowedAdjust[1][1] = EnemyMoveSquareY1;
        EnemyMoveAllowedAdjust[1][2] = EnemyMoveSquareWidth1;
        EnemyMoveAllowedAdjust[1][3] = EnemyMoveSquareHeight1;
        EnemyMoveAllowedAdjust[2][0] = EnemyMoveSquareX2;
        EnemyMoveAllowedAdjust[2][1] = EnemyMoveSquareY2;
        EnemyMoveAllowedAdjust[2][2] = EnemyMoveSquareWidth2;
        EnemyMoveAllowedAdjust[2][3] = EnemyMoveSquareHeight2;
        EnemyMoveAllowedAdjust[3][0] = EnemyMoveSquareX3;
        EnemyMoveAllowedAdjust[3][1] = EnemyMoveSquareY3;
        EnemyMoveAllowedAdjust[3][2] = EnemyMoveSquareWidth3;
        EnemyMoveAllowedAdjust[3][3] = EnemyMoveSquareHeight3;
        EnemyMoveAllowedAdjust[4][0] = EnemyMoveSquareX4;
        EnemyMoveAllowedAdjust[4][1] = EnemyMoveSquareY4;
        EnemyMoveAllowedAdjust[4][2] = EnemyMoveSquareWidth4;
        EnemyMoveAllowedAdjust[4][3] = EnemyMoveSquareHeight4;
        // Set Area Can Adjust Ally
        AllyMoveAllowedAdjust[0][0] = AllyMoveSquareX0;
        AllyMoveAllowedAdjust[0][1] = AllyMoveSquareY0;
        AllyMoveAllowedAdjust[0][2] = AllyMoveSquareWidth0;
        AllyMoveAllowedAdjust[0][3] = AllyMoveSquareHeight0;
        AllyMoveAllowedAdjust[1][0] = AllyMoveSquareX1;
        AllyMoveAllowedAdjust[1][1] = AllyMoveSquareY1;
        AllyMoveAllowedAdjust[1][2] = AllyMoveSquareWidth1;
        AllyMoveAllowedAdjust[1][3] = AllyMoveSquareHeight1;
        AllyMoveAllowedAdjust[2][0] = AllyMoveSquareX2;
        AllyMoveAllowedAdjust[2][1] = AllyMoveSquareY2;
        AllyMoveAllowedAdjust[2][2] = AllyMoveSquareWidth2;
        AllyMoveAllowedAdjust[2][3] = AllyMoveSquareHeight2;
        AllyMoveAllowedAdjust[3][0] = AllyMoveSquareX3;
        AllyMoveAllowedAdjust[3][1] = AllyMoveSquareY3;
        AllyMoveAllowedAdjust[3][2] = AllyMoveSquareWidth3;
        AllyMoveAllowedAdjust[3][3] = AllyMoveSquareHeight3;
        AllyMoveAllowedAdjust[4][0] = AllyMoveSquareX4;
        AllyMoveAllowedAdjust[4][1] = AllyMoveSquareY4;
        AllyMoveAllowedAdjust[4][2] = AllyMoveSquareWidth4;
        AllyMoveAllowedAdjust[4][3] = AllyMoveSquareHeight4;
        EffectAmount = 0;
        Gridinfo = Camera.main.GetComponent<GridLoad>();
        Character_Info = gameObject.GetComponent<CharacterBase>();
    }
    #region AiConditionApply;
    //Finds the location to start action
    //its in a int[]
    //[x,y,priorityAdd]
    // -69.-69 is default
    // Will use for grid effect is most useful/ default
    public virtual int[] CheckIfConditionsApply(Vector2 areaCheckFrom)
    {
        int[] DoesConditionsApply = new int[3];
        DoesConditionsApply[0] = -69;
        DoesConditionsApply[1] = -69;
        DoesConditionsApply[2] = -69;
        if (willUseForGridEffect)
        {
            for (int i = 0; i < BotAiCheckIfApply.Opponents.Length; i++)
            {
                if (BotAiCheckIfApply.Opponents[i].IsDead == false)
                {
                    int[][] newAreaCanSelect = new int[5][];
                    for (int z = 0; z < newAreaCanSelect.Length; z++)
                    {
                        newAreaCanSelect[z] = new int[4];
                        //-69 is the signal to null out a SelectionSquare
                        if (newAreaCanSelect[z][0] != -69 && newAreaCanSelect[z][1] != -69 && newAreaCanSelect[z][2] != -69 && newAreaCanSelect[z][3] != -69)
                        {
                            newAreaCanSelect[z][1] = (int)areaCheckFrom.y + AreaCanClick[z][1];
                            newAreaCanSelect[z][2] = (int)areaCheckFrom.x + AreaCanClick[z][2];
                            newAreaCanSelect[z][3] = (int)areaCheckFrom.y + AreaCanClick[z][3];
                            newAreaCanSelect[z][0] = (int)areaCheckFrom.x + AreaCanClick[z][0];
                        }
                    }
                    Vector2 LocationSpot = CheckIfLocationCorrespondsToAction((int)BotAiCheckIfApply.Opponents[i].CharacterLocationIndex.x, (int)BotAiCheckIfApply.Opponents[i].CharacterLocationIndex.y, PlayerSpacesAllowedAdjust, newAreaCanSelect);
                    if (BotAiCheckIfApply.Opponents[i] != null && Character_Info != null && LocationSpot.x != -69 && LocationSpot.y != -69)
                    {
                        DoesConditionsApply[2] = PriorityAdd;
                        Vector2 Location = LocationSpot;
                        DoesConditionsApply[0] = (int)Location.x;
                        DoesConditionsApply[1] = (int)Location.y;
                    }
                }
            }
                
        }
        return DoesConditionsApply;
       
    }
    #endregion
    public virtual void EstablishChargeIfExists(int XSelected, int YSelected)
    {
        if(WillBeUsedForCharging == true)
        {
            int[][] NewSelectedArea;
            NewSelectedArea = NewAreaEffectMove(PlayerSpacesAllowedAdjust, XSelected, YSelected);
            AreaSoonToEffect = NewSelectedArea;
            AreaHighLightToggle(NewSelectedArea, Character_Info.IsEnemy, true);
            Character_Info.IsCharging = true;
        }
    }
    public GridControl[] InMultiAreaGridControl(int[][] CheckAreaProvided)
    {
        GridControl[] ReturnVar = new GridControl[100];
        int CurrentAt = 0;
        if (CheckAreaProvided != null)
        {
            for (int i = 0; i < CheckAreaProvided.Length; i++)
            {
                for (int XCheck = 0; XCheck <= CheckAreaProvided[i][2] - CheckAreaProvided[i][0]; XCheck++)
                {
                    for (int YCheck = 0; YCheck <= CheckAreaProvided[i][3] - CheckAreaProvided[i][1]; YCheck++)
                    {
                        if (YCheck + CheckAreaProvided[i][1] < Gridinfo.AllGrids.Length && YCheck + CheckAreaProvided[i][1] >= 0 && XCheck + CheckAreaProvided[i][0] < Gridinfo.AllGrids[0].Length && XCheck + CheckAreaProvided[i][0] >= 0)
                        {
                            ReturnVar[CurrentAt] = Gridinfo.AllGrids[YCheck + CheckAreaProvided[i][1]][XCheck + CheckAreaProvided[i][0]].GetComponent<GridControl>();
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
                    if (YCheck + CheckAreaProvided[i][1] < Gridinfo.AllGrids.Length && YCheck + CheckAreaProvided[i][1] >= 0 && XCheck + CheckAreaProvided[i][0] < Gridinfo.AllGrids[i].Length && XCheck + CheckAreaProvided[i][0] >= 0)
                    {
                        ReturnVar[CurrentAt] = new Vector2(XCheck + CheckAreaProvided[i][0], YCheck + CheckAreaProvided[i][1]);
                        CurrentAt++;
                    }
                }
            }

        }
        return ReturnVar;
    }
    public bool InMultiAreaGridCheck(int x, int y, int[][] MultiAreaGridCheck)
    {
        for (int i = 0; i < MultiAreaGridCheck.Length; i++)
        {
            if (x <= MultiAreaGridCheck[i][2] && x >= MultiAreaGridCheck[i][0] && y <= MultiAreaGridCheck[i][3] && y >= MultiAreaGridCheck[i][1])
            {
                return true;
            }

        }
        return false;
    }
    //Changes the parameters of BotAi Grids
    public virtual void SelectionAdjustment()
    {
        MouseFollowingUI.IsSelecting = true;
        MouseFollowingUI.ObstacleSelectAllowed = true;
        MouseFollowingUI.CharacterSelectAllowed = true;
        MouseFollowingUI.WillGroupSelect = false;
        for (int i = 0; i < AreaCanClick.Length; i++)
        {
            //-69 is the signal to null out a SelectionSquare
            if (AreaCanClick[i][0] != -69 && AreaCanClick[i][1] != -69 && AreaCanClick[i][2] != -69 && AreaCanClick[i][3] != -69)
            {
                MouseFollowingUI.AllowedSelected[i][1] = (int)gameObject.GetComponent<CharacterBase>().CharacterLocationIndex.y + AreaCanClick[i][1];
                MouseFollowingUI.AllowedSelected[i][2] = (int)gameObject.GetComponent<CharacterBase>().CharacterLocationIndex.x + AreaCanClick[i][2];
                MouseFollowingUI.AllowedSelected[i][3] = (int)gameObject.GetComponent<CharacterBase>().CharacterLocationIndex.y + AreaCanClick[i][3];
                MouseFollowingUI.AllowedSelected[i][0] = (int)gameObject.GetComponent<CharacterBase>().CharacterLocationIndex.x + AreaCanClick[i][0];
            }
        }
    }
    //FindsCharacters
    public virtual GameObject[] AreaCheck(int XStart, int YStart, int XLength, int YLength)
    {
        CharactersGathered = 0;
        CharacterGatherer = new GameObject[6];
        for (int x = 0; x < XLength; x++)
        {
            for (int y = 0; y < YLength; y++)
            {
                if (x + XStart >= 0 && y + YStart >= 0 && x + XStart < Gridinfo.XWidthPublic && y + YStart < Gridinfo.YWidthPublic)
                {
                    if (Gridinfo.AllGrids[YStart + y][XStart + x].gameObject.GetComponent<GridControl>().CharacterOn != null)
                    {
                        CharacterGatherer[CharactersGathered] = Gridinfo.AllGrids[YStart + y][XStart + x].gameObject.GetComponent<GridControl>().CharacterOn;
                        CharactersGathered += 1;
                    }
                }
            }
        }
        return CharacterGatherer;
    }
    public virtual GameObject[] AreaEffect(int XStart, int YStart, int XLength, int YLength)
    {
        CharactersGathered = 0;
        CharacterGatherer = new GameObject[6];
        if (MoveSprite != null)
        {
            for (int x = 0; x < XLength; x++)
            {
                for (int y = 0; y < YLength; y++)
                {
                    if (x + XStart >= 0 && y + YStart >= 0 && x + XStart < Gridinfo.XWidthPublic && y + YStart < Gridinfo.YWidthPublic)
                    {
                        Effects[EffectAmount] = Instantiate(MoveSprite, new Vector3(Gridinfo.AllGrids[YStart + y][XStart + x].transform.position.x + AdjustSprite.x, Gridinfo.AllGrids[YStart + y][XStart + x].transform.position.y + AdjustSprite.y), Quaternion.identity.normalized);
                        Effects[EffectAmount].GetComponent<EffectsLifeTime>().TimeTillSelfDestruct = (float)0.5;
                        EffectAmount++;
                    }
                }
            }
        }
        return CharacterGatherer;
    }
    //Highlights a bunch of grids
    public virtual void AreaHighLightToggle(int[][] CheckAreaProvided, bool IsEnemy, bool IsOn)
    {
        if (CheckAreaProvided != null)
        {
            for (int i = 0; i < CheckAreaProvided.Length; i++)
            {
                for (int XCheck = 0; XCheck <= CheckAreaProvided[i][2] - CheckAreaProvided[i][0]; XCheck++)
                {
                    for (int YCheck = 0; YCheck <= CheckAreaProvided[i][3] - CheckAreaProvided[i][1]; YCheck++)
                    {
                        if (YCheck + CheckAreaProvided[i][1] < Gridinfo.AllGrids.Length && YCheck + CheckAreaProvided[i][1] >= 0 && XCheck + CheckAreaProvided[i][0] < Gridinfo.AllGrids[0].Length && XCheck + CheckAreaProvided[i][0] >= 0)
                        {
                            GridControl CurrentGrid;
                            CurrentGrid = Gridinfo.AllGrids[YCheck + CheckAreaProvided[i][1]][XCheck + CheckAreaProvided[i][0]].GetComponent<GridControl>();
                            if (IsEnemy)
                            {
                                CurrentGrid.IsDamageTargeted = IsOn;
                            }
                            else
                            {
                                CurrentGrid.IsTargeted = IsOn;
                            }
                        }
                    }
                }

            }
        }
    }
    //Creates the radius for things it can do
    //Creates a new area of effect based on two combined grids
    public virtual int[][] NewAreaEffectAdjust(int[][] OldAreaEffect, int[][] AreaEffectAdjust)
    {
        int[][] NewAreaEffect = new int[OldAreaEffect.Length * AreaEffectAdjust.Length][];
        for(int i = 0; i < NewAreaEffect.Length; i++)
        {
            NewAreaEffect[i] = new int[4];
        }
        for(int i = 0; i < OldAreaEffect.Length; i++)
        {
            for (int z = 0; z < AreaEffectAdjust.Length; z++)
            {
                if(OldAreaEffect[i][0] != -69 && OldAreaEffect[i][1] != -69 && OldAreaEffect[i][2] != -69 && OldAreaEffect[i][3] != -69 && AreaEffectAdjust[z][0] != -69 && AreaEffectAdjust[z][1] != -69 && AreaEffectAdjust[z][2] != -69 && AreaEffectAdjust[z][3] != -69)
                {
                    NewAreaEffect[z + (i * AreaEffectAdjust.Length)][0] = AreaEffectAdjust[z][0] + OldAreaEffect[i][0];
                    NewAreaEffect[z + (i * AreaEffectAdjust.Length)][1] = AreaEffectAdjust[z][1] + OldAreaEffect[i][1];
                    NewAreaEffect[z + (i * AreaEffectAdjust.Length)][2] = AreaEffectAdjust[z][2] + OldAreaEffect[i][2];
                    NewAreaEffect[z + (i * AreaEffectAdjust.Length)][3] = AreaEffectAdjust[z][3] + OldAreaEffect[i][3];
                }
                else
                {
                    NewAreaEffect[z + (i * AreaEffectAdjust.Length)][0] = -69;
                    NewAreaEffect[z + (i * AreaEffectAdjust.Length)][1] = -69;
                    NewAreaEffect[z + (i * AreaEffectAdjust.Length)][2] = -69;
                    NewAreaEffect[z + (i * AreaEffectAdjust.Length)][3] = -69;
                }
            }
        }
        return NewAreaEffect;
    }

    public virtual int[][] NewAreaEffectMove(int[][] OldAreaEffect, int XMove, int YMove)
    {
        int[][] NewAreaEffect = new int[OldAreaEffect.Length][];
        for (int i = 0; i < NewAreaEffect.Length; i++)
        {
            NewAreaEffect[i] = new int[4];
        }
        for (int i = 0; i < OldAreaEffect.Length; i++)
        {
            NewAreaEffect[i][0] = OldAreaEffect[i][0] + XMove;
            NewAreaEffect[i][2] = OldAreaEffect[i][2] + XMove;
            NewAreaEffect[i][1] = OldAreaEffect[i][1] + YMove;
            NewAreaEffect[i][3] = OldAreaEffect[i][3] + YMove;
        }
        return NewAreaEffect;
    }
    // Gives out the Location action of something to an location -69,-69 is the default value when the Location is not found
    public virtual Vector2 CheckIfLocationCorrespondsToAction(int XCheck, int YCheck, int[][] AreaEffectAdjust,int[][] OldAreaEffect)
    {
        Vector2 LocationAction = new Vector2(-69,-69);
        if(AreaEffectAdjust.Length != null && OldAreaEffect.Length != null)
        {
            for (int i = 0; i < OldAreaEffect.Length; i++)
            {
                for (int x = 0; x <= OldAreaEffect[i][2] - OldAreaEffect[i][0]; x++)
                {
                    for (int y = 0; y <= OldAreaEffect[i][3] - OldAreaEffect[i][1]; y++)
                    {
                        for (int z = 0; z < AreaEffectAdjust.Length; z++)
                        {
                            if (OldAreaEffect[i][0] + x >= 0 && OldAreaEffect[i][1] + y >= 0 && OldAreaEffect[i][0] + x < Gridinfo.XWidthPublic && OldAreaEffect[i][1] + y < Gridinfo.YWidthPublic && OldAreaEffect[i][0] != -69 && OldAreaEffect[i][1] != -69 && OldAreaEffect[i][2] != -69 && OldAreaEffect[i][3] != -69 && AreaEffectAdjust[z][0] + OldAreaEffect[i][0] + x <= XCheck && AreaEffectAdjust[z][2] + OldAreaEffect[i][0] + x >= XCheck && AreaEffectAdjust[z][1] + OldAreaEffect[i][1] + y <= YCheck && AreaEffectAdjust[z][3] + OldAreaEffect[i][1] + y >= YCheck)
                            {
                                LocationAction = new Vector2(OldAreaEffect[i][0] + x, OldAreaEffect[i][01] + y);
                            }
                        }
                    }
                }
            }
        }
        return LocationAction;
    }
    // Update is called once per frame
    /*
     *  Use This to CheckAreas
     *  gameObject.GetComponent<CharacterBase>().action = "inactive";
        for(int x = 0; x < AreaCheck((int)Character_Info.CharacterLocationIndex.x, (int)Character_Info.CharacterLocationIndex.y, 3, 3).Length; x++)
        {
            CharacterBase CheckedCharacterBase;
            if(AreaCheck((int)Character_Info.CharacterLocationIndex.x, (int)Character_Info.CharacterLocationIndex.y, 3, 3)[x] != null)
            {
                if(AreaCheck((int)Character_Info.CharacterLocationIndex.x, (int)Character_Info.CharacterLocationIndex.y, 3, 3)[x].GetComponent<CharacterBase>().IsEnemy = Character_Info.IsEnemy)
                {
                    
                }
            }
        }
    */
    public virtual void Update()
    {
        if (gameObject.name == "name1")
        {
            print("wow");
        }
        //Purely for debug
        AreaSelection = AreaCanSelect[0];
        if(AreaCanClick != null)
        {
            for (int i = 0; i < AreaCanClick.Length; i++)
            {
                AreaCanSelect[i] = new int[4];
                //-69 is the signal to null out a SelectionSquare
                if (AreaCanClick[i] != null && AreaCanClick[i][0] != -69 && AreaCanClick[i][1] != -69 && AreaCanClick[i][2] != -69 && AreaCanClick[i][3] != -69)
                {
                    AreaCanSelect[i][1] = (int)gameObject.GetComponent<CharacterBase>().CharacterLocationIndex.y + AreaCanClick[i][1];
                    AreaCanSelect[i][2] = (int)gameObject.GetComponent<CharacterBase>().CharacterLocationIndex.x + AreaCanClick[i][2];
                    AreaCanSelect[i][3] = (int)gameObject.GetComponent<CharacterBase>().CharacterLocationIndex.y + AreaCanClick[i][3];
                    AreaCanSelect[i][0] = (int)gameObject.GetComponent<CharacterBase>().CharacterLocationIndex.x + AreaCanClick[i][0];
                }
                else
                {
                    AreaCanSelect[i][1] = -69;
                    AreaCanSelect[i][2] = -69;
                    AreaCanSelect[i][3] = -69;
                    AreaCanSelect[i][0] = -69;
                }
            }
        }
        //Purely Debug
        if (willUseForMove && MoveSpacesAllowed != null)
        {
            /*
            print(Character_Info.LocationAction.x + " , " + Character_Info.LocationAction.y);'[
            lp
            print(AreaCanSelect[0][0] + " , " + AreaCanSelect[0][1] + " , " + AreaCanSelect[0][2] + " , " + AreaCanSelect[0][3] + " // " + AreaCanSelect[1][0] + " , " + AreaCanSelect[1][1] + " , " + AreaCanSelect[1][2] + " , " + AreaCanSelect[1][3] + " // " + AreaCanSelect[2][0] + " , " + AreaCanSelect[2][1] + " , " + AreaCanSelect[2][2] + " , " + AreaCanSelect[2][3] + " // " + AreaCanSelect[3][0] + " , " + AreaCanSelect[3][1] + " , " + AreaCanSelect[3][2] + " , " + AreaCanSelect[3][3] + " // " + AreaCanSelect[4][0] + " , " + AreaCanSelect[4][1] + " , " + AreaCanSelect[4][2] + " , " + AreaCanSelect[4][3]);
            print(MoveSpacesAllowedAdjust[0][0] + " , " + MoveSpacesAllowedAdjust[0][1] + " , " + MoveSpacesAllowedAdjust[0][2] + " , " + MoveSpacesAllowedAdjust[0][3] + " // " + MoveSpacesAllowedAdjust[1][0] + " , " + MoveSpacesAllowedAdjust[1][1] + " , " + MoveSpacesAllowedAdjust[1][2] + " , " + MoveSpacesAllowedAdjust[1][3] + " // " + MoveSpacesAllowedAdjust[2][0] + " , " + MoveSpacesAllowedAdjust[2][1] + " , " + MoveSpacesAllowedAdjust[2][2] + " , " + MoveSpacesAllowedAdjust[2][3] + " // " + MoveSpacesAllowedAdjust[3][0] + " , " + MoveSpacesAllowedAdjust[3][1] + " , " + MoveSpacesAllowedAdjust[3][2] + " , " + MoveSpacesAllowedAdjust[3][3] + " // " + MoveSpacesAllowedAdjust[4][0] + " , " + MoveSpacesAllowedAdjust[4][1] + " , " + MoveSpacesAllowedAdjust[4][2] + " , " + MoveSpacesAllowedAdjust[4][3]);
            print(CheckIfLocationCorrespondsToAction(3, 0, MoveSpacesAllowedAdjust, AreaCanSelect));
            print("START________________________________________________________________________________________________________________");
            for ( int i = 0; i < MoveSpacesAllowed.Length; i++)
            {
            print(MoveSpacesAllowed[i][0] + " , " + MoveSpacesAllowed[i][1] + " , " + MoveSpacesAllowed[i][2] + " , " + MoveSpacesAllowed[i][3] + " // ");
            }
            print("END________________________________________________________________________________________________________________");
           */
        }

        /*if(Character_Info.IsEnemy == true)
        {
        */
        if (willUseForMove)
            {
                MoveSpaceBackTrackAdjust = MoveSpacesAllowedAdjust;
                for(int i = 0; i < MoveSpaceBackTrackAdjust.Length; i++)
                {
                    for (int z = 0; z < MoveSpaceBackTrackAdjust[i].Length; z++)
                    {
                        MoveSpaceBackTrackAdjust[i][z] = MoveSpaceBackTrackAdjust[i][z] * -1;
                    }
                } 
                MoveSpacesAllowed = NewAreaEffectAdjust(AreaCanSelect, MoveSpacesAllowedAdjust);
                MoveSpaceBackTrackAllowed = NewAreaEffectAdjust(AreaCanSelect, MoveSpaceBackTrackAdjust);
            }
            if (willUseForGridEffect)
            {
                PlayerSpacesAllowed = NewAreaEffectAdjust(AreaCanSelect, PlayerSpacesAllowedAdjust);
            }
            if (willUseForEnemyMove)
            {
                EnemyMoveSpacesAllowed = NewAreaEffectAdjust(AreaCanSelect , EnemyMoveAllowedAdjust);
            }
            if (willUseForAllyMove)
            {
                AllyMoveSpacesAllowed = NewAreaEffectAdjust(AreaCanSelect , AllyMoveAllowedAdjust);
            }
        /*
    }
        */
    }
    public virtual void ChangeAnim(float time)
    {
        Character_Info.CharacterSChanger.SetSprite(time, 2);
    }
    public virtual void ActivateMove()
    {
        EffectAmount = 0;
        gameObject.GetComponent<CharacterBase>().action = "inactive";
        Effects[0] = Instantiate(MoveSprite, new Vector3(Gridinfo.AllGrids[(int)Character_Info.CharacterLocationIndex.y][(int)Character_Info.CharacterLocationIndex.x].transform.position.x+AdjustSprite.x, Gridinfo.AllGrids[(int)Character_Info.CharacterLocationIndex.y][(int)Character_Info.CharacterLocationIndex.x].transform.position.y + AdjustSprite.y), Quaternion.identity.normalized);
        Effects[0].GetComponent<EffectsLifeTime>().TimeTillSelfDestruct = 1;

    }
}
