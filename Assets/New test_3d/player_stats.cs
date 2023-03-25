using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace sg
{
    public class player_stats : character_stats
    {
        
        health_bar h_bar;
        stamina_bar stamina_Bar;
        animater_handler animater_h;

        private void Awake()
        {
            h_bar = FindObjectOfType<health_bar>();
            stamina_Bar = FindObjectOfType<stamina_bar>();
            animater_h = GetComponentInChildren<animater_handler>();
        }
        private void Start()
        {

            max_health = set_max_health_from_health_level();
            cur_health = max_health;
            h_bar.set_max_health(max_health);
            h_bar.set_cur_health(cur_health);


            max_stamina = set_max_stamina_from_stamina_level();
            cur_stamina = max_stamina;
        }

        private int set_max_health_from_health_level()
        {
            max_health = health_level * 10;
            return max_health;
        }
        private int set_max_stamina_from_stamina_level()
        {
            max_stamina = stamina_level * 10;
            return max_stamina;
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

        public void take_stamina_damage(int damage)
        {
            cur_stamina = cur_stamina - damage;
            //set bar
            stamina_Bar.set_cur_stamina(cur_stamina);
        }
    }
}
