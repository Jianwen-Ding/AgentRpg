using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    public Vector2 GridCoordinate;
    //"None" is the default
    [SerializeField]
    string StatusAffected;
    //"None" is the default
    [SerializeField]
    public int ObstacleIndex;
    [SerializeField]
    public int StatusIndex;
    [SerializeField]
    public bool AllowsForPenentration;
    public GameObject CharacterOn;
    public bool IsDamageTargeted;
    public bool IsTargeted;
    [SerializeField]
    SpriteRenderer CurrentRender;
    [SerializeField]
    GameObject IsTargetedSprite;
    [SerializeField]
    SpriteRenderer IsTargetedSpriteRender;
    [SerializeField]
    GameObject IsDamageSprite;
    [SerializeField]
    SpriteRenderer IsDamageSpriteRender;
    [SerializeField]
    public bool HasBeenHit;
    [SerializeField]
    ObstacleInsert InserterScript;

    // For color effects
    class colorEffect
    {
        public string effectID;
        public int prio;
        public UnityEngine.Color colorCarried;
        public colorEffect(string setID, int setPrio, UnityEngine.Color setColor)
        {
            effectID = setID;
            prio = setPrio;
            colorCarried = setColor;
        }
    }
    List<colorEffect> colorEffects = new List<colorEffect>();

    // Start is called before the first frame update
    void Start()
    {
        InserterScript = gameObject.GetComponent<ObstacleInsert>();
        IsTargetedSpriteRender = IsTargetedSprite.GetComponent<SpriteRenderer>();
        IsDamageSpriteRender = IsDamageSprite.GetComponent<SpriteRenderer>();
        CurrentRender = gameObject.GetComponent<SpriteRenderer>();
    }

    void checkGreatestPrioColor()
    {
        int greatestEffect = -1;
        int greatestPrio = -1;
        for(int i = 0; i < colorEffects.Count; i++)
        {
            if (colorEffects[i].prio > greatestPrio)
            {
                greatestPrio = colorEffects[i].prio;
                greatestEffect = i;
            }
        }
        if(greatestEffect >= 0)
        {
            CurrentRender.color = colorEffects[greatestEffect].colorCarried;
        }
        else
        {
            CurrentRender.color = UnityEngine.Color.white;
        }
    }

    public void removeColorAlt(string ID)
    {
        for(int i = 0; i < colorEffects.Count; i++)
        {
            if (colorEffects[i].effectID == ID)
            {
                colorEffects.Remove(colorEffects[i]);
                checkGreatestPrioColor();
            }
        }
    }
    public void addColorAlt(string ID, int prio, UnityEngine.Color color)
    {
        colorEffects.Add(new colorEffect(ID, prio, color));
        checkGreatestPrioColor();
    }

    // Update is called once per frame
    void Update()
    {
        InserterScript.InsertObstacle(ObstacleIndex, gameObject);
        InserterScript.InsertStatus(StatusIndex, gameObject);
        if (CharacterOn != null && CharacterOn.GetComponent<CharacterBase>().CharacterLocationIndex != GridCoordinate )
        {
            CharacterOn = null;
        }
        if (IsTargeted)
        {
            IsTargetedSpriteRender.color = new UnityEngine.Color(0, 0, 255, 255); 
        }
        else
        {
            IsTargetedSpriteRender.color = new UnityEngine.Color(255, 0, 0, 0);
        }
        if (IsDamageTargeted)
        {
            IsDamageSpriteRender.color = new UnityEngine.Color(255, 0, 0, 255);
        }
        else
        {
            IsDamageSpriteRender.color = new UnityEngine.Color(255, 0, 0, 0);
        }
    }
}
