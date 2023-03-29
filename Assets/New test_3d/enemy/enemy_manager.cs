using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace sg
{
    public class enemy_manager : character_manager
    {
        enemy_locomotion_manager enemy_Locomotion_mng;
        enemy_animation_manager en_animaion_mng;
        enemy_stats en_stats;

        public NavMeshAgent navmeshagent;
        public Rigidbody en_rigid;
        public stats cur_stats;
        public character_stats cur_target;

        public bool is_preforming_action;
        public float distance_from_target;
        public float rotation_speed = 15;
        public float max_attack_range = 1.5f;

        [Header("A.I settings")]
        public float detection_radius =20;
        //눈으로 보듯 = 뒤에있으면 안보이듯
        public float max_detection_angle = 50;
        public float min_detection_angle = -50;
        public float viewable_angle;

        public float cur_recovery_time = 0;

        private void Awake()
        {
            enemy_Locomotion_mng = GetComponent<enemy_locomotion_manager>();
            en_animaion_mng = GetComponent<enemy_animation_manager>();
            en_stats = GetComponent<enemy_stats>();
            navmeshagent = GetComponentInChildren<NavMeshAgent>();
            en_rigid = GetComponent<Rigidbody>();
            navmeshagent.enabled = false;
        }
        private void Start()
        {
            en_rigid.isKinematic = false;
        }
        private void Update()
        {
            handle_recovery_time();
        }
        private void FixedUpdate()
        {
            handle_stats_machine();
            
        }
        private void handle_stats_machine()
        {
            if(cur_stats != null)
            {
                stats next_stats = cur_stats.tick(this, en_stats, en_animaion_mng);

                if(next_stats != null)
                {
                    switch_to_next_stats(next_stats);
                }
            }
            /*
            if(enemy_Locomotion_mng.cur_target != null)
            {
                enemy_Locomotion_mng.distance_from_target =
                    Vector3.Distance(enemy_Locomotion_mng.cur_target.transform.position, transform.position);
            }
            
            if(enemy_Locomotion_mng.cur_target == null)
            {
                enemy_Locomotion_mng.handle_detection();
            }
            else if(enemy_Locomotion_mng.distance_from_target >= enemy_Locomotion_mng.stopping_distance)
            {
                enemy_Locomotion_mng.handle_move_to_target();
            }
            else if(enemy_Locomotion_mng.distance_from_target <= enemy_Locomotion_mng.stopping_distance)
            {
                attack_target();
            }
            */
        }

        private void switch_to_next_stats(stats _stats)
        {
            cur_stats = _stats;
        }

        private void handle_recovery_time()
        {
            if(cur_recovery_time > 0)
            {
                cur_recovery_time -= Time.deltaTime;
            }

            if (is_preforming_action)
            {
                if(cur_recovery_time <= 0)
                {
                    is_preforming_action = false;
                }
            }
        }
        #region attacks
        private void attack_target()
        {
            /*
            if (is_preforming_action)
            {
                return;
            }

            if(cur_attack == null)
            {
                get_new_attack();
            }
            else
            {
                is_preforming_action = true;
                cur_recovery_time = cur_attack.recovery_time;
                en_animaion_mng.player_target_animation(cur_attack.action_animation, true);
                cur_attack = null;
            }
            */
        }
        private void get_new_attack()
        {
            /*
            Vector3 target_dir = enemy_Locomotion_mng.cur_target.transform.position - transform.position;
            float viewable_angle = Vector3.Angle(target_dir, transform.forward);
            enemy_Locomotion_mng.distance_from_target = Vector3.Distance(enemy_Locomotion_mng.cur_target.transform.position, transform.position);

            int max_score = 0;

            for (int i = 0; i < enemy_atk_ations.Length; i++)
            {
                enemy_attack_ations en_atk_ations = enemy_atk_ations[i];

                if(enemy_Locomotion_mng.distance_from_target <= en_atk_ations.max_distance_needed_to_atk &&
                    enemy_Locomotion_mng.distance_from_target >= en_atk_ations.min_distance_needed_to_atk)
                {
                    if(viewable_angle <= en_atk_ations.max_distance_needed_to_atk &&
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

                if (enemy_Locomotion_mng.distance_from_target <= en_atk_ations.max_distance_needed_to_atk &&
                    enemy_Locomotion_mng.distance_from_target >= en_atk_ations.min_distance_needed_to_atk)
                {
                    if (viewable_angle <= en_atk_ations.max_distance_needed_to_atk &&
                        viewable_angle >= en_atk_ations.min_distance_needed_to_atk)
                    {
                        if(cur_attack != null)
                        {
                            return;
                        }

                        temp_score += en_atk_ations.attack_score;
                        if(temp_score > random_value)
                        {
                            cur_attack = en_atk_ations;
                        }
                    }
                }
            }
            */
        }
            
        #endregion

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detection_radius);
            Vector3 fovLine1 = Quaternion.AngleAxis(max_detection_angle, transform.up) * transform.forward * detection_radius;
            Vector3 fovLine2 = Quaternion.AngleAxis(min_detection_angle, transform.up) * transform.forward * detection_radius;
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, fovLine1);
            Gizmos.DrawRay(transform.position, fovLine2);
        }
    }
}

