using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace sg
{
    public class enemy_locomotion_manager : MonoBehaviour
    {
        enemy_manager enemy_mng;
        enemy_animation_manager en_anime_mng;

        NavMeshAgent navmeshagent;

        public character_stats cur_target;
        public LayerMask detection_layer;

        public float distance_from_target;
        public float stopping_distance = 0.5f;

        private void Awake()
        {
            enemy_mng = GetComponent<enemy_manager>();
            en_anime_mng = GetComponent<enemy_animation_manager>();
            navmeshagent = GetComponent<NavMeshAgent>();
        }
        public void handle_detection()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemy_mng.detection_radius, detection_layer);
            
            for (int i = 0; i < colliders.Length; i++)
            {
                character_stats ch_stats = colliders[i].transform.GetComponent<character_stats>();

                if(ch_stats != null)
                {
                    //

                    Vector3 target_dir = ch_stats.transform.position - transform.position;
                    float viewable_angle = Vector3.Angle(target_dir, transform.forward);

                    if(viewable_angle > enemy_mng.min_detection_angle && viewable_angle < enemy_mng.max_detection_angle)
                    {
                        cur_target = ch_stats;
                    }
                }
            }
        }

        public void handle_move_to_target()
        {
            Vector3 target_dir = cur_target.transform.position - transform.position;
            distance_from_target = Vector3.Distance(cur_target.transform.position, transform.position);
            float viewable_angle = Vector3.Angle(target_dir, transform.forward);

            // 행동을 취할시, 움직임 멈춤
            if (enemy_mng.is_preforming_action)
            {
                en_anime_mng.anime.SetFloat("vertical", 0, 0.1f, Time.deltaTime);
                navmeshagent.enabled = false;
            }
            else
            {
                if (distance_from_target > stopping_distance)
                {
                    en_anime_mng.anime.SetFloat("vertical", 1, 0.1f, Time.deltaTime);
                }
            }
        }
    }
}

