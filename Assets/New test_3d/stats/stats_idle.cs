using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class stats_idle : stats
    {
        public stats_pursue_target stats_pur_target;

        public LayerMask detection_layer;

        public override stats tick(enemy_manager enemy_mng, enemy_stats en_stats, enemy_animation_manager en_anime_mng)
        {
            #region handle enemy target detection
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemy_mng.detection_radius, detection_layer);

            for (int i = 0; i < colliders.Length; i++)
            {
                character_stats ch_stats = colliders[i].transform.GetComponent<character_stats>();

                if (ch_stats != null)
                {
                    //

                    Vector3 target_dir = ch_stats.transform.position - transform.position;
                    float viewable_angle = Vector3.Angle(target_dir, transform.forward);

                    if (viewable_angle > enemy_mng.min_detection_angle && viewable_angle < enemy_mng.max_detection_angle)
                    {
                        enemy_mng.cur_target = ch_stats;
                    }
                }
            }
            #endregion

            #region handle switching to next state
            if (enemy_mng.cur_target != null)
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

