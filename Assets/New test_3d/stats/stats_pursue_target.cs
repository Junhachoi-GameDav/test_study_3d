using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class stats_pursue_target : stats
    {

        public stats_combet_stance stats_co_stance;

        public override stats tick(enemy_manager enemy_mng, enemy_stats en_stats, enemy_animation_manager en_anime_mng)
        {
            //chase the target
            //if within atk range, etc
            if (enemy_mng.is_preforming_action)
            {
                return this;
            }
            Vector3 target_dir = enemy_mng.cur_target.transform.position - transform.position;
            enemy_mng.distance_from_target = Vector3.Distance(enemy_mng.cur_target.transform.position, transform.position);
            float viewable_angle = Vector3.Angle(target_dir, transform.forward);

            // 행동을 취할시, 움직임 멈춤
            if (enemy_mng.distance_from_target > enemy_mng.max_attack_range)
            {
                en_anime_mng.anime.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            }
            

            handle_rotate_towards_target(enemy_mng);

            //transform.position = new Vector3(transform.position.x, navmeshagent.transform.position.y, transform.position.z);
            enemy_mng.navmeshagent.transform.localPosition = Vector3.zero;
            enemy_mng.navmeshagent.transform.localRotation = Quaternion.identity;

            if(enemy_mng.distance_from_target <= enemy_mng.max_attack_range)
            {
                return stats_co_stance;
            }
            else
            {
                return this;
            }
        }

        private void handle_rotate_towards_target(enemy_manager enemy_mng)
        {
            // 수동적이게 회전
            if (enemy_mng.is_preforming_action)
            {
                Vector3 dir = enemy_mng.cur_target.transform.position - transform.position;
                dir.y = 0;
                dir.Normalize();

                if (dir == Vector3.zero)
                {
                    dir = transform.forward;
                }

                Quaternion target_rotation = Quaternion.LookRotation(dir);
                enemy_mng.transform.rotation = Quaternion.Slerp(transform.rotation, target_rotation, enemy_mng.rotation_speed / Time.deltaTime);
            }
            // 네비 따라서 회전
            else
            {
                Vector3 relative_dir = transform.InverseTransformDirection(enemy_mng.navmeshagent.desiredVelocity);
                Vector3 target_velocity = enemy_mng.en_rigid.velocity;

                enemy_mng.navmeshagent.enabled = true;
                enemy_mng.navmeshagent.SetDestination(enemy_mng.cur_target.transform.position);
                enemy_mng.en_rigid.velocity = target_velocity;

                enemy_mng.transform.rotation = Quaternion.Slerp(transform.rotation, 
                    enemy_mng.navmeshagent.transform.rotation, enemy_mng.rotation_speed / Time.deltaTime);
            }
        }
    }
}

