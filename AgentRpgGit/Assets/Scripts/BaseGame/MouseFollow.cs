using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    //Mousefollow object needs to be called MouseObject
    //or MovementUI WILL NOT WORK
    //This Script is meant to repersent the cursor interactions
    //-69,-420 is default mousePositionGridChoose if its out
    [SerializeField]
    public Vector2 MousePositionGridChoose;
    [SerializeField]
    GameObject GridIn;
    [SerializeField]
    Color HighLightColor;
    [SerializeField]
    Color GroupColor;
    public Color OriginalColor;
    public bool IsSelecting;
    public bool ObstacleSelectAllowed;
    public bool CharacterSelectAllowed;
    //What is allowed to be selected
    //1 x1
    //2 y1
    //3 x2
    //4 y2
    // For second array
    public int[][] AllowedSelected = new int[6][];
    //Same Idea However adjusts for position
    public int[][] GroupSelection = new int[6][];
    //
    public Color[][] GroupSelectionOriginalColors;
    public bool WillGroupSelect;
    [SerializeField]
    public bool OnGrid;
    GridLoad GridInfo;
    //DEBUG
    public bool CheckAllowedSelected;
    void Start()
    {
        GridInfo = Camera.main.GetComponent<GridLoad>();
        WipeAllowedSelected();
    }
    public void WipeGroupSelect()
    {
        for (int z = 0; z < GroupSelection.Length; z++)
        {
            for (int XCurrent = GroupSelection[z][0]; XCurrent < GroupSelection[z][2]; XCurrent++)
            {
                for (int YCurrent = GroupSelection[z][1]; YCurrent < GroupSelection[z][3]; YCurrent++)
                {
                    if(GridIn != null)
                    {
                        //Switches over x,y to y,x to go with AllGrid way of doing things
                        Vector2 CurrentArea = new Vector2(GridIn.GetComponent<GridControl>().GridCoordinate.y + YCurrent, GridIn.GetComponent<GridControl>().GridCoordinate.x + XCurrent);
                        if (CurrentArea.x >= 0 && CurrentArea.y >= 0 && CurrentArea.y < GridInfo.XWidthPublic && CurrentArea.x < GridInfo.YWidthPublic)
                        {
                            GridInfo.AllGrids[(int)CurrentArea.x][(int)CurrentArea.y].GetComponent<SpriteRenderer>().color = Color.white;
                        }
                    }
                }
            }
        }
    }
    //This makes it so nothing is allowed
    public void WipeAllowedSelected()
    {
        for (int x = 0; x < GroupSelection.Length; x++)
        {
            GroupSelection[x] = new int[4];
            for (int y = 0; y < 4; y++)
            {
                if (y == 0)
                {
                    GroupSelection[x][y] = -1;
                }
                else if (y == 1)
                {
                    GroupSelection[x][y] = -1;
                }
                else if (y == 2)
                {
                    GroupSelection[x][y] = -1;
                }
                else if (y == 3)
                {
                    GroupSelection[x][y] = -1;
                }

            }

        }
        GroupSelectionOriginalColors = new Color[GridInfo.YWidthPublic][];
        for(int x = 0; x < GroupSelectionOriginalColors.Length; x++)
        {
            GroupSelectionOriginalColors[x] = new Color[GridInfo.XWidthPublic];
        }
        for (int x = 0; x < AllowedSelected.Length; x++)
        {
            AllowedSelected[x] = new int[4];
            for (int y = 0; y < 4; y++)
            {
                if (y == 0)
                {
                    AllowedSelected[x][y] = -1;
                }
                else if (y == 1)
                {
                    AllowedSelected[x][y] = -1;
                }
                else if (y == 2)
                {
                    AllowedSelected[x][y] = -1;
                }
                else if (y == 3)
                {
                    AllowedSelected[x][y] = -1;
                }

            }

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GridIn = col.gameObject;
        GridControl ColGridInfo;
        ColGridInfo = col.gameObject.GetComponent<GridControl>();
        if(col.gameObject.GetComponent<SpriteRenderer>().color != GroupColor && col.gameObject.GetComponent<SpriteRenderer>().color != HighLightColor)
        {
            OriginalColor = col.gameObject.GetComponent<SpriteRenderer>().color;
        }
        for(int x = 0; x < AllowedSelected.Length; x++)
        {
            if (col.gameObject.tag == "Grid" && IsSelecting && ColGridInfo.GridCoordinate.x >= (float)AllowedSelected[x][0] && ColGridInfo.GridCoordinate.y >= (float)AllowedSelected[x][1] && ColGridInfo.GridCoordinate.x <= (float)AllowedSelected[x][2] && ColGridInfo.GridCoordinate.y <= (float)AllowedSelected[x][3])
            {
                if(WillGroupSelect == true)
                {
                    for(int z = 0; z < GroupSelection.Length; z++)
                    {
                        for (int XCurrent = GroupSelection[z][0]; XCurrent < GroupSelection[z][2]; XCurrent++)
                        {
                            for (int YCurrent = GroupSelection[z][1]; YCurrent < GroupSelection[z][3]; YCurrent++)
                            {

                                //Switches over x,y to y,x to go with AllGrid way of doing things
                                bool WillGroupActivate;
                                Vector2 CurrentArea = new Vector2(ColGridInfo.GridCoordinate.y + YCurrent, ColGridInfo.GridCoordinate.x + XCurrent);
                                if (CurrentArea.x >= 0 && CurrentArea.y >= 0 && CurrentArea.y < GridInfo.XWidthPublic && CurrentArea.x < GridInfo.YWidthPublic)
                                {
                                    GridControl CurrentGridInfo = GridInfo.AllGrids[(int)CurrentArea.x][(int)CurrentArea.y].GetComponent<GridControl>();
                                    GroupSelectionOriginalColors[(int)CurrentArea.x][(int)CurrentArea.y] = GridInfo.AllGrids[(int)CurrentArea.x][(int)CurrentArea.y].GetComponent<SpriteRenderer>().color;
                                    GridInfo.AllGrids[(int)CurrentArea.x][(int)CurrentArea.y].GetComponent<SpriteRenderer>().color = GroupColor;
                                }
                            }
                        }
                    }
                }
                bool WillActivate;
                WillActivate = true;
                if (!CharacterSelectAllowed && ColGridInfo.CharacterOn != null)
                {
                    WillActivate = false;
                }
                if (!ObstacleSelectAllowed && ColGridInfo.ObstacleIndex != 0)
                {
                    WillActivate = false;
                }
                if (WillActivate == true)
                {
                    OnGrid = true;
                    MousePositionGridChoose = ColGridInfo.GridCoordinate;
                    col.gameObject.GetComponent<SpriteRenderer>().color = HighLightColor;
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        GridIn = col.gameObject;
        GridControl ColGridInfo;
        ColGridInfo = col.gameObject.GetComponent<GridControl>();
        for (int x = 0; x < AllowedSelected.Length; x++)
        {
            if (col.gameObject.tag == "Grid" && IsSelecting && ColGridInfo.GridCoordinate.x >= (float)AllowedSelected[x][0] && ColGridInfo.GridCoordinate.y >= (float)AllowedSelected[x][1] && ColGridInfo.GridCoordinate.x <= (float)AllowedSelected[x][2] && ColGridInfo.GridCoordinate.y <= (float)AllowedSelected[x][3])
            {
                if (WillGroupSelect == true)
                {
                    for (int z = 0; z < GroupSelection.Length; z++)
                    {
                        for (int XCurrent = GroupSelection[z][0]; XCurrent < GroupSelection[z][2]; XCurrent++)
                        {
                            for (int YCurrent = GroupSelection[z][1]; YCurrent < GroupSelection[z][3]; YCurrent++)
                            {

                                //Switches over x,y to y,x to go with AllGrid way of doing things
                                Vector2 CurrentArea = new Vector2(ColGridInfo.GridCoordinate.y + YCurrent, ColGridInfo.GridCoordinate.x + XCurrent);
                                if (CurrentArea.x >= 0 && CurrentArea.y >= 0 && CurrentArea.y < GridInfo.XWidthPublic && CurrentArea.x < GridInfo.YWidthPublic)
                                {
                                    GridInfo.AllGrids[(int)CurrentArea.x][(int)CurrentArea.y].GetComponent<SpriteRenderer>().color = GroupSelectionOriginalColors[(int)CurrentArea.x][(int)CurrentArea.y];
                                }
                            }
                        }
                    }
                }
            }
        }
        col.gameObject.GetComponent<SpriteRenderer>().color = OriginalColor;
        for (int x = 0; x < AllowedSelected.Length; x++)
        {
            if (col.gameObject.tag == "Grid" && IsSelecting && col.gameObject.GetComponent<GridControl>().GridCoordinate.x >= (float)AllowedSelected[x][0] && col.gameObject.GetComponent<GridControl>().GridCoordinate.y >= (float)AllowedSelected[x][1] && col.gameObject.GetComponent<GridControl>().GridCoordinate.x <= (float)AllowedSelected[x][2] && col.gameObject.GetComponent<GridControl>().GridCoordinate.y <= (float)AllowedSelected[x][3])
            {
                OnGrid = false;
            }
        }
    }
    void Update()
    {
        //Debug
        /*
        if (CheckAllowedSelected)
        {
            print(AllowedSelected[0][0] + "," + AllowedSelected[0][1] + ","+ AllowedSelected[0][2] + "," + AllowedSelected[0][3] + " // " + AllowedSelected[1][0] + "," + AllowedSelected[1][1] + "," + AllowedSelected[1][2] + "," + AllowedSelected[1][3] + " // " + AllowedSelected[2][0] + "," + AllowedSelected[2][1] + "," + AllowedSelected[2][2] + "," + AllowedSelected[2][3] + " // " + AllowedSelected[3][0] + "," + AllowedSelected[3][1] + "," + AllowedSelected[3][2] + "," + AllowedSelected[3][3] +  " // "  + AllowedSelected[4][0] + "," + AllowedSelected[4][1] + "," + AllowedSelected[4][2] + "," + AllowedSelected[4][3]);
        }*/
        gameObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,0)).x, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)).y,0);
        if(IsSelecting == false && OnGrid == true)
        { 
            GridIn.GetComponent<SpriteRenderer>().color = OriginalColor;
            OnGrid = false;
        }
    }
}
