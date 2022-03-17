using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindStats : MonoBehaviour
{
    public CharacterRememberance g;
    public int findIndex;
    public GameObject Speed;
    public GameObject Defense;
    public GameObject Attack;
    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.FindGameObjectWithTag("CharacterRemeberance").GetComponent<CharacterRememberance>();
        Speed.GetComponent<TMPro.TextMeshProUGUI>().text = "Speed: " + g.Speed[findIndex];
        Defense.GetComponent<TMPro.TextMeshProUGUI>().text = "Defense: " + g.Defense[findIndex];
        Attack.GetComponent<TMPro.TextMeshProUGUI>().text = "Attack: " + g.Damage[findIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
