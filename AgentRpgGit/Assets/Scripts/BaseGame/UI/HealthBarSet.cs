using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarSet : MonoBehaviour
{
    //This displays stat Modifyers and status effects
    //Used find Character
    [SerializeField]
    int CharacterIndex;
    //Used to get CharacterList
    [SerializeField]
    GameObject GetCharacter;
    [SerializeField]
    LineRenderer HealthBarRender;
    [SerializeField]
    CharacterBase CharacterBaseGet;
    float OriginalFarX;
    // Start is called before the first frame update
    void Start()
    {
        HealthBarRender = gameObject.GetComponent<LineRenderer>();
        OriginalFarX = HealthBarRender.GetPosition(1).x;
        
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
        if(CharacterBaseGet != null)
        {
            HealthBarRender.SetPosition(1,new Vector3(((OriginalFarX- HealthBarRender.GetPosition(0).x) * (CharacterBaseGet.Health/ CharacterBaseGet.MaxHealth)) + HealthBarRender.GetPosition(0).x, HealthBarRender.GetPosition(1).y,0));
        }
    }
}
