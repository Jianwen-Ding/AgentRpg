using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        InserterScript = gameObject.GetComponent<ObstacleInsert>();
        IsTargetedSpriteRender = IsTargetedSprite.GetComponent<SpriteRenderer>();
        IsDamageSpriteRender = IsDamageSprite.GetComponent<SpriteRenderer>();
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
            IsTargetedSpriteRender.color = new Color(0, 0, 255, 255); 
        }
        else
        {
            IsTargetedSpriteRender.color = new Color(255, 0, 0, 0);
        }
        if (IsDamageTargeted)
        {
            IsDamageSpriteRender.color = new Color(255, 0, 0, 255);
        }
        else
        {
            IsDamageSpriteRender.color = new Color(255, 0, 0, 0);
        }
    }
}
