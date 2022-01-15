using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EventSystem : MonoBehaviour
{
    //Change Scene
    public bool HasClicked;
    [SerializeField]
    public float TimeUntilSceneChangeMin;
    [SerializeField]
    public float TimeUntilSceneChangeLeft;
    //Event System Activation
    public bool active;
    [SerializeField]
    public int FontSizeNormal;
    [SerializeField]
    public MoveSystem BaseMoveSystem;
    //MostLikelyUsed agaisnt characters, gameObjects meant to denote what string is to what character change
    [SerializeField]
    public GameObject[] GameObjectsInQue = new GameObject[20];
    //Id List 
    //0 Id = Status Effect
    //1 Id = Character Move
    //2 Id = Flavor Text
    //3 Id = Urgent Text/ Special Text
    [SerializeField]
    public int[] IntIdsInQue = new int[20];
    [SerializeField]
    public string[] StringsInQue = new string[20];
    //Priority list
    //0-5 Priority = Character Action
    //6 Priority = Flavor Text
    //7 Priority = Statseffect
    //8 Priority = Special Attribute
    //9 Priority = Urgent Text/ Special text
    //10 Priorty = Quick Moves
    //Special Text is for stuff like Character Death
    [SerializeField]
    public int[] PriorityInQue = new int[20];
    [SerializeField]
    public int CurrentQue;
    [SerializeField]
    public int QueFilledIn;
    [SerializeField]
    public float SecondsTillSkipAllowed;
    [SerializeField]
    public float SecondsLeft;
    [SerializeField]
    public TextMeshProUGUI TextUI;
    // Start is called before the first frame update
    void Start()
    {
        TextUI = gameObject.GetComponent<TextMeshProUGUI>();
        CurrentQue = 0;
        QueFilledIn = 0;
    }
    private void EventSort()
    {
        for (int x = 0; x < PriorityInQue.Length; x++)
        {
            for (int y = 0; y < PriorityInQue.Length; y++)
            {
                if (y >= 1 && PriorityInQue[y] > PriorityInQue[y - 1] && y - 1 >= 0 && (CurrentQue < y - 1 || active == false))
                {
                    //the switch
                    int temp = PriorityInQue[y - 1];
                    PriorityInQue[y - 1] = PriorityInQue[y];
                    PriorityInQue[y] = temp;
                    string tempString = StringsInQue[y - 1];
                    StringsInQue[y - 1] = StringsInQue[y];
                    StringsInQue[y] = tempString;
                    GameObject tempGameObject = GameObjectsInQue[y - 1];
                    GameObjectsInQue[y - 1] = GameObjectsInQue[y];
                    GameObjectsInQue[y] = tempGameObject;
                    int tempId = IntIdsInQue[y - 1];
                    IntIdsInQue[y - 1] = IntIdsInQue[y];
                    IntIdsInQue[y] = tempId;
                }
            }
        }
    }
    public void QueEvent(GameObject gameObjectInQue,int ID, string TextDisplayed, int Priority)
    {
       
        if(QueFilledIn < 20)
        {
            GameObjectsInQue[QueFilledIn] = gameObjectInQue;
            StringsInQue[QueFilledIn] = TextDisplayed;
            IntIdsInQue[QueFilledIn] = ID;
            PriorityInQue[QueFilledIn] = Priority;
            QueFilledIn++;
            EventSort();
        }
    }
    public bool CheckQue(GameObject gameObjectInQue, int IDConfirmation)
    {
        if(active == true && GameObjectsInQue[CurrentQue] == gameObjectInQue && IDConfirmation == IntIdsInQue[CurrentQue])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void DestroyQue(GameObject gameObjectFind, int IDFind)
    {
        for (int i = 0; i < IntIdsInQue.Length; i++)
        {
            if (GameObjectsInQue[i] == gameObjectFind && IntIdsInQue[i] == IDFind)
            {
                Destroy(GameObjectsInQue[i]);
                PriorityInQue[i] = 0;
                StringsInQue[i] = null;

                QueFilledIn--;
            }
        }
        EventSort();
    }
    public string GetCurrentString(int CurrentIndex)
    {
        return StringsInQue[CurrentIndex];
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Click") != 0)
        {
            if(HasClicked == false && TimeUntilSceneChangeLeft > TimeUntilSceneChangeMin && active == true)
            {
                CurrentQue++; 
            }
            HasClicked = true;
        }
        else
        {
            HasClicked = false;
        }
        if (active == true) 
        {
            TimeUntilSceneChangeLeft += Time.deltaTime;
            TextUI.fontSize = FontSizeNormal;
            TextUI.text = StringsInQue[CurrentQue];
            if (CurrentQue >= QueFilledIn)
            {
                CurrentQue = 0;
                QueFilledIn = 0;
                for (int x = 0; x < StringsInQue.Length; x++)
                {
                    StringsInQue[x] = null;
                    GameObjectsInQue[x] = null;
                    IntIdsInQue[x] = 0;
                    PriorityInQue[x] = 0;
                }
                active = false;
                
            }
        }
        else
        {
            if(TextUI != null)
            {
                TextUI.fontSize = 0;
                CurrentQue = 0;
            }
            
        }
    }
}
