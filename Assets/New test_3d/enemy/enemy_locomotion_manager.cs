using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class enemy_locomotion_manager : MonoBehaviour
    {
        enemy_manager enemy_mng;

        public character_stats cur_target;
        public LayerMask detection_layer;

        private void Awake()
        {
            enemy_mng = GetComponent<enemy_manager>();
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
    }
}

