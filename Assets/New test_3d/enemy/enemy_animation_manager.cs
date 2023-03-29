using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class enemy_animation_manager : animator_manager
    {
        //enemy_locomotion_manager en_loco_mng;
        enemy_manager enemy_mng;
        private void Awake()
        {
            anime = GetComponent<Animator>();
            enemy_mng = GetComponent<enemy_manager>();
        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemy_mng.en_rigid.drag = 0;
            Vector3 delta_pos = anime.deltaPosition;
            delta_pos.y = 0;
            Vector3 velocity = delta_pos / delta;
            enemy_mng.en_rigid.velocity = velocity;
        }
    }
}

