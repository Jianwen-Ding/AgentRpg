using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseInsert : MonoBehaviour
{
    public void InsertCharacterPassive(int index, GameObject GameObjectInsert)
    {
        switch (index) 
        {
            default:
                GameObjectInsert.AddComponent(typeof(CharacterBase));
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }

    }
}
