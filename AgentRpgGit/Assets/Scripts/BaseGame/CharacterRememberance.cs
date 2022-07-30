using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterRememberance : MonoBehaviour
{
    //BackGround
    public GameObject backGround;
    //Music
    public AudioClip music;
    public float replayStart;
    public float replayEnd;
    //1 is max
    public float volume;
    //1 = Sandman - Caster 
    //2 = Jade    - Carry
    //3 = Aurthur - Tank
    public float[] Health = new float[3];
    public float[] MaxHealth = new float[3];
    public float[] Damage = new float[3];
    public float[] Speed = new float[3];
    public float[] Defense = new float[3];
    public int[] PassiveIndex = new int[3];
    public int[] MovesPutIn1 = new int[4];
    public int[] MovesPutIn2 = new int[4];
    public int[] MovesPutIn3 = new int[4];
    //Gun Varibles Stored In here 
    public int[] GunFunctionIndex3 = new int[3];
    public string[] GunName = new string[50];
    public GameObject[] GunEffectTrial = new GameObject[50];
    public Vector2[] GunEffectTrialAdjust = new Vector2[50];
    public GameObject[] GunEffectHit = new GameObject[50];
    public Vector2[] GunEffectHitAdjust = new Vector2[50];
    [SerializeField]
    public int[] GunRange = new int[50];
    [SerializeField]
    public float[] GunClassDamagePercentage = new float[50];
    [SerializeField]
    public bool[] CanPierceObstacle = new bool[50];
    [SerializeField]
    public bool[] CanPierceCharacter = new bool[50];
    [SerializeField]
    public int[] BulletsAdded = new int[50];
    [SerializeField]
    public float[] DamageFallOff = new float[50];
    public bool HasWon;

    [SerializeField]
    //EnemySpawning
    public GameObject[] Enemies = new GameObject[3];
    public GameObject[] EnemyHeader = new GameObject[3];
    public Vector3[] EnemyHeaderLocation = new Vector3[3];

    // Start is called before the first frame update
    private void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("CharacterRemeberance").Length != 1)
        {
            Destroy(gameObject);
        }


    }
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(gameObject);
    }
}
