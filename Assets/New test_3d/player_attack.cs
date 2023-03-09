using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class player_attack : MonoBehaviour
    {
        animater_handler anime_h;
        input_handler input_h;
        public string last_atk;
        public string last_atk2;

        private void Awake()
        {
            anime_h = GetComponentInChildren<animater_handler>();
            input_h = GetComponent<input_handler>();
        }

        public void handle_weapon_combo(weapon_item weapon)
        {
            if (input_h.combo_flag)
            {
                anime_h.anime.SetBool("can_combo", false);
                if (last_atk == weapon.o_h_light_atk_1)
                {
                    //¥Ÿ¿Ω∞≈
                    anime_h.anime.SetBool("can_combo", false);
                    anime_h.player_target_animation(weapon.o_h_light_atk_2, true);
                    last_atk2 = weapon.o_h_light_atk_2;
                    StartCoroutine(handle_combo_routine());
                }

                if(last_atk == weapon.o_h_light_atk_2)
                {
                    anime_h.anime.SetBool("can_combo", false);
                    anime_h.player_target_animation(weapon.o_h_light_atk_3, true);

                }
            }
        }
        IEnumerator handle_combo_routine()
        {
            yield return null;
            last_atk = last_atk2;
        }
        public void handle_light_atk(weapon_item weapon)
        {
            anime_h.player_target_animation(weapon.o_h_light_atk_1, true);
            last_atk = weapon.o_h_light_atk_1;
        }
        public void handle_heavy_atk(weapon_item weapon)
        {
            anime_h.player_target_animation(weapon.o_h_heavy_atk_1, true);
            last_atk = weapon.o_h_heavy_atk_1;
        }
    }
}

