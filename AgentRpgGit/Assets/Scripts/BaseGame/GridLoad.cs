using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLoad : MonoBehaviour
{
    // REMEMBER [Y][X] I MESSED IT UP BUT SPAGETHBRHUI
    [SerializeField]
    Vector2 GridStartingPoint = new Vector2((float)-6.5, -1);
    [SerializeField]
    float XDiffriential = (float)1.95;
    [SerializeField]
    float YDiffriential = -(float)0.5;
    [SerializeField]
    float DiagonalHorizontalLength = (float)0.25;
    static int XWidth = 8;
    static int YWidth = 5;
    public int XWidthPublic;
    public int YWidthPublic;
    [SerializeField]
    GameObject Grid;
    [SerializeField]
    GameObject[] PlayerChampionsOnField = new GameObject[3];
    [SerializeField]
    public GameObject[][] AllGrids = new GameObject[YWidth][];
    // Start is called before the first frame update
    void Awake()
    {
        XWidthPublic = XWidth;
        YWidthPublic = YWidth;
        for (int y = 0; y < YWidth; y++)
        {
            AllGrids[y] = new GameObject[XWidth];
            for (int x = 0; x < XWidth; x++)
            {
                AllGrids[y][x] = Instantiate(Grid, new Vector3((float)GridStartingPoint.x + (XDiffriential * (float)x) -(DiagonalHorizontalLength * y), (float)GridStartingPoint.y + (YDiffriential * (float)y), 0), Quaternion.identity);
                AllGrids[y][x].gameObject.GetComponent<GridControl>().GridCoordinate = new Vector2(x,y);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
