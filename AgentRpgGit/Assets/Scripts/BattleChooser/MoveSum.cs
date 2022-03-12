using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class MoveSum : MonoBehaviour
{
    [SerializeField ]
    Vector3 OwnStartingPosition;
    public GameObject[] Enemies = new GameObject[3];
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
            case "DEADMAN":
                SummaryFound = "DEADMAN: kills an opponent instanly and teleports the user to a random spot";
                break;
        }
        return SummaryFound;
    }
    void Start()
    {
        transform.position = OwnStartingPosition;
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
            else if (SummaryDisplay[i] != null)
            {
                Destroy(SummaryDisplay[i]);
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
