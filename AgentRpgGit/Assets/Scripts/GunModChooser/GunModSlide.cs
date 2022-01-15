using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GunModSlide : MonoBehaviour
{
    [SerializeField]
    CharacterRememberance CharacterRemembered;
    [SerializeField]
    public int CurrentSlide;
    [SerializeField]
    GameObject DescriptionHeader;
    [SerializeField]
    GameObject DamagePercentageFill;
    [SerializeField]
    GameObject DistanceFill;
    [SerializeField]
    GameObject BulletsAddFill;
    [SerializeField]
    GameObject DistanceFallOffFill;
    [SerializeField]
    GameObject CanPierceObstaclesTrue;
    [SerializeField]
    GameObject CanPierceCharactersTrue;
    [SerializeField]
    GameObject CurrentGunMod;
    // Start is called before the first frame update
    void Start()
    {
        CharacterRemembered = GameObject.FindGameObjectWithTag("CharacterRemeberance").GetComponent<CharacterRememberance>();
        CurrentSlide = PlayerPrefs.GetInt("IndexGunModRemember", 0);
    }
    // Update is called once per frame
    void Update()
    {
        CurrentSlide = gameObject.GetComponent<GunModSliderMove>().CurrentY;
        CurrentGunMod.GetComponent<TextMeshPro>().text = "Current Gun Mod: " + GameObject.FindGameObjectWithTag("CharacterRemeberance").GetComponent<CharacterRememberance>().GunName[GameObject.FindGameObjectWithTag("CharacterRemeberance").GetComponent<CharacterRememberance>().GunFunctionIndex3[PlayerPrefs.GetInt("CharacterIndexGunModRemember", 0)]];
        DescriptionHeader.GetComponent<TextMeshPro>().text = CharacterRemembered.GunName[CurrentSlide];
        DamagePercentageFill.GetComponent<TextMeshPro>().text = "Character Damage Percentage Expressed: "+( 100*CharacterRemembered.GunClassDamagePercentage[CurrentSlide] )+ "%";
        DistanceFill.GetComponent<TextMeshPro>().text = "Gun Range: " + CharacterRemembered.GunRange[CurrentSlide] + " tiles";
        BulletsAddFill.GetComponent<TextMeshPro>().text = "Bullets:" + CharacterRemembered.BulletsAdded[CurrentSlide] +" bullets";
        DistanceFallOffFill.GetComponent<TextMeshPro>().text = "Damage Falloff Per Tile:" + (100 * CharacterRemembered.DamageFallOff[CurrentSlide]) + "%";
        if (!CharacterRemembered.CanPierceCharacter[CurrentSlide])
        {
            CanPierceCharactersTrue.GetComponent<SpriteRenderer>().color = Color.clear;
        }
        else
        {
            CanPierceCharactersTrue.GetComponent<SpriteRenderer>().color = Color.black;
        }
        if (!CharacterRemembered.CanPierceObstacle[CurrentSlide])
        {
            CanPierceObstaclesTrue.GetComponent<SpriteRenderer>().color = Color.clear;
        }
        else
        {
            CanPierceObstaclesTrue.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }
}
