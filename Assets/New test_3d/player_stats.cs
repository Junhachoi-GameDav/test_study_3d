using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace sg
{
    public class player_stats : MonoBehaviour
    {
        public int health_level = 10;
        public int max_health;
        public int cur_health;

        public health_bar h_bar;

        animater_handler animater_h;

        private void Awake()
        {
            animater_h = GetComponentInChildren<animater_handler>();
        }
        private void Start()
        {

            max_health = set_max_health_from_health_level();
            cur_health = max_health;
            h_bar.set_max_health(max_health);
        }

        private int set_max_health_from_health_level()
        {
            max_health = health_level * 10;
            return max_health;
        }

        public void take_damage(int damage)
        {
            cur_health = cur_health - damage;

            h_bar.set_cur_health(cur_health);

            animater_h.player_target_animation("heavy_hited", true);

            if(cur_health <= 0)
            {
                cur_health = 0;
                animater_h.player_target_animation("dying", true);
            }
        }
    }
}
