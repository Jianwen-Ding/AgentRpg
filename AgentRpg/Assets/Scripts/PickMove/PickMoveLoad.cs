using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickMoveLoad : MonoBehaviour
{
    [SerializeField]
    int MaxAmountOfMoves;
    [SerializeField]
    bool[] TypesOfMovesAllowed;
    [SerializeField]
    bool[] SandmanMoveAllow;
    [SerializeField]
    bool[] JadeMoveAllow;
    [SerializeField]
    bool[] AurthurAllow;
    [SerializeField]
    float GridStartx;
    [SerializeField]
    float GridStarty;
    [SerializeField]
    float Changex;
    [SerializeField]
    float Changey;
    [SerializeField]
    float MinChangeMaxY;
    [SerializeField]
    int MaxX;
    [SerializeField]
    GameObject GameObjectAllAttach;
    [SerializeField]
    GameObject ButtonChoosePrefab;
    [SerializeField]
    GameObject DescriptorObject;
    // Start is called before the first frame update
    void Start()
    {
        switch (PlayerPrefs.GetInt("CharacterIndexRemember" ))
        {
            case 0:
                TypesOfMovesAllowed = SandmanMoveAllow;
                break;
            case 1:
                TypesOfMovesAllowed = JadeMoveAllow;
                break;
            case 2:
                TypesOfMovesAllowed = AurthurAllow;
                break;
        }
        int MovesLoaded = 0;
        int RowsLoaded = 0;
        int MovesLoadedInRow;
        while(MovesLoaded < MaxAmountOfMoves)
        {
            MovesLoadedInRow = 0;
            while (MovesLoadedInRow < MaxX && MovesLoaded < MaxAmountOfMoves)
            {
                GameObject GameObjectLoaded = Instantiate(ButtonChoosePrefab, new Vector3(GridStartx + Changex * MovesLoadedInRow, GridStarty + Changey * RowsLoaded, 0), Quaternion.identity.normalized);
                GameObjectLoaded.GetComponent<MoveDisplay>().IsLocked = !TypesOfMovesAllowed[MovesLoaded];
                GameObjectLoaded.GetComponent<LoadSpecialMoveDescription>().InsertIndex(MovesLoaded, DescriptorObject);
                GameObjectLoaded.transform.parent = GameObjectAllAttach.transform;
                MovesLoadedInRow++;
                MovesLoaded++;
            }
            RowsLoaded++;
        }
        GameObjectAllAttach.GetComponent<PickMoveSlide>().MinY = 0;
        GameObjectAllAttach.GetComponent<PickMoveSlide>().MaxY = -Changey * RowsLoaded + MinChangeMaxY;
        //???????????????????????????????
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
