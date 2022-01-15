using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MoveSystem : MonoBehaviour
{
    [SerializeField]
    string SceneGoTo;
    [SerializeField]
    public bool IsDisplayingHappening;
    [SerializeField]
    int OnFieldCharactersAmount;
    //0 = Sandman
    //1 = Jade
    //2 = Aurthur
    //3 - 5 = enemies
    public GameObject[] CharacterOnField = new GameObject[6];
    public CharacterBase[] CharacterBasesOnField = new CharacterBase[6];
    bool CompletedCharacterBasesOnField = false;
    //From most speed to least speed, move order
    [SerializeField]
    GameObject[] CharacterOnFieldInOrder = new GameObject[6];
    public EventSystem TextBoxLoader;
    [SerializeField]
    public GameObject EventDisplayer;
    [SerializeField]
    GameObject MoveUI;
    [SerializeField]
    float TimeTillLoadNewSceneTime;
    [SerializeField]
    float TimeTillLoadNewSceneTimeLeft;
    [SerializeField]
    public bool GameHasEnded;
    [SerializeField]
    bool HaveWon;
    [SerializeField]
    GameObject GameOverScreenBlackout;
    [SerializeField]
    GameObject GameOverScreenWhiteout;
    [SerializeField]
    GameObject GameOverScreen;
    [SerializeField]
    bool HasLoadedScreen;
    [SerializeField]
    AudioClip BattleTheme;
    [SerializeField]
    float timeToStartLoopBat;
    [SerializeField]
    AudioClip EndTheme;
    [SerializeField]
    float timeToStartLoopEnd;
    // Start is called before the first frame update
    void Start()
    {
        MenuMusic.DestroySelf();
        EventSystemEnd();
        IsDisplayingHappening = false;
        EventDisplayer = GameObject.Find("EventDisplayer");
        TextBoxLoader = EventDisplayer.GetComponent<EventSystem>();
        MoveUI = GameObject.Find("MovementUI");
        MoveUI.gameObject.GetComponent<MovementUI>().CurrentCharactersInPlay[0] = CharacterOnField[0];
        MoveUI.gameObject.GetComponent<MovementUI>().CurrentCharactersInPlay[1] = CharacterOnField[1];
        MoveUI.gameObject.GetComponent<MovementUI>().CurrentCharactersInPlay[2] = CharacterOnField[2];
        MoveUI.GetComponent<MovementUI>().StartUIScene();
    }
    //Usually start at the end of the Event System Display, where the event happens and the text is displayed
    public void EventSystemEnd()
    {
        CharacterBase[] CharacterBases = new CharacterBase[6];
        for(int x = 0; x < CharacterOnField.Length; x++)
        {
            if (CharacterOnField[x] != null)
            {
                CharacterBases[x] = CharacterOnField[x].gameObject.GetComponent<CharacterBase>();
            }
            else
            {
                CharacterBases[x] = null;
            }
        }
        for(int x = 0; x < CharacterBases.Length; x++)
        {
            for(int y = 0; y<CharacterBases.Length; y++)
            {
                if(y+1 != CharacterBases.Length)
                {
                    if(CharacterBases[y] != null && CharacterBases[y + 1] != null && CharacterBases[y].ExpressedSpeed > CharacterBases[y + 1].ExpressedSpeed || CharacterBases[y] != null && CharacterBases[y + 1] == null)
                    {
                        CharacterBase temp = CharacterBases[y + 1];
                        CharacterBases[y + 1] = CharacterBases[y];
                        CharacterBases[y] = temp;
                    } 
                }
            }
        }
        for (int x = 0; x < CharacterBases.Length; x++)
        {
            //FIX THIS
            if(CharacterBases[x] != null)
            {
                CharacterOnFieldInOrder[x] = CharacterBases[x].gameObject;
                CharacterBases[x].SpeedPriority = x;
            }
            else
            {
                CharacterOnFieldInOrder[x] = null;
            }
        }
        MoveUI.GetComponent<MovementUI>().StartUIScene();
    }
    // Update is called once per frame
    void Update()
    {
        if (!CompletedCharacterBasesOnField)
        {
            for (int i = 0; i < CharacterOnField.Length; i++)
            {
                CharacterBasesOnField[i] = CharacterOnField[i].GetComponent<CharacterBase>();
                CompletedCharacterBasesOnField = true;
                for (int z = 0; z < CharacterBasesOnField.Length; z++)
                {
                    if (CharacterBasesOnField[i] == null)
                    {
                        CompletedCharacterBasesOnField = false;
                    }
                }
            }
        }
        if (EventDisplayer.GetComponent<EventSystem>().active == false && IsDisplayingHappening == true)
        {
            IsDisplayingHappening = false;
            EventSystemEnd();

        }
        if(EventDisplayer.GetComponent<EventSystem>().active == true && IsDisplayingHappening == false)
        {
            IsDisplayingHappening = true;
        }
        //Game
        if (CompletedCharacterBasesOnField == true && CharacterBasesOnField[0].IsDead == true && CharacterBasesOnField[1].IsDead == true && CharacterBasesOnField[2].IsDead == true && TimeTillLoadNewSceneTimeLeft == 0)
        {
            GameHasEnded = true;
            HaveWon = false;
            TimeTillLoadNewSceneTimeLeft = TimeTillLoadNewSceneTime;
            Instantiate(GameOverScreenBlackout);
        }
        if (CompletedCharacterBasesOnField == true && CharacterBasesOnField[3].IsDead == true && CharacterBasesOnField[4].IsDead == true && CharacterBasesOnField[5].IsDead == true && TimeTillLoadNewSceneTimeLeft == 0)
        {
            PlayerPrefs.SetInt("HasFinished" + PlayerPrefs.GetInt("CurrentWatching", 1),1);
            Instantiate(GameOverScreenWhiteout);
            GameHasEnded = true;
            TimeTillLoadNewSceneTimeLeft = TimeTillLoadNewSceneTime;
            HaveWon = true;
        }
        if (GameHasEnded)
        {
            TimeTillLoadNewSceneTimeLeft -= Time.deltaTime;
        }
        if(TimeTillLoadNewSceneTimeLeft < 0 && HaveWon == false && HasLoadedScreen == false)
        {
            HasLoadedScreen = true;
            Instantiate(GameOverScreen);
        }
        if(TimeTillLoadNewSceneTimeLeft < 0 && HaveWon)
        {
            SceneManager.LoadScene(SceneGoTo);
        }
    }
}
