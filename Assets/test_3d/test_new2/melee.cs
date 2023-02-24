using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : MonoBehaviour
{
    public GameObject player_melee;
    public void player_melee_on()
    {
        player_melee.SetActive(true);
    }
    public void player_melee_off()
    {
        player_melee.SetActive(false);
    }
}
