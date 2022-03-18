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
    public GameObject MaxHealth;
    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.FindGameObjectWithTag("CharacterRemeberance").GetComponent<CharacterRememberance>();
        Speed.GetComponent<TMPro.TextMeshProUGUI>().text = "Speed: " + g.Speed[findIndex];
        Defense.GetComponent<TMPro.TextMeshProUGUI>().text = "Defense: " + g.Defense[findIndex];
        Attack.GetComponent<TMPro.TextMeshProUGUI>().text = "Attack: " + g.Damage[findIndex];
        MaxHealth.GetComponent<TMPro.TextMeshProUGUI>().text = "Max Health: " + g.MaxHealth[findIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
