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
        public Rigidbody en_rigid;
        NavMeshAgent navmeshagent;

        public character_stats cur_target;
        public LayerMask detection_layer;

        public float distance_from_target;
        public float stopping_distance = 1f;

        public float rotation_speed = 15;
        private void Awake()
        {
            enemy_mng = GetComponent<enemy_manager>();
            en_anime_mng = GetComponent<enemy_animation_manager>();
            en_rigid = GetComponent<Rigidbody>();
            navmeshagent = GetComponentInChildren<NavMeshAgent>();
        }
        private void Start()
        {
            navmeshagent.enabled = false;
            en_rigid.isKinematic = false;
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
            if (enemy_mng.is_preforming_action)
            {
                return;
            }
            Vector3 target_dir = cur_target.transform.position - transform.position;
            distance_from_target = Vector3.Distance(cur_target.transform.position, transform.position);
            float viewable_angle = Vector3.Angle(target_dir, transform.forward);

            // 행동을 취할시, 움직임 멈춤
            if (enemy_mng.is_preforming_action)
            {
                en_anime_mng.anime.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                navmeshagent.enabled = false;
            }
            else
            {
                if (distance_from_target > stopping_distance)
                {
                    en_anime_mng.anime.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                }
                else if(distance_from_target <= stopping_distance)
                {
                    en_anime_mng.anime.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                }
            }

            handle_rotate_towards_target();

            //transform.position = new Vector3(transform.position.x, navmeshagent.transform.position.y, transform.position.z);
            navmeshagent.transform.localPosition = Vector3.zero;
            navmeshagent.transform.localRotation = Quaternion.identity;
        }

        private void handle_rotate_towards_target()
        {
            // 수동적이게 회전
            if (enemy_mng.is_preforming_action)
            {
                Vector3 dir = cur_target.transform.position - transform.position;
                dir.y = 0;
                dir.Normalize();

                if(dir == Vector3.zero)
                {
                    dir = transform.forward;
                }

                Quaternion target_rotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, target_rotation, rotation_speed / Time.deltaTime);
            }
            // 네비 따라서 회전
            else
            {
                Vector3 relative_dir = transform.InverseTransformDirection(navmeshagent.desiredVelocity);
                Vector3 target_velocity = en_rigid.velocity;

                navmeshagent.enabled = true;
                navmeshagent.SetDestination(cur_target.transform.position);
                en_rigid.velocity = target_velocity;

                transform.rotation = Quaternion.Slerp(transform.rotation, navmeshagent.transform.rotation, rotation_speed / Time.deltaTime);
            }
        }
    }
}

