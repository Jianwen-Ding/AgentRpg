using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertSpecialMoves : MonoBehaviour
{
    [SerializeField]
    GameObject[] MoveSprites = new GameObject[50];
    [SerializeField]
    GameObject[] MoveSpritesSecondary = new GameObject[50];
    [SerializeField]
    Vector2[] SpriteAdjust = new Vector2[50];
    [SerializeField]
    GameObject UiDamageCounter;
    GenericMove CurrentInsert;
    int index;
    public void InsertCharacterPassive(int index1,int index2 , int index3, int index4, GameObject GameObjectInsert)
    {
        for (int x = 0; x < 4; x++)
        {
            if(x == 0)
            {
                index = index1;
            }
            if(x == 1)
            {
                index = index2;
            }
            if(x == 2)
            {
                index = index3;
            }
            if(x == 3)
            {
                index = index4;
            }
            switch (index)
            {
                default:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(GenericMove));
                    break;
                case 1:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Cripple));
                    break;
                case 2:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Leap));
                    break;
                case 3:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Artillery));
                    break;
                case 4:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Staggerstep));
                    break;
                case 5:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Uppercut));
                    break;
                case 6:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Hookshot));
                    break;
                case 7:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Backblast));
                    break;
                case 8:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Rig));
                    break;
                case 9:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Shove));
                    break;
                case 10:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Guard));
                    break;
                case 11:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Bunker));
                    break;
                case 12:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Pumpup));
                    break;
                case 13:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Warcry));
                    break;
                case 14:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Misty));
                    break;
                case 15:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Firebomb));
                    break;
                case 16:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Chargestrike));
                    break;
                case 17:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Deathray));
                    break;
                case 18:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Acidrain));
                    break;
                case 19:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Demolish));
                    break;
                case 20:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Dartshot));
                    break;
                case 21:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Splashstep));
                    break;
                case 22:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Switcharoo));
                    break;
                case 23:
                    CurrentInsert = (GenericMove)GameObjectInsert.AddComponent(typeof(Healstation));
                    break;
            }
            CurrentInsert.MoveSprite = MoveSprites[index];
            CurrentInsert.MoveSpriteSecondary = MoveSpritesSecondary[index];
            CurrentInsert.AdjustSprite = SpriteAdjust[index];
            CurrentInsert.HitUiSprite = UiDamageCounter;
        }
    }
}
