using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInsert : MonoBehaviour
{
    [SerializeField]
    GameObject[] ObstacleSprite = new GameObject[20];
    [SerializeField]
    Vector2[] ObstacleAdjust = new Vector2[20];
    [SerializeField]
    GameObject[] StatusSprite = new GameObject[20];
    [SerializeField]
    Vector2[] StatusAdjust = new Vector2[20];
    [SerializeField]
    int indexInsertObstacle;
    [SerializeField]
    GameObject GameObjectInsertIntoObstacle;
    [SerializeField]
    bool StartedSprite;
    [SerializeField]
    int indexInsertStatus;
    [SerializeField]
    GameObject GameObjectInsertIntoStatus;
    [SerializeField]
    bool StartedStatus;
    [SerializeField]
    GameObject StatusSpriteCurrent;
    [SerializeField]
    GameObject ObstacleSpriteCurrent;
    [SerializeField]
    int PreviousIntStatus;
    [SerializeField]
    int PreviousIntObstacle;
    [SerializeField]
    public GameObject TextFloatObject;
    public void WipeObstacle()
    {
        Destroy(gameObject.GetComponent<BaseObstacle>());
        Destroy(ObstacleSpriteCurrent);
    }
    public void WipeStatus()
    {
        Destroy(gameObject.GetComponent<BaseStatus>());
        Destroy(StatusSpriteCurrent);
    }
    public void InsertObstacle(int index, GameObject GameObjectInsert)
    {
        indexInsertObstacle = index;
        GameObjectInsertIntoObstacle = GameObjectInsert;
        StartedSprite = true;
    }
    public void InsertStatus(int index, GameObject GameObjectInsert)
    {
        indexInsertStatus = index;
        GameObjectInsertIntoStatus = GameObjectInsert;
        StartedStatus = true;
    }
    public void DirectInsertIntoSelf(int i)
    {
        BaseStatus Insert = null;
        switch (i)
        {
            default:
                break;
            case 1:
                Insert = (BaseStatus)GameObjectInsertIntoStatus.AddComponent(typeof(BaseStatus));
                StatusSpriteCurrent = Instantiate(StatusSprite[indexInsertStatus], new Vector3(gameObject.transform.position.x + StatusAdjust[indexInsertStatus].x, gameObject.transform.position.y + StatusAdjust[indexInsertStatus].y), Quaternion.identity.normalized);
                break;
            case 2:
                Insert = (BaseStatus)GameObjectInsertIntoStatus.AddComponent(typeof(RiggedObjectBegginingStatus));
                StatusSpriteCurrent = Instantiate(StatusSprite[indexInsertStatus], new Vector3(gameObject.transform.position.x + StatusAdjust[indexInsertStatus].x, gameObject.transform.position.y + StatusAdjust[indexInsertStatus].y), Quaternion.identity.normalized);
                break;
            case 3:
                Insert = (BaseStatus)GameObjectInsertIntoStatus.AddComponent(typeof(RiggedObjectMidStatus));
                StatusSpriteCurrent = Instantiate(StatusSprite[indexInsertStatus], new Vector3(gameObject.transform.position.x + StatusAdjust[indexInsertStatus].x, gameObject.transform.position.y + StatusAdjust[indexInsertStatus].y), Quaternion.identity.normalized);
                break;
            case 4:
                Insert = (BaseStatus)GameObjectInsertIntoStatus.AddComponent(typeof(RiggedObjectEndStatus));
                StatusSpriteCurrent = Instantiate(StatusSprite[indexInsertStatus], new Vector3(gameObject.transform.position.x + StatusAdjust[indexInsertStatus].x, gameObject.transform.position.y + StatusAdjust[indexInsertStatus].y), Quaternion.identity.normalized);
                break;
            case 5:
                Insert = (BaseStatus)GameObjectInsertIntoStatus.AddComponent(typeof(MistyStatus));
                StatusSpriteCurrent = Instantiate(StatusSprite[indexInsertStatus], new Vector3(gameObject.transform.position.x + StatusAdjust[indexInsertStatus].x, gameObject.transform.position.y + StatusAdjust[indexInsertStatus].y), Quaternion.identity.normalized);
                break;
            case 6:
                Insert = (BaseStatus)GameObjectInsertIntoStatus.AddComponent(typeof(FirebombStatus));
                StatusSpriteCurrent = Instantiate(StatusSprite[indexInsertStatus], new Vector3(gameObject.transform.position.x + StatusAdjust[indexInsertStatus].x, gameObject.transform.position.y + StatusAdjust[indexInsertStatus].y), Quaternion.identity.normalized);
                break;
            case 7:
                Insert = (BaseStatus)GameObjectInsertIntoStatus.AddComponent(typeof(AcidrainStatus));
                StatusSpriteCurrent = Instantiate(StatusSprite[indexInsertStatus], new Vector3(gameObject.transform.position.x + StatusAdjust[indexInsertStatus].x, gameObject.transform.position.y + StatusAdjust[indexInsertStatus].y), Quaternion.identity.normalized);
                break;
            case 8:
                Insert = (BaseStatus)GameObjectInsertIntoStatus.AddComponent(typeof(HealstationStatus));
                StatusSpriteCurrent = Instantiate(StatusSprite[indexInsertStatus], new Vector3(gameObject.transform.position.x + StatusAdjust[indexInsertStatus].x, gameObject.transform.position.y + StatusAdjust[indexInsertStatus].y), Quaternion.identity.normalized);
                break;
            case 9:
                Insert = (BaseStatus)GameObjectInsertIntoStatus.AddComponent(typeof(ShockwaveMain));
                StatusSpriteCurrent = Instantiate(StatusSprite[indexInsertStatus], new Vector3(gameObject.transform.position.x + StatusAdjust[indexInsertStatus].x, gameObject.transform.position.y + StatusAdjust[indexInsertStatus].y), Quaternion.identity.normalized);
                break;
        }
    }
    void Update()
    {
        if (StartedStatus == true && PreviousIntStatus != indexInsertStatus)
        {
            WipeStatus();
            StartedStatus = false;
            DirectInsertIntoSelf(indexInsertStatus);
        }
        if (StartedSprite == true && PreviousIntObstacle != indexInsertObstacle)
        {
            WipeObstacle();
            StartedSprite = false;
            switch (indexInsertObstacle)
            {
                default:
                    gameObject.GetComponent<GridControl>().AllowsForPenentration = true;
                    gameObject.GetComponent<GridControl>().ObstacleIndex = 0;
                    break;
                case 1:
                    GameObjectInsertIntoObstacle.AddComponent(typeof(BaseObstacle));
                    ObstacleSpriteCurrent = Instantiate(ObstacleSprite[indexInsertObstacle], new Vector3(gameObject.transform.position.x + ObstacleAdjust[indexInsertObstacle].x, gameObject.transform.position.y + ObstacleAdjust[indexInsertObstacle].y), Quaternion.identity.normalized);
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
        PreviousIntObstacle = indexInsertObstacle;
        PreviousIntStatus = indexInsertStatus;
    }
    
}
