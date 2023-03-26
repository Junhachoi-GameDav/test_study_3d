using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class enemy_manager : character_manager
    {
        enemy_locomotion_manager enemy_Locomotion_mng;
        public bool is_preforming_action;

        [Header("A.I settings")]
        public float detection_radius =20;
        //눈으로 보듯 = 뒤에있으면 안보이듯
        public float max_detection_angle = 50;
        public float min_detection_angle = -50;

        private void Awake()
        {
            enemy_Locomotion_mng = GetComponent<enemy_locomotion_manager>();
        }
        private void Update()
        {
            
        }
        private void FixedUpdate()
        {
            handle_cur_action();
            
        }
        private void handle_cur_action()
        {
            if(enemy_Locomotion_mng.cur_target == null)
            {
                enemy_Locomotion_mng.handle_detection();
            }
            else
            {
                enemy_Locomotion_mng.handle_move_to_target();
            }
        }
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

