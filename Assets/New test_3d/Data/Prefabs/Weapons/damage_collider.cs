using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace sg
{
    public class damage_collider : MonoBehaviour
    {
        Collider d_collider;

        public int cur_weapon_damage = 10;

        private void Awake()
        {
            d_collider = GetComponent<Collider>();
            d_collider.gameObject.SetActive(true);
            d_collider.isTrigger = true;
            d_collider.enabled = false;
        }

        public void enable_damage_collider()
        {
            d_collider.enabled = true;
        }
        public void disable_damage_collider()
        {
            d_collider.enabled = false;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if(collision.CompareTag("Player"))
            {
                player_stats p_stats = collision.GetComponent<player_stats>();

                if (p_stats != null)
                {
                    p_stats.take_damage(cur_weapon_damage);
                }
            }

            if(collision.CompareTag("Enemy"))
            {
                enemy_stats en_stats = collision.GetComponent<enemy_stats>();

                if(en_stats != null)
                {
                    en_stats.take_damage(cur_weapon_damage);
                }
            }
        }
    }
}

