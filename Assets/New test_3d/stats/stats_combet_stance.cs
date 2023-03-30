using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class stats_combet_stance : stats
    {
        public stats_attack stats_atk;
        public stats_pursue_target stats_pur_target;
        public override stats tick(enemy_manager enemy_mng, enemy_stats en_stats, enemy_animation_manager en_anime_mng)
        {
            float distance_from_target = Vector3.Distance(enemy_mng.cur_target.transform.position,
                enemy_mng.transform.position);
            if (enemy_mng.is_preforming_action)
            {
                en_anime_mng.anime.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            }



            if(enemy_mng.cur_recovery_time <=0 && distance_from_target <= enemy_mng.max_attack_range)
            {
                return stats_atk;
            }
            else if(distance_from_target > enemy_mng.max_attack_range)
            {
                return stats_pur_target;
            }
            else
            {
                return this;
            }
        }
    }
}

