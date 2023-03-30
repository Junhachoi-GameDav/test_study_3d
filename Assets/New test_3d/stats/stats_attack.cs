using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class stats_attack : stats
    {
        public stats_combet_stance stats_com_stance;
        
        public enemy_attack_ations[] enemy_atk_ations;
        public enemy_attack_ations cur_atk_ations;
        public override stats tick(enemy_manager enemy_mng, enemy_stats en_stats, enemy_animation_manager en_anime_mng)
        {
            Vector3 target_dir = enemy_mng.cur_target.transform.position - transform.position;
            float distance_from_target = Vector3.Distance(enemy_mng.cur_target.transform.position, enemy_mng.transform.position);
            float viewable_angle = Vector3.Angle(target_dir, transform.forward);
            //enemy_mng.viewable_angle = Vector3.Angle(target_dir, transform.forward);

            if (enemy_mng.is_preforming_action)
            {
                return stats_com_stance;
            }

            if(cur_atk_ations != null)
            {
                //가까이 있을시 새로운 공격
                if(distance_from_target < cur_atk_ations.min_distance_needed_to_atk)
                {
                    return this;
                }
                // 가까워지면 어택
                else if(distance_from_target < cur_atk_ations.max_distance_needed_to_atk)
                {
                    if(viewable_angle <= cur_atk_ations.max_attack_angle&&
                        viewable_angle >= cur_atk_ations.min_attack_angle)
                    {
                        if(enemy_mng.cur_recovery_time <=0 && enemy_mng.is_preforming_action == false)
                        {
                            en_anime_mng.anime.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                            en_anime_mng.anime.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                            en_anime_mng.player_target_animation(cur_atk_ations.action_animation, true);
                            enemy_mng.is_preforming_action = true;
                            enemy_mng.cur_recovery_time = cur_atk_ations.recovery_time;
                            cur_atk_ations = null;
                            return stats_com_stance;
                        }
                    }
                }
            }
            else
            {
                get_new_attack(enemy_mng);
            }

            return stats_com_stance;
        }

        private void get_new_attack(enemy_manager enemy_mng)
        {

            Vector3 target_dir = enemy_mng.cur_target.transform.position - transform.position;
            float viewable_angle = Vector3.Angle(target_dir, transform.forward);
            float distance_from_target = Vector3.Distance(enemy_mng.cur_target.transform.position, transform.position);

            int max_score = 0;

            for (int i = 0; i < enemy_atk_ations.Length; i++)
            {
                enemy_attack_ations en_atk_ations = enemy_atk_ations[i];

                if (distance_from_target <= en_atk_ations.max_distance_needed_to_atk &&
                    distance_from_target >= en_atk_ations.min_distance_needed_to_atk)
                {
                    if (viewable_angle <= en_atk_ations.max_distance_needed_to_atk &&
                        viewable_angle >= en_atk_ations.min_distance_needed_to_atk)
                    {
                        max_score += en_atk_ations.attack_score;
                    }
                }
            }

            int random_value = Random.Range(0, max_score);
            int temp_score = 0;

            for (int i = 0; i < enemy_atk_ations.Length; i++)
            {
                enemy_attack_ations en_atk_ations = enemy_atk_ations[i];

                if (distance_from_target <= en_atk_ations.max_distance_needed_to_atk &&
                    distance_from_target >= en_atk_ations.min_distance_needed_to_atk)
                {
                    if (viewable_angle <= en_atk_ations.max_distance_needed_to_atk &&
                        viewable_angle >= en_atk_ations.min_distance_needed_to_atk)
                    {
                        if (cur_atk_ations != null)
                        {
                            return;
                        }

                        temp_score += en_atk_ations.attack_score;
                        if (temp_score > random_value)
                        {
                            cur_atk_ations = en_atk_ations;
                        }
                    }
                }
            }

        }
    }
}
