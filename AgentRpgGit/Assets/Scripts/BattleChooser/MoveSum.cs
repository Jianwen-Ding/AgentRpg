using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class MoveSum : MonoBehaviour
{
    [SerializeField ]
    Vector3 OwnStartingPosition;
    public GameObject[] Enemies = new GameObject[3];
    [SerializeField]
    public GameObject[] enemyHeaders;
    [SerializeField ]
    Sprite[] SpriteEnemyDisplay = new Sprite[3];
    Color[] ColorEnemyDisplay = new Color[3];
    Transform[] TransformEnemyDisplay = new Transform[3];
    [SerializeField]
    GameObject EnemyDisplayObject;
    [SerializeField]
    GameObject EnemyPassiveDescriber;
    [SerializeField]
    string[] EnemyPassiveText = new string[3];
    [SerializeField]
    public int EnemyCurrentMain = 0;
    [SerializeField]
    GameObject[] SummaryDisplay;
    [SerializeField]
    GameObject Button1;
    [SerializeField]
    GameObject Button2;
    bool hasSet = false;
    // STORES ALL OF THE MOVE SUMMARIES
    public string MoveSummaryDispense(GenericMove MoveInput)
    {
        string SummaryFound;
        switch (MoveInput.GetType().Name)
        {
            default:
                SummaryFound = MoveInput.GetType().Name;
                break;
            case "GenericMove":
                SummaryFound = "Generic Move: Does nothing";
                break;
            case "Leap":
                SummaryFound = "Leap: Cripples movespeed for a long ranged omnidirectional jump";
                break;
            case "Vigor":
                SummaryFound = "Vigor: Heals the user for a large chunk of its health";
                break;
            case "Hostage":
                SummaryFound = "Hostage: Causes enemies in a area to gain the hostage status effect after a turn, this status effect reflects all damage taken by the user on to the affected enemy";
                break;
            case "Shockwave":
                SummaryFound = "Shockwave: Causes the grid under the user to become shocked, the shock spreads afflicting speed and defense debuffs to enemies that stand on shocked grids";
                break;
            case "Rage":
                SummaryFound = "Rage: Does a small amount of damage to all enemies around the user, it debuffs the speed and damage of the debuffed enemies and increases the speed and damage of the user";
                break;
            case "Artillery":
                SummaryFound = "Artillery: A long range move that deals a small amount of damage after a turn";
                break;
            case "Cripple":
                SummaryFound = "Cripple: Does a small amount of damage around the user and reduces the speed of enemies hit";
                break;
            case "DEADMAN":
                SummaryFound = "DEADMAN: kills an opponent instanly and teleports the user to a random spot";
                break;
            case "Staggerstep":
                SummaryFound = "Staggerstep: cleanses all speed debuffs and moves foward after a turn";
                break;
            case "Lariat":
                SummaryFound = "Lariat: moves toward and strikes at a opponent. Very powerful";
                break;
            case "Rythm":
                SummaryFound = "Rythm: moves and launches a attack around the user.";
                break;
            case "Escapist":
                SummaryFound = "Escapist: teleports user to diffrent location on low health";
                break;
            case "Melonlob":
                SummaryFound = "Melonlob: a long range move that does a bit of damage after a turn";
                break;
            case "SpiritOfOrange":
                SummaryFound = "SpiritOfOrange: sends a swarm of ghostly oranges that slowly spread out in all directions";
                break;
            case "BileJockey":
                SummaryFound = "BileJockey: A ranged move with splash that does minimal damage that inflicts a status effect that doubles in damage every single time it hits";
                break;
            case "SpineSting":
                SummaryFound = "SpineSting: A ranged move that does massive damage onto a single targe";
                break;
            case "SelfDestruct":
                SummaryFound = "SelfDestruct: A move that does massive damage to anything within its range and kills the user";
                break;
            case "LizardBreath":
                SummaryFound = "LizardBreath: pushes all opponents back 1 grid";
                break;
            case "LizardCry":
                SummaryFound = "LizardCry: heals user for each ally in range";
                break;
            case "LizardLaserEyes":
                SummaryFound = "LizardLaserEyes: Does large amount of damage to opponents close to user";
                break;
            case "LizardQuake":
                SummaryFound = "LizardQuake: Damages opponents and heals user for each opponent damaged, heals allys";
                break;
        }
        return SummaryFound;
    }
    void Start()
    {
        transform.position = OwnStartingPosition;
        EnemyDisplayObject.GetComponent<SpriteRenderer>().sprite = SpriteEnemyDisplay[EnemyCurrentMain];
        EnemyDisplayObject.GetComponent<SpriteRenderer>().color = ColorEnemyDisplay[EnemyCurrentMain];
        Button1.GetComponent<ButtonChange>().Main = gameObject.GetComponent<MoveSum>();
        Button2.GetComponent<ButtonChange>().Main = gameObject.GetComponent<MoveSum>();
        for (int i = 0; i < 3; i++)
        {
            SpriteEnemyDisplay[i] = Enemies[i].GetComponent<SpriteRenderer>().sprite;
            ColorEnemyDisplay[i] = Enemies[i].GetComponent<SpriteRenderer>().color;
            TransformEnemyDisplay[i] = Enemies[i].transform;
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSet == false && GameObject.FindGameObjectsWithTag("CharacterRemeberance").Length == 1)
        {
            hasSet = true;
            GameObject.FindGameObjectsWithTag("CharacterRemeberance")[0].gameObject.GetComponent<CharacterRememberance>().EnemyHeader = enemyHeaders;
        }
        if (EnemyCurrentMain < 0)
        {
            EnemyCurrentMain = Enemies.Length - 1;
        }
        if (EnemyCurrentMain >= Enemies.Length)
        {
            EnemyCurrentMain = 0;
        }
        for (int i = 0; i < Enemies[EnemyCurrentMain].GetComponent<CharacterBase>().MovesAllowed.Length; i++)
        {
            if (SummaryDisplay[i] != null && Enemies[EnemyCurrentMain].GetComponent<CharacterBase>().MovesAllowed[i] != null)
            {
                SummaryDisplay[i].GetComponent<TextMeshPro>().text = MoveSummaryDispense(Enemies[EnemyCurrentMain].GetComponent<CharacterBase>().MovesAllowed[i]);
            }
            else
            {
                SummaryDisplay[i].GetComponent<TextMeshPro>().text = "";
            }
        }
        EnemyPassiveDescriber.GetComponent<TextMeshPro>().text = EnemyPassiveText[EnemyCurrentMain];
        for (int i = 0; i < 3; i++)
        {
            EnemyDisplayObject.GetComponent<SpriteRenderer>().sprite = SpriteEnemyDisplay[EnemyCurrentMain];
            EnemyDisplayObject.GetComponent<SpriteRenderer>().color = ColorEnemyDisplay[EnemyCurrentMain];
            EnemyDisplayObject.transform.rotation = TransformEnemyDisplay[EnemyCurrentMain].rotation;
            EnemyDisplayObject.transform.localScale = TransformEnemyDisplay[EnemyCurrentMain].localScale;
            
        }
           
       
        
    }
}
