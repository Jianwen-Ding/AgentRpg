using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseCharacterStatusInsert : MonoBehaviour
{
    //StatusSprites In CharacterSpawner
    public GameObject[] StatusSprite = new GameObject[50];
    public Vector2[] StatusAdjust = new Vector2[50];
    [SerializeField]
    GameObject[] StatusSpriteCurrent;
    [SerializeField]
    int[] indexInsertStatus;
    [SerializeField]
    CharacterBase CharacterInfo;
    [SerializeField]
    int[] PreviousStatusInt;
    [SerializeField]
    BaseCharacterStatus[] StatusesInCharacter;
    public StatusDisplay DisplayedStatus;
    // Start is called before the first frame update
    void Start()
    {
        CharacterInfo = gameObject.GetComponent<CharacterBase>();
        StatusSpriteCurrent = new GameObject[CharacterInfo.StatusEffects.Length];
        indexInsertStatus = new int[CharacterInfo.StatusEffects.Length];
        PreviousStatusInt = new int[CharacterInfo.StatusEffects.Length];
        StatusesInCharacter = new BaseCharacterStatus[CharacterInfo.StatusEffects.Length];
        StatusSprite[0] = null;
    }
    public void WipeStatus(int index)
    {
        Destroy(StatusesInCharacter[index]);
        Destroy(StatusSpriteCurrent[index]);      
        
    }
    // Update is called once per frame
    void Update()
    {
        for(int x = 0; x < CharacterInfo.StatusEffects.Length; x++)
        {
            indexInsertStatus[x] = CharacterInfo.StatusEffects[x];
            if (StatusSpriteCurrent[x] != null)
            {
                StatusSpriteCurrent[x].transform.position = new Vector3(gameObject.transform.position.x + StatusAdjust[indexInsertStatus[x]].x, gameObject.transform.position.y + StatusAdjust[indexInsertStatus[x]].y);
            }
            if (PreviousStatusInt[x] != indexInsertStatus[x])
            {
                WipeStatus(x);
                DisplayedStatus.IdentidyBot[x] = -69;
                Destroy(DisplayedStatus.ModifierDisplayBot[x]);
                if (indexInsertStatus[x] != 0)
                {
                        DisplayedStatus.ModifierDisplayBot[x] = Instantiate(DisplayedStatus.ModifierDisplayPrefab);
                        //Text for statuses
                        switch (indexInsertStatus[x])
                        {
                            default:
                                break;
                            case 1:
                                DisplayedStatus.ModifierDisplayBot[x].GetComponent<TextMeshPro>().text = "GEN";
                                break;
                            case 2:
                                DisplayedStatus.ModifierDisplayBot[x].GetComponent<TextMeshPro>().text = "RIG";
                                break;
                            case 3:
                                DisplayedStatus.ModifierDisplayBot[x].GetComponent<TextMeshPro>().text = "WIT";
                                break;
                            case 4:
                                DisplayedStatus.ModifierDisplayBot[x].GetComponent<TextMeshPro>().text = "POI";
                                break;
                            case 5:
                                DisplayedStatus.ModifierDisplayBot[x].GetComponent<TextMeshPro>().text = "GEN";
                                break;

                        }
                    
                }
                //Character Status type indexing
                switch (indexInsertStatus[x])
                {
                    default:
                        break;
                    case 1:
                        StatusesInCharacter[x] = (BaseCharacterStatus)gameObject.AddComponent(typeof(BaseCharacterStatus));
                        
                        break;
                    case 2:
                        StatusesInCharacter[x] = (BaseCharacterStatus)gameObject.AddComponent(typeof(Rigged));
                        break;
                    case 3:
                        StatusesInCharacter[x] = (BaseCharacterStatus)gameObject.AddComponent(typeof(GuardWithdrawel));
                        break;
                    case 4:
                        StatusesInCharacter[x] = (BaseCharacterStatus)gameObject.AddComponent(typeof(DartshotPoision));
                        break;
                    case 5:
                        break;
                }
                if(StatusSprite[indexInsertStatus[x]] != null)
                {
                    
                    StatusesInCharacter[x].indexInCharacter = x;
                    StatusSpriteCurrent[x] = Instantiate(StatusSprite[indexInsertStatus[x]], new Vector3(gameObject.transform.position.x + StatusAdjust[indexInsertStatus[x]].x, gameObject.transform.position.y + StatusAdjust[indexInsertStatus[x]].y), Quaternion.identity.normalized);
                }
            }
            PreviousStatusInt[x] = CharacterInfo.StatusEffects[x];
        }
    }
}
