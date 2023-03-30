using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class stats_ambush : stats
    {
        public bool is_sleeping;
        public float detection_radius = 2;
        public string sleep_animation;
        public string wake_animation;

        public LayerMask detection_layer;

        public stats_pursue_target stats_pur_target;

        public override stats tick(enemy_manager enemy_mng, enemy_stats en_stats, enemy_animation_manager en_anime_mng)
        {
            if(is_sleeping && enemy_mng.is_interacting == false)
            {
                en_anime_mng.player_target_animation(sleep_animation, true);
            }

            #region handle target detection
            Collider[] colliders = Physics.OverlapSphere(enemy_mng.transform.position, detection_radius, detection_layer);

            for (int i = 0; i < colliders.Length; i++)
            {
                character_stats char_stats = colliders[i].transform.GetComponent<character_stats>();

                if(char_stats != null)
                {
                    Vector3 target_dir = char_stats.transform.position - enemy_mng.transform.position;
                    float viewable_angle = Vector3.Angle(target_dir, enemy_mng.transform.forward);

                    if(viewable_angle > enemy_mng.min_detection_angle &&
                        viewable_angle < enemy_mng.max_detection_angle)
                    {
                        enemy_mng.cur_target = char_stats;
                        is_sleeping = false;
                        en_anime_mng.player_target_animation(wake_animation, true);
                    }
                }
            }
            #endregion

            #region handle stats change

            if(enemy_mng.cur_target != null)
            {
                return stats_pur_target;
            }
            else
            {
                return this;
            }

            #endregion

        }
    }
}

