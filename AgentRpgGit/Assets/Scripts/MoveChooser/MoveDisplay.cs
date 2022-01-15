using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MoveDisplay : MonoBehaviour
{
    [SerializeField]
    GameObject MainTitleObject;
    [SerializeField]
    GameObject TypeTitleObject;
    [SerializeField]
    GameObject IsChargeObject;
    string MainText;
    //Types include
    //Damage
    //Misc
    //Support
    //Strategic
    //Movement
    //None
    [SerializeField]
    string TypeOfObject;
    [SerializeField]
    bool IsChargeMove;
    [SerializeField]
    Color noneColor;
    [SerializeField]
    Color miscColor;
    [SerializeField]
    Color damageColor;
    [SerializeField]
    Color supportColor;
    [SerializeField]
    Color strategicColor;
    [SerializeField]
    Color movementColor;
    [SerializeField]
    public bool IsLocked;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void InsertMoveIndex(int MoveIndex)
    {
        if (MoveIndex == -69)
        {
            MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Move not set";
            TypeOfObject = "None";
            IsChargeMove = false;
        }
        else
        {
            if (IsLocked)
            {
                MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Move Locked For Char";
                TypeOfObject = "None";
                IsChargeMove = false;
            }
            else
            {
                switch (MoveIndex)
                {
                    default:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Error- Move not found";
                        TypeOfObject = "None";
                        IsChargeMove = false;
                        break;
                    case 0:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Generic Move";
                        TypeOfObject = "Misc";
                        IsChargeMove = false;
                        break;
                    case 1:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Cripple";
                        TypeOfObject = "Damage";
                        IsChargeMove = false;
                        break;
                    case 2:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Leap";
                        TypeOfObject = "Movement";
                        IsChargeMove = false;
                        break;
                    case 3:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Artillery";
                        TypeOfObject = "Damage";
                        IsChargeMove = true;
                        break;
                    case 4:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Staggerstep";
                        TypeOfObject = "Misc";
                        IsChargeMove = true;
                        break;
                    case 5:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Uppercut";
                        TypeOfObject = "Damage";
                        IsChargeMove = false;
                        break;
                    case 6:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Hookshot";
                        TypeOfObject = "Strategic";
                        IsChargeMove = true;
                        break;
                    case 7:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Backblast";
                        TypeOfObject = "Movement";
                        IsChargeMove = false;
                        break;
                    case 8:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Rig";
                        TypeOfObject = "Damage";
                        IsChargeMove = false;
                        break;
                    case 9:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Shove";
                        TypeOfObject = "Strategic";
                        IsChargeMove = false;
                        break;
                    case 10:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Guard";
                        TypeOfObject = "Movement";
                        IsChargeMove = false;
                        break;
                    case 11:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Bunker";
                        TypeOfObject = "Misc";
                        IsChargeMove = false;
                        break;
                    case 12:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Pumpup";
                        TypeOfObject = "Misc";
                        IsChargeMove = false;
                        break;
                    case 13:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Warcry";
                        TypeOfObject = "Misc";
                        IsChargeMove = false;
                        break;
                    case 14:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Misty";
                        TypeOfObject = "Strategic";
                        IsChargeMove = false;
                        break;
                    case 15:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Firebomb";
                        TypeOfObject = "Damage";
                        IsChargeMove = false;
                        break;
                    case 16:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Chargestrike";
                        TypeOfObject = "Damage";
                        IsChargeMove = true;
                        break;
                    case 17:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Deathray";
                        TypeOfObject = "Damage";
                        IsChargeMove = true;
                        break;
                    case 18:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Acidrain";
                        TypeOfObject = "Strategic";
                        IsChargeMove = false;
                        break;
                    case 19:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Demolish";
                        TypeOfObject = "Strategic";
                        IsChargeMove = false;
                        break;
                    case 20:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Dartshot";
                        TypeOfObject = "Damage";
                        IsChargeMove = false;
                        break;
                    case 21:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Splashstep";
                        TypeOfObject = "Damage";
                        IsChargeMove = false;
                        break;
                    case 22:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Switcharoo";
                        TypeOfObject = "Movement";
                        IsChargeMove = false;
                        break;
                    case 23:
                        MainTitleObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Healstation";
                        TypeOfObject = "Support";
                        IsChargeMove = true;
                        break;
                }
            }
            
            TypeTitleObject.GetComponent<TextMeshProUGUI>().text = TypeOfObject;
            if (gameObject.GetComponent<SpriteRenderer>() == null && gameObject.GetComponent<Button>() != null)
            {
                switch (TypeOfObject)
                {
                    case "None":
                        gameObject.GetComponent<Image>().color = noneColor;
                        break;
                    case "Misc":
                        gameObject.GetComponent<Image>().color = miscColor;
                        break;
                    case "Damage":
                        gameObject.GetComponent<Image>().color = damageColor;
                        break;
                    case "Movement":
                        gameObject.GetComponent<Image>().color = movementColor;
                        break;
                    case "Support":
                        gameObject.GetComponent<Image>().color = supportColor;
                        break;
                    case "Strategic":
                        gameObject.GetComponent<Image>().color = strategicColor;
                        break;

                }
            }
            else
            {
                switch (TypeOfObject)
                {
                    case "None":

                        gameObject.GetComponent<SpriteRenderer>().color = noneColor;
                        break;
                    case "Misc":
                        gameObject.GetComponent<SpriteRenderer>().color = miscColor;
                        break;
                    case "Damage":
                        gameObject.GetComponent<SpriteRenderer>().color = damageColor;
                        break;
                    case "Movement":
                        gameObject.GetComponent<SpriteRenderer>().color = movementColor;
                        break;
                    case "Support":
                        gameObject.GetComponent<SpriteRenderer>().color = supportColor;
                        break;
                    case "Strategic":
                        gameObject.GetComponent<SpriteRenderer>().color = strategicColor;
                        break;

                }
            }
            if (IsChargeMove == false)
            {
                IsChargeObject.GetComponent<SpriteRenderer>().color = Color.clear;
            }
        }
        //Refrence Special Move Insert
        
    }
}
