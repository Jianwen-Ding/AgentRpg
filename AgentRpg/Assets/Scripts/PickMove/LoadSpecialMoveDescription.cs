using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadSpecialMoveDescription : MonoBehaviour
{
    MoveDescriptor DescriptorScript;
    int MoveIndex = -69;
    bool HasBeenInserted = false;
    // Start is called before the first frame update
    public void InsertIndex(int MoveIndexInsert, GameObject DescriptorInsert)
    {
        if (gameObject.GetComponent<MoveDisplay>().IsLocked)
        {
            gameObject.GetComponent<Button>().enabled = false;
        }
        MoveIndex = MoveIndexInsert;
        DescriptorScript = DescriptorInsert.GetComponent<MoveDescriptor>();
        gameObject.GetComponent<MoveDisplay>().InsertMoveIndex(MoveIndex);
        HasBeenInserted = true;
    }
    public void Activate()
    {
        if (HasBeenInserted == true && !gameObject.GetComponent<MoveDisplay>().IsLocked)
        {
            DescriptorScript.MoveIndex = MoveIndex;
        }
    }
}
