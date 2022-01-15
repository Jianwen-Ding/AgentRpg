using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject SandmanPrefab;

    [SerializeField]
    GameObject JadePrefab;

    [SerializeField]
    GameObject AurthurPrefab;

    [SerializeField]
    GameObject[] MoveResidueEffect;

    public GameObject Enemy1;

    public GameObject Enemy2;

    public GameObject Enemy3;

    [SerializeField]
    GameObject floatingTextPrefab;

    [SerializeField]
    CharacterBaseInsert CharacterPassiveInsert;

    [SerializeField]
    InsertSpecialMoves CharacterMoveInsert;

    [SerializeField]
    GameObject[] StatusSprite = new GameObject[50];
    [SerializeField]
    Vector2[] StatusAdjust = new Vector2[50];
    // Start is called before the first frame update
    void Start()
    {
        CharacterRememberance CharacterSave;
        GridLoad GridData;
        GridData = Camera.main.gameObject.GetComponent<GridLoad>();
        CharacterSave = GameObject.FindWithTag("CharacterRemeberance").GetComponent<CharacterRememberance>();

        //Sandman
        SandmanPrefab = Instantiate(SandmanPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        CharacterPassiveInsert.InsertCharacterPassive(CharacterSave.PassiveIndex[CharacterSave.GunFunctionIndex3[0]], SandmanPrefab);
        CharacterBase SandmanCharacterBase;
       
        SandmanCharacterBase = SandmanPrefab.gameObject.GetComponent<CharacterBase>();
        SandmanCharacterBase.MoveLeftOver = MoveResidueEffect[0];
        SandmanCharacterBase.DamageShakeRatio = (float)0.003;
        SandmanCharacterBase.Health = CharacterSave.Health[0];
        SandmanCharacterBase.MaxHealth = CharacterSave.MaxHealth[0];
        SandmanCharacterBase.Damage = CharacterSave.Damage[0];
        SandmanCharacterBase.Defense = CharacterSave.Defense[0];
        SandmanCharacterBase.Speed = CharacterSave.Speed[0];
        SandmanCharacterBase.ExpressedSpeed = CharacterSave.Speed[0];
        CharacterMoveInsert.InsertCharacterPassive(CharacterSave.MovesPutIn1[0], CharacterSave.MovesPutIn1[1], CharacterSave.MovesPutIn1[2], CharacterSave.MovesPutIn1[3], SandmanPrefab);
        SandmanCharacterBase.DistanceUp = (float)0.8;
        SandmanCharacterBase.CharacterLocationIndex = new Vector2(0, 0);
        SandmanCharacterBase.gameObject.AddComponent(typeof(GunFunction));
        SandmanCharacterBase.gameObject.AddComponent(typeof(ShakeObject));
        SandmanCharacterBase.GetComponent<GunFunction>().Text = floatingTextPrefab;
        SandmanCharacterBase.TimeUntilShoot = (float)0.5;
        SandmanCharacterBase.TimeUseMove = (float)0.5;
        SandmanCharacterBase.GetComponent<GunFunction>().EstablishGun(CharacterSave.GunRange[CharacterSave.GunFunctionIndex3[0]], CharacterSave.CanPierceObstacle[CharacterSave.GunFunctionIndex3[0]], CharacterSave.CanPierceCharacter[CharacterSave.GunFunctionIndex3[0]], CharacterSave.GunClassDamagePercentage[CharacterSave.GunFunctionIndex3[0]], CharacterSave.DamageFallOff[CharacterSave.GunFunctionIndex3[0]], CharacterSave.BulletsAdded[CharacterSave.GunFunctionIndex3[0]], CharacterSave.GunEffectTrial[CharacterSave.GunFunctionIndex3[0]], CharacterSave.GunEffectTrialAdjust[CharacterSave.GunFunctionIndex3[0]], CharacterSave.GunEffectHit[CharacterSave.GunFunctionIndex3[0]],CharacterSave.GunEffectHitAdjust[CharacterSave.GunFunctionIndex3[0]]);
        //Jade
        JadePrefab = Instantiate(JadePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        CharacterPassiveInsert.InsertCharacterPassive(CharacterSave.PassiveIndex[CharacterSave.GunFunctionIndex3[0]], JadePrefab);
        CharacterBase JadeCharacterBase;
        JadeCharacterBase = JadePrefab.gameObject.GetComponent<CharacterBase>();
        JadeCharacterBase.MoveLeftOver = MoveResidueEffect[1];
        JadeCharacterBase.DamageShakeRatio = (float)0.004;
        JadeCharacterBase.Health = CharacterSave.Health[1];
        JadeCharacterBase.MaxHealth = CharacterSave.MaxHealth[1];
        JadeCharacterBase.Damage = CharacterSave.Damage[1];
        JadeCharacterBase.Defense = CharacterSave.Defense[1];
        JadeCharacterBase.Speed = CharacterSave.Speed[1];
        JadeCharacterBase.ExpressedSpeed = CharacterSave.Speed[1];
        CharacterMoveInsert.InsertCharacterPassive(CharacterSave.MovesPutIn2[0], CharacterSave.MovesPutIn2[1], CharacterSave.MovesPutIn2[2], CharacterSave.MovesPutIn2[3], JadePrefab);
        JadeCharacterBase.DistanceUp = (float)0.8;
        JadeCharacterBase.CharacterLocationIndex = new Vector2(0, Mathf.RoundToInt((GridData.YWidthPublic-1) / 2));
        JadeCharacterBase.gameObject.AddComponent(typeof (GunFunction));
        JadeCharacterBase.gameObject.AddComponent(typeof(ShakeObject));
        JadeCharacterBase.GetComponent<GunFunction>().Text = floatingTextPrefab;
        JadeCharacterBase.TimeUntilShoot = (float)0.5;
        JadeCharacterBase.TimeUseMove = (float)0.5;
        JadeCharacterBase.GetComponent<GunFunction>().EstablishGun(CharacterSave.GunRange[CharacterSave.GunFunctionIndex3[1]], CharacterSave.CanPierceObstacle[CharacterSave.GunFunctionIndex3[1]], CharacterSave.CanPierceCharacter[CharacterSave.GunFunctionIndex3[1]], CharacterSave.GunClassDamagePercentage[CharacterSave.GunFunctionIndex3[1]], CharacterSave.DamageFallOff[CharacterSave.GunFunctionIndex3[1]], CharacterSave.BulletsAdded[CharacterSave.GunFunctionIndex3[1]], CharacterSave.GunEffectTrial[CharacterSave.GunFunctionIndex3[1]], CharacterSave.GunEffectTrialAdjust[CharacterSave.GunFunctionIndex3[1]], CharacterSave.GunEffectHit[CharacterSave.GunFunctionIndex3[1]], CharacterSave.GunEffectHitAdjust[CharacterSave.GunFunctionIndex3[1]]);
        //Aurthur
        AurthurPrefab = Instantiate(AurthurPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        CharacterPassiveInsert.InsertCharacterPassive(CharacterSave.PassiveIndex[CharacterSave.GunFunctionIndex3[0]], AurthurPrefab);
        CharacterBase AurthurCharacterBase;
        AurthurCharacterBase = AurthurPrefab.gameObject.GetComponent<CharacterBase>();
        AurthurCharacterBase.DamageShakeRatio = (float)0.002;
        AurthurCharacterBase.MoveLeftOver = MoveResidueEffect[2];
        AurthurCharacterBase.Health = CharacterSave.Health[2];
        AurthurCharacterBase.MaxHealth = CharacterSave.MaxHealth[2];
        AurthurCharacterBase.Damage = CharacterSave.Damage[2];
        AurthurCharacterBase.Defense = CharacterSave.Defense[2];
        AurthurCharacterBase.Speed = CharacterSave.Speed[2];
        AurthurCharacterBase.ExpressedSpeed = CharacterSave.Speed[2];
        CharacterMoveInsert.InsertCharacterPassive(CharacterSave.MovesPutIn3[0], CharacterSave.MovesPutIn3[1], CharacterSave.MovesPutIn3[2], CharacterSave.MovesPutIn3[3], AurthurPrefab);
        AurthurCharacterBase.DistanceUp = (float)0.8;
        AurthurCharacterBase.CharacterLocationIndex = new Vector2(0, Mathf.RoundToInt(GridData.YWidthPublic - 1));
        AurthurCharacterBase.gameObject.AddComponent(typeof(GunFunction));
        AurthurCharacterBase.gameObject.AddComponent(typeof(ShakeObject));
        AurthurCharacterBase.GetComponent<GunFunction>().Text = floatingTextPrefab;
        AurthurCharacterBase.TimeUntilShoot = (float)0.5;
        AurthurCharacterBase.TimeUseMove = (float)0.5;
        AurthurCharacterBase.GetComponent<GunFunction>().EstablishGun(CharacterSave.GunRange[CharacterSave.GunFunctionIndex3[2]], CharacterSave.CanPierceObstacle[CharacterSave.GunFunctionIndex3[2]], CharacterSave.CanPierceCharacter[CharacterSave.GunFunctionIndex3[2]], CharacterSave.GunClassDamagePercentage[CharacterSave.GunFunctionIndex3[2]], CharacterSave.DamageFallOff[CharacterSave.GunFunctionIndex3[2]], CharacterSave.BulletsAdded[CharacterSave.GunFunctionIndex3[2]], CharacterSave.GunEffectTrial[CharacterSave.GunFunctionIndex3[2]], CharacterSave.GunEffectTrialAdjust[CharacterSave.GunFunctionIndex3[2]], CharacterSave.GunEffectHit[CharacterSave.GunFunctionIndex3[2]], CharacterSave.GunEffectHitAdjust[CharacterSave.GunFunctionIndex3[2]]);
        //Enemy
        Enemy1 = Instantiate(CharacterSave.Enemies[0], new Vector3(0, 0, 0), Quaternion.identity);
        Enemy1.gameObject.GetComponent<CharacterBase>().CharacterLocationIndex = new Vector2(GridData.XWidthPublic-1,0);
        Enemy2 = Instantiate(CharacterSave.Enemies[1], new Vector3(0, 0, 0), Quaternion.identity);
        Enemy2.gameObject.GetComponent<CharacterBase>().CharacterLocationIndex = new Vector2(GridData.XWidthPublic-1, Mathf.RoundToInt((GridData.YWidthPublic-1) / 2));
        Enemy3 = Instantiate(CharacterSave.Enemies[2], new Vector3(0, 0, 0), Quaternion.identity);
        Enemy3.gameObject.GetComponent<CharacterBase>().CharacterLocationIndex = new Vector2(GridData.XWidthPublic-1, GridData.YWidthPublic-1);
        //Headers
        Instantiate(CharacterSave.EnemyHeader[0], CharacterSave.EnemyHeaderLocation[0], Quaternion.identity.normalized);
        Instantiate(CharacterSave.EnemyHeader[1], CharacterSave.EnemyHeaderLocation[1], Quaternion.identity.normalized);
        Instantiate(CharacterSave.EnemyHeader[2], CharacterSave.EnemyHeaderLocation[2], Quaternion.identity.normalized);
        Camera.main.gameObject.GetComponent<MoveSystem>().CharacterOnField[0] = SandmanPrefab;
        Camera.main.gameObject.GetComponent<MoveSystem>().CharacterOnField[1] = JadePrefab;
        Camera.main.gameObject.GetComponent<MoveSystem>().CharacterOnField[2] = AurthurPrefab;
        Camera.main.gameObject.GetComponent<MoveSystem>().CharacterOnField[3] = Enemy1;
        Camera.main.gameObject.GetComponent<MoveSystem>().CharacterOnField[4] = Enemy2;
        Camera.main.gameObject.GetComponent<MoveSystem>().CharacterOnField[5] = Enemy3;
        for(int x = 0; x < Camera.main.gameObject.GetComponent<MoveSystem>().CharacterOnField.Length; x++)
        {
            if(Camera.main.gameObject.GetComponent<MoveSystem>().CharacterOnField[x] != null)
            {
                BaseCharacterStatusInsert Script;
                Script = (BaseCharacterStatusInsert)Camera.main.gameObject.GetComponent<MoveSystem>().CharacterOnField[x].AddComponent(typeof(BaseCharacterStatusInsert));
                Script.StatusAdjust = StatusAdjust;
                Script.StatusSprite = StatusSprite;
            }
        }
    }

}
