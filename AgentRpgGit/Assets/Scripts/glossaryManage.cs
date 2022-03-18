using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glossaryManage : MonoBehaviour
{
    [SerializeField]
    string[] text;
    [SerializeField]
    GameObject textPrefab;
    [SerializeField]
    float GridStartx;
    [SerializeField]
    float GridStarty;
    [SerializeField]
    float Changey;
    [SerializeField]
    float MinChangeMaxY;
    [SerializeField]
    GameObject GameObjectAllAttach;
    // Start is called before the first frame update
    void Start()
    {

        int MovesLoaded = 0;
            while(MovesLoaded < text.Length)
            {
                GameObject GameObjectLoaded = Instantiate(textPrefab, new Vector3(GridStartx , GridStarty + Changey * MovesLoaded, 0), Quaternion.identity.normalized);
                GameObjectLoaded.GetComponent<TMPro.TextMeshProUGUI>().text = text[MovesLoaded];
                GameObjectLoaded.transform.parent = GameObjectAllAttach.transform;
            MovesLoaded++;
            }
        GameObjectAllAttach.GetComponent<PickMoveSlide>().MinY = 0;
        GameObjectAllAttach.GetComponent<PickMoveSlide>().MaxY = -Changey * MovesLoaded + MinChangeMaxY;
        //???????????????????????????????
    }
}
