using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MovementUI : MonoBehaviour
{
    GenericMove CurrentMoveOn;
    [SerializeField]
    Color OrignialColor;
    //DiffrentScenesOfUi
    //Main Hub = "main"
    //Special Move = "SPMove"
    //SpecialMove = "SPMove" + [Index of special move]
    //Move = "Move"
    //Shoot = "shoot"
    //ChargingText = "ChargeInterrupt"
    //Inactive = "Inactive"
    //Waiting for enemy = "EnemyWait"
    public string Scenes;
    public bool HasEstablishedScene;
    //EventSystem Acsess
    [SerializeField]
    EventSystem EventSystemCommunication;
    [SerializeField]
    MoveSystem MoveSystemCommunication;
    //Storing Things
    [SerializeField]
    //Move = "Move"
    //Shoot = "Shoot"
    //SpecialMove = [Index of special move]
    public string[] StoredActions = new string[3];
    [SerializeField]
    public Vector2[] StoredTargetCoordinates = new Vector2[3];
    [SerializeField]
    public GameObject[] CurrentCharactersInPlay = new GameObject[3];
    [SerializeField]
    public GameObject[] Enemies = new GameObject[3];
    [SerializeField]
    public BotAi[] EnemiesAi = new BotAi[3];
    CharacterSpawner CharacterSpawnerInfo;
    //Mouse select grid UI
    [SerializeField]
    public MouseFollow MouseFollowingUI;
    [SerializeField]
    public int CurrentCharacterInPlay;
    //Text
    [SerializeField]
    string TextDisplayed;
    [SerializeField]
    GameObject Text;
    //Buttons
    [SerializeField]
    GameObject ContinueButton;
    [SerializeField]
    Vector3 ContinueMoveCoordinate;
    [SerializeField]
    GameObject ShootButton;
    [SerializeField]
    Vector3 ShootButtonCoordinate;
    [SerializeField]
    GameObject MoveButton;
    [SerializeField]
    Vector3 MoveButtonCoordinate;
    [SerializeField]
    GameObject SpecialMoveButton;
    [SerializeField]
    Vector3 SpecialMoveButtonCoordinate;
    [SerializeField]
    GameObject ReturnButtonMove;
    [SerializeField]
    Vector3 ReturnButtonMoveCoordinate;
    [SerializeField]
    GameObject ReturnToPreviousButton;
    [SerializeField]
    Vector3 ReturnToPreviousCoordinate;
    [SerializeField]
    GameObject SpecialMoveActivateButtonTemplate;
    [SerializeField]
    Vector3 SpecialTemplate1;
    [SerializeField]
    Vector3 SpecialTemplate2;
    [SerializeField]
    Vector3 SpecialTemplate3;
    [SerializeField]
    Vector3 SpecialTemplate4;
    [SerializeField]
    Vector3 SPReturn;
    [SerializeField]
    Vector3 SPReturn2;
    [SerializeField]
    GameObject[] Buttons = new GameObject[5];
    //Text
    [SerializeField]
    GameObject TextUI;
    [SerializeField]
    Vector2 TextLocation;
    [SerializeField]
    string text;
    public bool HasClicked;
    //Random enemy load thing
    public float EnemyWaitLoadDotCoolDownLeft;
    public float AmountOfDots;
    // Start is called before the first frame update
    void Start()
    {
        AmountOfDots = 1;
        CharacterSpawnerInfo = Camera.main.gameObject.GetComponent<CharacterSpawner>();
        MoveSystemCommunication = Camera.main.gameObject.GetComponent<MoveSystem>();
        Enemies[0] = MoveSystemCommunication.CharacterOnField[3];
        Enemies[1] = MoveSystemCommunication.CharacterOnField[4];
        Enemies[2] = MoveSystemCommunication.CharacterOnField[5];
        //Mousefollow object needs to be called MouseObject
        MouseFollowingUI = GameObject.Find("MouseObject").GetComponent<MouseFollow>();
    }
    public string StartUIScene()
    {
        if (Scenes == "Inactive" || Scenes == null || Scenes == "")
        {
            CurrentCharacterInPlay = 0;
            Scenes = "main";
            HasEstablishedScene = false;
            return "Success";
        }
        else
        {
            return "error already in play";
        }
    }
    // Update is called once per frame
    void Update()
    {
        Enemies[0] = MoveSystemCommunication.CharacterOnField[3];
        Enemies[1] = MoveSystemCommunication.CharacterOnField[4];
        Enemies[2] = MoveSystemCommunication.CharacterOnField[5];
        if (EnemiesAi[2] == null && EnemiesAi[1] == null && EnemiesAi[0] == null)
        {
            EnemiesAi[0] = Enemies[0].GetComponent<BotAi>();
            EnemiesAi[1] = Enemies[1].GetComponent<BotAi>();
            EnemiesAi[2] = Enemies[2].GetComponent<BotAi>();
        }
        //print(EnemiesAi[0].IsMakingDecison + " // " + EnemiesAi[1].IsMakingDecison + " // " + EnemiesAi[2].IsMakingDecison);
        if (CurrentCharacterInPlay > 2 || CurrentCharacterInPlay >= CurrentCharactersInPlay.Length || Scenes == "EnemyWait")
        {
            bool IsCurrentlyMakingDecison;
            IsCurrentlyMakingDecison = false;
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i].GetComponent<BotAi>() != null && Enemies[i].GetComponent<BotAi>().IsMakingDecison == true)
                {
                    IsCurrentlyMakingDecison = true;
                }
            }
            if (IsCurrentlyMakingDecison == false)
            {
                for (int x = 0; x < CurrentCharactersInPlay.Length; x++)
                {
                        CurrentCharactersInPlay[x].GetComponent<CharacterBase>().PushAction(StoredTargetCoordinates[x], StoredActions[x], EventSystemCommunication);
     
                }
                //We only need to make it active becuase it resets on its own
                EventSystemCommunication.active = true;
                CurrentCharacterInPlay = 0;
                Scenes = "Inactive";
            }
            else
            {
                CurrentCharacterInPlay = 0;
                Scenes = "EnemyWait";
            }
        }
        CharacterBase CurrentCharacterBase = CurrentCharactersInPlay[CurrentCharacterInPlay].GetComponent<CharacterBase>();
        if (Scenes == null || Scenes == "")
        {
            Scenes = "Inactive";
        }

        if (Scenes == "Inactive")
        {
            TextUI.GetComponent<TextMeshProUGUI>().text = "";
            if (Buttons[0] != null)
            {
                Destroy(Buttons[0]);
            }
            if (Buttons[1] != null)
            {
                Destroy(Buttons[1]);
            }
            if (Buttons[2] != null)
            {
                Destroy(Buttons[2]);
            }
            if (Buttons[3] != null)
            {
                Destroy(Buttons[3]);
            }
            if (Buttons[4] != null)
            {
                Destroy(Buttons[4]);
            }
        }
        if (Scenes == "main")
        {
            if(CurrentCharactersInPlay[CurrentCharacterInPlay].GetComponent<CharacterBase>()!= null && CurrentCharactersInPlay[CurrentCharacterInPlay].GetComponent<CharacterBase>().IsDead == false)
            {
                if (CurrentCharacterBase.CharacterSChanger != null )
                {
                    CurrentCharacterBase.CharacterSChanger.SetSprite(-69, 6);
                }
                if (CurrentMoveOn != null)
                {
                    for (int z = 0; z < MouseFollowingUI.AllowedSelected.Length; z++)
                    {
                        for (int XCurrent = MouseFollowingUI.AllowedSelected[z][0]; XCurrent <= MouseFollowingUI.AllowedSelected[z][2]; XCurrent++)
                        {
                            for (int YCurrent = MouseFollowingUI.AllowedSelected[z][1]; YCurrent <= MouseFollowingUI.AllowedSelected[z][3]; YCurrent++)
                            {
                                if (XCurrent >= 0 && XCurrent < CurrentMoveOn.Gridinfo.AllGrids[0].Length && YCurrent >= 0 && YCurrent < CurrentMoveOn.Gridinfo.AllGrids.Length && CurrentMoveOn.Gridinfo.AllGrids[YCurrent][XCurrent].GetComponent<SpriteRenderer>().color == Color.green)
                                {
                                    CurrentMoveOn.Gridinfo.AllGrids[YCurrent][XCurrent].GetComponent<SpriteRenderer>().color = OrignialColor;
                                }
                            }
                        }
                    }
                    MouseFollowingUI.OriginalColor = OrignialColor;
                    MouseFollowingUI.WipeAllowedSelected();
                    CurrentMoveOn = null;
                }
                if (CurrentCharactersInPlay[CurrentCharacterInPlay].GetComponent<CharacterBase>().IsCharging == true)
                {
                    Scenes = "Charging";
                    HasEstablishedScene = false;
                }
                else if (HasEstablishedScene == false)
                {

                    switch (CurrentCharacterInPlay)
                    {
                        case 0:
                            TextUI.GetComponent<TextMeshProUGUI>().text = "What will Sandman do";
                            break;
                        case 1:
                            TextUI.GetComponent<TextMeshProUGUI>().text = "What will Jade do";
                            break;
                        case 2:
                            TextUI.GetComponent<TextMeshProUGUI>().text = "What will Aurthur do";
                            break;
                    }
                    if (Buttons[0] != null)
                    {
                        Destroy(Buttons[0]);
                    }
                    if (Buttons[1] != null)
                    {
                        Destroy(Buttons[1]);
                    }
                    if (Buttons[2] != null)
                    {
                        Destroy(Buttons[2]);
                    }
                    if (Buttons[3] != null)
                    {
                        Destroy(Buttons[3]);
                    }
                    if (Buttons[4] != null)
                    {
                        Destroy(Buttons[4]);
                    }
                    //Comehere to change position of buttons
                    HasEstablishedScene = true;
                    Buttons[0] = Instantiate(MoveButton);
                    Buttons[0].GetComponent<ButtonBase>().UIBase = gameObject;
                    Buttons[0].transform.SetParent(gameObject.transform.parent);
                    Buttons[1] = Instantiate(ShootButton);
                    Buttons[1].GetComponent<ButtonBase>().UIBase = gameObject;
                    Buttons[1].transform.SetParent(gameObject.transform.parent);
                    Buttons[2] = Instantiate(SpecialMoveButton);
                    Buttons[2].GetComponent<ButtonBase>().UIBase = gameObject;
                    Buttons[2].transform.SetParent(gameObject.transform.parent);
                    Buttons[3] = Instantiate(ReturnToPreviousButton);
                    Buttons[3].GetComponent<ButtonBase>().UIBase = gameObject;
                    Buttons[3].transform.SetParent(gameObject.transform.parent);
                    Buttons[0].transform.position = MoveButtonCoordinate;
                    Buttons[1].transform.position = ShootButtonCoordinate;
                    Buttons[2].transform.position = SpecialMoveButtonCoordinate;
                    Buttons[3].transform.position = ReturnToPreviousCoordinate;
                }
            }
            else
            {
                CurrentCharacterInPlay += 1;
                HasEstablishedScene = false;
            }

        }
        if (Scenes == "shoot")
        {
            if (HasEstablishedScene == false)
            {
                
                TextUI.GetComponent<TextMeshProUGUI>().text = "Select a angle to shoot";
                if (Buttons[0] != null)
                {
                    Destroy(Buttons[0]);
                }
                if (Buttons[1] != null)
                {
                    Destroy(Buttons[1]);
                }
                if (Buttons[2] != null)
                {
                    Destroy(Buttons[2]);
                }
                if (Buttons[3] != null)
                {
                    Destroy(Buttons[3]);
                }
                if (Buttons[4] != null)
                {
                    Destroy(Buttons[4]);
                }
                Buttons[0] = Instantiate(ReturnButtonMove);
                Buttons[0].GetComponent<ButtonBase>().UIBase = gameObject;
                Buttons[0].transform.SetParent(gameObject.transform.parent);
                Buttons[0].transform.position = ReturnButtonMoveCoordinate;
                MouseFollowingUI.IsSelecting = true;
                MouseFollowingUI.WillGroupSelect = false;
                HasEstablishedScene = true;
                MouseFollowingUI.ObstacleSelectAllowed = true;
                MouseFollowingUI.CharacterSelectAllowed = true;
                MouseFollowingUI.AllowedSelected[0][0] = (int)CurrentCharacterBase.CharacterLocationIndex.x - 1;
                MouseFollowingUI.AllowedSelected[0][1] = (int)CurrentCharacterBase.CharacterLocationIndex.y;
                MouseFollowingUI.AllowedSelected[0][2] = (int)CurrentCharacterBase.CharacterLocationIndex.x - 1;
                MouseFollowingUI.AllowedSelected[0][3] = (int)CurrentCharacterBase.CharacterLocationIndex.y;
                MouseFollowingUI.AllowedSelected[1][0] = (int)CurrentCharacterBase.CharacterLocationIndex.x + 1;
                MouseFollowingUI.AllowedSelected[1][1] = (int)CurrentCharacterBase.CharacterLocationIndex.y;
                MouseFollowingUI.AllowedSelected[1][2] = (int)CurrentCharacterBase.CharacterLocationIndex.x + 1;
                MouseFollowingUI.AllowedSelected[1][3] = (int)CurrentCharacterBase.CharacterLocationIndex.y;
                MouseFollowingUI.AllowedSelected[2][0] = (int)CurrentCharacterBase.CharacterLocationIndex.x;
                MouseFollowingUI.AllowedSelected[2][1] = (int)CurrentCharacterBase.CharacterLocationIndex.y - 1;
                MouseFollowingUI.AllowedSelected[2][2] = (int)CurrentCharacterBase.CharacterLocationIndex.x;
                MouseFollowingUI.AllowedSelected[2][3] = (int)CurrentCharacterBase.CharacterLocationIndex.y - 1;
                MouseFollowingUI.AllowedSelected[3][0] = (int)CurrentCharacterBase.CharacterLocationIndex.x;
                MouseFollowingUI.AllowedSelected[3][1] = (int)CurrentCharacterBase.CharacterLocationIndex.y + 1;
                MouseFollowingUI.AllowedSelected[3][2] = (int)CurrentCharacterBase.CharacterLocationIndex.x;
                MouseFollowingUI.AllowedSelected[3][3] = (int)CurrentCharacterBase.CharacterLocationIndex.y + 1;
            }
            if (Input.GetAxis("Click") != 0)
            {
                if (MouseFollowingUI.OnGrid == true && HasClicked == false)
                {
                    if (MouseFollowingUI.MousePositionGridChoose.x == (int)CurrentCharactersInPlay[CurrentCharacterInPlay].GetComponent<CharacterBase>().CharacterLocationIndex.x || MouseFollowingUI.MousePositionGridChoose.y == (int)CurrentCharactersInPlay[CurrentCharacterInPlay].GetComponent<CharacterBase>().CharacterLocationIndex.y)
                    {
                        CurrentCharacterBase.CharacterSChanger.SetSprite(-69, 0);
                        StoredActions[CurrentCharacterInPlay] = "Shoot";
                        //StoredTargetCoordinates acts as velocity storer for the gun
                        StoredTargetCoordinates[CurrentCharacterInPlay] = new Vector2(MouseFollowingUI.MousePositionGridChoose.x - (int)CurrentCharactersInPlay[CurrentCharacterInPlay].GetComponent<CharacterBase>().CharacterLocationIndex.x, MouseFollowingUI.MousePositionGridChoose.y - (int)CurrentCharactersInPlay[CurrentCharacterInPlay].GetComponent<CharacterBase>().CharacterLocationIndex.y);
                        CurrentCharacterInPlay += 1;
                        Scenes = "main";
                        MouseFollowingUI.IsSelecting = false;
                        HasEstablishedScene = false;
                        MouseFollowingUI.WipeGroupSelect();
                        MouseFollowingUI.WipeAllowedSelected();
                    }
                }
                HasClicked = true;
            }
            else
            {
                HasClicked = false;
            }
        }
        if (Scenes == "Move")
        {
            if (HasEstablishedScene == false)
            {
                TextUI.GetComponent<TextMeshProUGUI>().text = "Select a place to move";
                if (Buttons[0] != null)
                {
                    Destroy(Buttons[0]);
                }
                if (Buttons[1] != null)
                {
                    Destroy(Buttons[1]);
                }
                if (Buttons[2] != null)
                {
                    Destroy(Buttons[2]);
                }
                if (Buttons[3] != null)
                {
                    Destroy(Buttons[3]);
                }
                if (Buttons[4] != null)
                {
                    Destroy(Buttons[4]);
                }
                Buttons[0] = Instantiate(ReturnButtonMove);
                Buttons[0].GetComponent<ButtonBase>().UIBase = gameObject;
                Buttons[0].transform.SetParent(gameObject.transform.parent);
                Buttons[0].transform.position = ReturnButtonMoveCoordinate;
                MouseFollowingUI.WillGroupSelect = false;
                MouseFollowingUI.IsSelecting = true;
                HasEstablishedScene = true;
                MouseFollowingUI.ObstacleSelectAllowed = false;
                MouseFollowingUI.CharacterSelectAllowed = false;
                MouseFollowingUI.AllowedSelected[0][0] = (int)CurrentCharactersInPlay[CurrentCharacterInPlay].GetComponent<CharacterBase>().CharacterLocationIndex.x - 1;
                MouseFollowingUI.AllowedSelected[0][1] = (int)CurrentCharactersInPlay[CurrentCharacterInPlay].GetComponent<CharacterBase>().CharacterLocationIndex.y - 1;
                MouseFollowingUI.AllowedSelected[0][2] = (int)CurrentCharactersInPlay[CurrentCharacterInPlay].GetComponent<CharacterBase>().CharacterLocationIndex.x + 1;
                MouseFollowingUI.AllowedSelected[0][3] = (int)CurrentCharactersInPlay[CurrentCharacterInPlay].GetComponent<CharacterBase>().CharacterLocationIndex.y + 1;
            }
            if (Input.GetAxis("Click") != 0)
            {
                if (MouseFollowingUI.OnGrid == true && HasClicked == false)
                {
                    CurrentCharacterBase.CharacterSChanger.SetSprite(-69, 0);
                    StoredActions[CurrentCharacterInPlay] = "Move";
                    StoredTargetCoordinates[CurrentCharacterInPlay] = MouseFollowingUI.MousePositionGridChoose;
                    CurrentCharacterInPlay += 1;
                    Scenes = "main";
                    MouseFollowingUI.IsSelecting = false;
                    HasEstablishedScene = false;
                    if (OrignialColor.a != 0)
                    {
                        MouseFollowingUI.OriginalColor = OrignialColor;
                    }
                    
                    MouseFollowingUI.WipeGroupSelect();
                    MouseFollowingUI.WipeAllowedSelected();
                    if (CurrentCharacterInPlay < CurrentCharactersInPlay.Length && CurrentCharactersInPlay[CurrentCharacterInPlay].GetComponent<CharacterBase>().IsEnemy == false)
                    {
                        CurrentCharactersInPlay[CurrentCharacterInPlay].GetComponent<CharacterBase>().GridData.AllGrids[(int)MouseFollowingUI.MousePositionGridChoose.y][(int)MouseFollowingUI.MousePositionGridChoose.x].GetComponent<GridControl>().IsTargeted = true;
                    }
                }
                HasClicked = true;
            }
            else
            {
                HasClicked = false;
            }
        }
        if (Scenes == "SPMove")
        {
            if (HasEstablishedScene == false)
            {
                TextUI.GetComponent<TextMeshProUGUI>().text = "Select a special move";
                if (Buttons[0] != null)
                {
                    Destroy(Buttons[0]);
                }
                if (Buttons[1] != null)
                {
                    Destroy(Buttons[1]);
                }
                if (Buttons[2] != null)
                {
                    Destroy(Buttons[2]);
                }
                if (Buttons[3] != null)
                {
                    Destroy(Buttons[3]);
                }
                if (Buttons[4] != null)
                {
                    Destroy(Buttons[4]);
                }
                ;
                Buttons[0] = Instantiate(SpecialMoveActivateButtonTemplate);
                Buttons[0].GetComponent<ButtonBase>().UIBase = gameObject;
                Buttons[0].GetComponent<SpecialMoveFurthurButton>().Character = CurrentCharactersInPlay[CurrentCharacterInPlay];
                Buttons[0].GetComponent<SpecialMoveFurthurButton>().index = 0;
                Buttons[0].transform.SetParent(gameObject.transform.parent);
                Buttons[1] = Instantiate(SpecialMoveActivateButtonTemplate);
                Buttons[1].GetComponent<ButtonBase>().UIBase = gameObject;
                Buttons[1].GetComponent<SpecialMoveFurthurButton>().Character = CurrentCharactersInPlay[CurrentCharacterInPlay];
                Buttons[1].GetComponent<SpecialMoveFurthurButton>().index = 1;
                Buttons[1].transform.SetParent(gameObject.transform.parent);
                Buttons[2] = Instantiate(SpecialMoveActivateButtonTemplate);
                Buttons[2].GetComponent<ButtonBase>().UIBase = gameObject;
                Buttons[2].GetComponent<SpecialMoveFurthurButton>().Character = CurrentCharactersInPlay[CurrentCharacterInPlay];
                Buttons[2].GetComponent<SpecialMoveFurthurButton>().index = 2;
                Buttons[2].transform.SetParent(gameObject.transform.parent);
                Buttons[3] = Instantiate(SpecialMoveActivateButtonTemplate);
                Buttons[3].GetComponent<ButtonBase>().UIBase = gameObject;
                Buttons[3].GetComponent<SpecialMoveFurthurButton>().Character = CurrentCharactersInPlay[CurrentCharacterInPlay];
                Buttons[3].GetComponent<SpecialMoveFurthurButton>().index = 3;
                Buttons[3].transform.SetParent(gameObject.transform.parent);
                Buttons[4] = Instantiate(ReturnButtonMove);
                Buttons[4].GetComponent<ButtonBase>().UIBase = gameObject;
                Buttons[4].transform.SetParent(gameObject.transform.parent);
                Buttons[0].transform.position = SpecialTemplate1;
                Buttons[1].transform.position = SpecialTemplate2;
                Buttons[2].transform.position = SpecialTemplate3;
                Buttons[3].transform.position = SpecialTemplate4;
                Buttons[4].transform.position = SPReturn;
                HasEstablishedScene = true;
            }
        }
        for (int x = 0; x < 4; x++)
        {
            if (Scenes == "SPMove" + x)
            {
                CurrentMoveOn = CurrentCharacterBase.MovesAllowed[x];
                if (HasEstablishedScene == false)
                {

                    TextUI.GetComponent<TextMeshProUGUI>().text = "Pick a tile for " + CurrentCharactersInPlay[CurrentCharacterInPlay].name + " to cast " + CurrentMoveOn.GetType().Name;
                    if (Buttons[0] != null)
                    {
                        Destroy(Buttons[0]);
                    }
                    if (Buttons[1] != null)
                    {
                        Destroy(Buttons[1]);
                    }
                    if (Buttons[2] != null)
                    {
                        Destroy(Buttons[2]);
                    }
                    if (Buttons[3] != null)
                    {
                        Destroy(Buttons[3]);
                    }
                    HasEstablishedScene = true;
                    
                    CurrentMoveOn.SelectionAdjustment();
                    for (int z = 0; z < MouseFollowingUI.AllowedSelected.Length; z++)
                    {
                        for (int XCurrent = MouseFollowingUI.AllowedSelected[z][0]; XCurrent <= MouseFollowingUI.AllowedSelected[z][2]; XCurrent++)
                        {
                            for (int YCurrent = MouseFollowingUI.AllowedSelected[z][1]; YCurrent <= MouseFollowingUI.AllowedSelected[z][3]; YCurrent++)
                            {
                                if (XCurrent >= 0 && XCurrent < CurrentMoveOn.Gridinfo.AllGrids[0].Length && YCurrent >= 0 && YCurrent < CurrentMoveOn.Gridinfo.AllGrids.Length)
                                {
                                    OrignialColor = CurrentMoveOn.Gridinfo.AllGrids[YCurrent][XCurrent].GetComponent<SpriteRenderer>().color;
                                }
                            }
                        }
                    }
                    
                    for (int z = 0; z < MouseFollowingUI.AllowedSelected.Length; z++)
                    {
                        for (int XCurrent = MouseFollowingUI.AllowedSelected[z][0]; XCurrent <= MouseFollowingUI.AllowedSelected[z][2]; XCurrent++)
                        {
                            for (int YCurrent = MouseFollowingUI.AllowedSelected[z][1]; YCurrent <= MouseFollowingUI.AllowedSelected[z][3]; YCurrent++)
                            {
                                if (XCurrent >= 0 && XCurrent < CurrentMoveOn.Gridinfo.AllGrids[0].Length && YCurrent >= 0 && YCurrent < CurrentMoveOn.Gridinfo.AllGrids.Length)
                                {
                                    CurrentMoveOn.Gridinfo.AllGrids[YCurrent][XCurrent].GetComponent<SpriteRenderer>().color = Color.green;
                                }
                            }
                        }
                    }
                    Buttons[4].transform.position = SPReturn2;
                }
                if (Input.GetAxis("Click") != 0)
                {
                    if (MouseFollowingUI.OnGrid == true && HasClicked == false)
                    {
                        for (int z = 0; z < MouseFollowingUI.AllowedSelected.Length; z++)
                        {
                            for (int XCurrent = MouseFollowingUI.AllowedSelected[z][0]; XCurrent <= MouseFollowingUI.AllowedSelected[z][2]; XCurrent++)
                            {
                                for (int YCurrent = MouseFollowingUI.AllowedSelected[z][1]; YCurrent <= MouseFollowingUI.AllowedSelected[z][3]; YCurrent++)
                                {
                                    if (XCurrent >= 0 && XCurrent < CurrentMoveOn.Gridinfo.AllGrids[0].Length && YCurrent >= 0 && YCurrent < CurrentMoveOn.Gridinfo.AllGrids.Length && CurrentMoveOn.Gridinfo.AllGrids[YCurrent][XCurrent].GetComponent<SpriteRenderer>().color == Color.green)
                                    {
                                        CurrentMoveOn.Gridinfo.AllGrids[YCurrent][XCurrent].GetComponent<SpriteRenderer>().color = OrignialColor;
                                    }
                                }
                            }
                        }
                        MouseFollowingUI.OriginalColor = OrignialColor;
                        CurrentCharacterBase.CharacterSChanger.SetSprite(-69, 0);
                        CurrentMoveOn.EstablishChargeIfExists((int)MouseFollowingUI.MousePositionGridChoose.x, (int)MouseFollowingUI.MousePositionGridChoose.y);
                        if(CurrentCharactersInPlay[CurrentCharacterInPlay].GetComponent<CharacterBase>().IsCharging)
                        {
                            StoredActions[CurrentCharacterInPlay] = "SPMoveCharge" + x;
                        }
                        else
                        {
                            StoredActions[CurrentCharacterInPlay] = "SPMove" + x;
                        }
                        StoredTargetCoordinates[CurrentCharacterInPlay] = MouseFollowingUI.MousePositionGridChoose;
                        CurrentCharacterInPlay += 1;
                        Scenes = "main";
                        MouseFollowingUI.WillGroupSelect = false;
                        MouseFollowingUI.IsSelecting = false;
                        HasEstablishedScene = false;
                        for(int z= 0; z < MouseFollowingUI.GroupSelectionOriginalColors.Length; z++)
                        {
                            for (int u = 0; u < MouseFollowingUI.GroupSelectionOriginalColors[0].Length; u++)
                            {
                                MouseFollowingUI.GroupSelectionOriginalColors[z][u] = OrignialColor;
                            }
                        }
                        MouseFollowingUI.WipeGroupSelect();
                        MouseFollowingUI.WipeAllowedSelected();
                    }
                    HasClicked = true;
                }
                else
                {
                    HasClicked = false;
                }
            }
        }
        if (Scenes == "Charging")
        {
            if (HasEstablishedScene == false)
            {
                HasEstablishedScene = true;
                if (Buttons[0] != null)
                {
                    Destroy(Buttons[0]);
                }
                if (Buttons[1] != null) 
                {
                    Destroy(Buttons[1]);
                }
                if (Buttons[2] != null)
                {
                    Destroy(Buttons[2]);
                }
                if (Buttons[3] != null)
                {
                    Destroy(Buttons[3]);
                }
                if (Buttons[4] != null)
                {
                    Destroy(Buttons[4]);
                }
                Buttons[2] = Instantiate(ContinueButton);
                Buttons[2].GetComponent<ButtonBase>().UIBase = gameObject;
                Buttons[2].transform.SetParent(gameObject.transform.parent);
                Buttons[2].transform.position = ContinueMoveCoordinate;
                Buttons[3] = Instantiate(ReturnToPreviousButton);
                Buttons[3].GetComponent<ButtonBase>().UIBase = gameObject;
                Buttons[3].transform.SetParent(gameObject.transform.parent);
                Buttons[3].transform.position = ReturnToPreviousCoordinate;
                switch (CurrentCharacterInPlay)
                {
                    case 0:
                        TextUI.GetComponent<TextMeshProUGUI>().text = "Sandman is charging";
                        break;
                    case 1:
                        TextUI.GetComponent<TextMeshProUGUI>().text = "Jade is charging";
                        break;
                    case 2:
                        TextUI.GetComponent<TextMeshProUGUI>().text = "Aurthur is charging";
                        break;
                }
            }
            
        }
        if (Scenes == "EnemyWait")
        {
            string waitDot;
            waitDot = "";
            EnemyWaitLoadDotCoolDownLeft -= Time.deltaTime;
            if(EnemyWaitLoadDotCoolDownLeft < 0)
            {
                AmountOfDots -= 1;
                EnemyWaitLoadDotCoolDownLeft = 1;
            }
            if( AmountOfDots < 1)
            {
                AmountOfDots = 3;
            }
            switch (AmountOfDots)
            {
                case 1:
                    waitDot = " .";
                    break;
                case 2:
                    waitDot = " ..";
                    break;
                case 3:
                    waitDot = " ...";
                    break;
            }
            TextUI.GetComponent<TextMeshProUGUI>().text = "Wait For Enemies To make their decison" + waitDot;

        }
    }
}
