using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class enemy_stats : MonoBehaviour
    {
        public int health_level = 10;
        public int max_health;
        public int cur_health;


        Animator anime;

        private void Awake()
        {
            anime = GetComponentInChildren<Animator>();
        }
        private void Start()
        {
            max_health = set_max_health_from_health_level();
            cur_health = max_health;
        }

        private int set_max_health_from_health_level()
        {
            max_health = health_level * 10;
            return max_health;
        }

        public void take_damage(int damage)
        {
            cur_health = cur_health - damage;
            Debug.Log("hit");

            //anime.Play("damage_1");

            if (cur_health <= 0)
            {
                cur_health = 0;
                //anime.Play("damage_1");
                Debug.Log("dead");
            }
        }
    }
}

