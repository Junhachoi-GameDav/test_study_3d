using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace sg
{
    public class damage_player : MonoBehaviour
    {
        public int damage = 20;
        private void OnTriggerEnter(Collider other)
        {

            player_stats player_st = other.GetComponent<player_stats>();

            if(player_st != null)
            {
                player_st.take_damage(damage);
            }
        }
    }
}
