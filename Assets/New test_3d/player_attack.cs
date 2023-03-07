using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class player_attack : MonoBehaviour
    {
        animater_handler anime_h;
        private void Awake()
        {
            anime_h = GetComponentInChildren<animater_handler>();
        }
        public void handle_light_atk(weapon_item weapon)
        {
            anime_h.player_target_animation(weapon.o_h_light_atk_1, true);
        }
        public void handle_heavy_atk(weapon_item weapon)
        {
            anime_h.player_target_animation(weapon.o_h_heavy_atk_1, true);

        }
    }
}

