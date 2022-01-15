using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusDisplay : MonoBehaviour
{
    //This displays stat Modifyers and status effects
    //Used find Character
    [SerializeField]
    int CharacterIndex;
    //Used to get CharacterList
    [SerializeField]
    GameObject GetCharacter;
    public GameObject ModifierDisplayPrefab;
    //Displays modifiers in order 
    public GameObject[] ModifierDisplayTop = new GameObject[10];
    public string[] IdentidyTop = new string[10];
    [SerializeField]
    float YStartTop;
    [SerializeField]
    float XStartTop;
    [SerializeField]
    float XChangeTop;
    public bool SpeedDisplay;
    public bool DefenseDisplay;
    public bool DamageDisplay;
    public GameObject[] ModifierDisplayBot = new GameObject[10];
    public int[] IdentidyBot = new int[10];
    [SerializeField]
    float YStartBot;
    [SerializeField]
    float XStartBot;
    [SerializeField]
    float XChangeBot;
    [SerializeField]
    BaseCharacterStatusInsert CharacterStatusGet;
    [SerializeField]
    CharacterBase CharacterBaseGet;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;  i < IdentidyBot.Length; i++)
        {
            IdentidyBot[i] = -69;
        }
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if (GetCharacter == null && Camera.main.gameObject.GetComponent<MoveSystem>().CharacterOnField[CharacterIndex].GetComponent<CharacterBase>() != null)
        {
            GetCharacter = Camera.main.gameObject.GetComponent<MoveSystem>().CharacterOnField[CharacterIndex];
        }
        if (CharacterBaseGet == null && GetCharacter.GetComponent<CharacterBase>() != null)
        {
            CharacterBaseGet = GetCharacter.GetComponent<CharacterBase>();
        }
        if (CharacterStatusGet == null && GetCharacter.GetComponent<BaseCharacterStatusInsert>() != null)
        {
            CharacterStatusGet = GetCharacter.GetComponent<BaseCharacterStatusInsert>();
            CharacterStatusGet.DisplayedStatus = gameObject.GetComponent<StatusDisplay>();
        }
        if(CharacterBaseGet != null && CharacterStatusGet != null)
        {
            for (int i = 0; i < ModifierDisplayTop.Length - 1; i++)
            {
                switch (IdentidyTop[i])
                {
                    default:
                        break;
                    case "Speed":
                        ModifierDisplayTop[i].GetComponent<TextMeshPro>().text = "SPD %" + (int)(CharacterBaseGet.SpeedMultiplier * 100);
                        if ((int)(CharacterBaseGet.SpeedMultiplier * 100) == 100)
                        {
                            Destroy(ModifierDisplayTop[i]);
                            IdentidyTop[i] = null;
                            SpeedDisplay = false;
                        }
                        break;
                    case "Defense":
                        ModifierDisplayTop[i].GetComponent<TextMeshPro>().text = "DEF %" + (int)(CharacterBaseGet.DefenseMultiplier * 100);
                        if ((int)(CharacterBaseGet.DefenseMultiplier * 100) == 100)
                        {
                            Destroy(ModifierDisplayTop[i]);
                            IdentidyTop[i] = null;
                            DefenseDisplay = false;
                        }
                        break;
                    case "Damage":
                        ModifierDisplayTop[i].GetComponent<TextMeshPro>().text = "ATK %" + (int)(CharacterBaseGet.DamageMultiplier * 100);
                        if ((int)(CharacterBaseGet.DamageMultiplier * 100) == 100)
                        {
                            Destroy(ModifierDisplayTop[i]);
                            IdentidyTop[i] = null;
                            DamageDisplay = false;
                        }
                        break;
                }
                if (ModifierDisplayTop[i] == null && ModifierDisplayTop[i + 1] != null)
                {
                    ModifierDisplayTop[i] = ModifierDisplayTop[i + 1];
                    IdentidyTop[i] = IdentidyTop[i + 1];
                    ModifierDisplayTop[i + 1] = null;
                    IdentidyTop[i + 1] = null;

                }
                if (ModifierDisplayTop[i] == null)
                {
                    if (SpeedDisplay == false && (int)(CharacterBaseGet.SpeedMultiplier * 100) != 100)
                    {
                        ModifierDisplayTop[i] = Instantiate(ModifierDisplayPrefab, new Vector3(-69, 69, 69), Quaternion.identity.normalized);
                        IdentidyTop[i] = "Speed";
                        SpeedDisplay = true;
                    }
                    else if (DefenseDisplay == false && (int)(CharacterBaseGet.DefenseMultiplier * 100) != 100)
                    {
                        ModifierDisplayTop[i] = Instantiate(ModifierDisplayPrefab, new Vector3(-69, 69, 69), Quaternion.identity.normalized);
                        IdentidyTop[i] = "Defense";
                        DefenseDisplay = true;
                    }
                    else if (DamageDisplay == false && (int)(CharacterBaseGet.DamageMultiplier * 100) != 100)
                    {
                        ModifierDisplayTop[i] = Instantiate(ModifierDisplayPrefab, new Vector3(-69, 69, 69), Quaternion.identity.normalized);
                        IdentidyTop[i] = "Damage";
                        DamageDisplay = true;
                    }
                }
                if (ModifierDisplayTop[i] != null)
                {

                    ModifierDisplayTop[i].transform.position = new Vector3(XStartTop + XChangeTop * i, YStartTop, 0);
                }

            }
            for (int i = 0; i < ModifierDisplayBot.Length - 1; i++)
            {
                if (ModifierDisplayBot[i] == null && ModifierDisplayBot[i + 1] != null)
                {
                    ModifierDisplayBot[i] = ModifierDisplayBot[i + 1];
                    IdentidyBot[i] = IdentidyBot[i + 1];
                    ModifierDisplayBot[i + 1] = null;
                    IdentidyBot[i + 1] = -69;

                }
                if (ModifierDisplayBot[i] != null)
                {
                    ModifierDisplayBot[i].transform.position = new Vector2(XStartBot + XChangeBot * i, YStartBot);
                }

            }
        }
       
    }
}
