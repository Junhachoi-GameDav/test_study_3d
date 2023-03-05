using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class player_locomotion : MonoBehaviour
    {
        player_manager player_mng;
        Transform camera_obj;
        input_handler input_h;

        Vector3 move_dir;

        [HideInInspector]
        public Transform my_transform;
        [HideInInspector]
        public animater_handler animater_h;

        public new Rigidbody rigid;
        public GameObject normal_camera;

        [Header("Movement Stats")]
        [SerializeField]
        float movement_speed = 5f;
        [SerializeField]
        float sprint_speed = 7f;
        [SerializeField]
        float rotation_speed = 10f;

        void Start()
        {
            player_mng = GetComponent<player_manager>();
            rigid = GetComponent<Rigidbody>();
            input_h = GetComponent<input_handler>();
            animater_h = GetComponentInChildren<animater_handler>();
            animater_h.initialize();

            camera_obj = Camera.main.transform;
            my_transform = transform;

        }

        #region movement
        Vector3 normal_vector;
        Vector3 target_position;

        private void handle_rotation(float delta)
        {
            Vector3 target_dir = Vector3.zero;

            float move_override = input_h.move_amount;

            target_dir = camera_obj.forward * input_h.vertical;
            target_dir += camera_obj.right * input_h.horizontal;

            target_dir.Normalize();
            target_dir.y = 0;

            if(target_dir == Vector3.zero)
            {
                target_dir = my_transform.forward;
            }

            float r_s = rotation_speed;
            Quaternion t_r = Quaternion.LookRotation(target_dir);
            Quaternion target_rotation = Quaternion.Slerp(my_transform.rotation, t_r, r_s * delta);

            my_transform.rotation = target_rotation;
        }

        public void handle_movement(float delta)
        {
            if (input_h.roll_flag)
            {
                return;
            }

            move_dir = camera_obj.forward * input_h.vertical;
            move_dir += camera_obj.right * input_h.horizontal;
            move_dir.Normalize();
            move_dir.y = 0;

            float speed = movement_speed;
            
            if (input_h.sprint_flag)
            {
                speed = sprint_speed;
                player_mng.is_sprinting = true;
                move_dir *= speed;
            }
            else
            {
                move_dir *= speed;
            }

            Vector3 projected_velocity = Vector3.ProjectOnPlane(move_dir, normal_vector);
            rigid.velocity = projected_velocity;


            animater_h.updete_animation_value(input_h.move_amount, 0, player_mng.is_sprinting);


            if (animater_h.can_rotate)
            {
                handle_rotation(delta);
            }
        }

        public void handle_rolling_sprinting(float delta)
        {
            if (animater_h.anime.GetBool("is_interacting"))
            {
                return;
            }
            
            if (input_h.roll_flag)
            {
                move_dir = camera_obj.forward * input_h.vertical;
                move_dir += camera_obj.right * input_h.horizontal;
                
                if(input_h.move_amount > 0) //rolling
                {
                    animater_h.player_target_animation("Rolling", true);
                    move_dir.y = 0;
                    Quaternion roll_rotation = Quaternion.LookRotation(move_dir);
                    my_transform.rotation = roll_rotation;
                }
                else // step back
                {
                    animater_h.player_target_animation("JumpLand", true);
                }
            }
        }
        #endregion
    }
}

