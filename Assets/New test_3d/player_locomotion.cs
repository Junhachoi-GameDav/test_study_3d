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

        public Vector3 move_dir;

        [HideInInspector]
        public Transform my_transform;
        [HideInInspector]
        public animater_handler animater_h;

        public new Rigidbody rigid;
        public GameObject normal_camera;
        [Header("Ground & Air Detection Stats")]
        [SerializeField]
        float ground_detection_ray_start_point =0.5f;
        [SerializeField]
        float min_Distance_needed_to_begin_fall = 1f;
        [SerializeField]
        float ground_direction_ray_distance = 0.2f;
        LayerMask ignore_for_ground_check;
        public float in_air_timer;


        [Header("Movement Stats")]
        [SerializeField]
        float movement_speed = 5f;
        [SerializeField]
        float sprint_speed = 7f;
        [SerializeField]
        float rotation_speed = 10f;
        [SerializeField]
        float falling_speed = 45;


        void Start()
        {
            player_mng = GetComponent<player_manager>();
            rigid = GetComponent<Rigidbody>();
            input_h = GetComponent<input_handler>();
            animater_h = GetComponentInChildren<animater_handler>();
            camera_obj = Camera.main.transform;
            my_transform = transform;
            animater_h.initialize();

            player_mng.is_ground = true;
            ignore_for_ground_check = ~(1 << 8 | 1 << 11);


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
            if (player_mng.is_interacting)
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
                    animater_h.player_target_animation("step_back", true);
                }
            }
        }

        public void handle_falling(float delta, Vector3 move_dir)
        {
            player_mng.is_ground = false;
            RaycastHit hit;
            Vector3 origin = my_transform.position;
            origin.y += ground_detection_ray_start_point;

            if(Physics.Raycast(origin, my_transform.forward, out hit, 0.4f))
            {
                move_dir = Vector3.zero;
            }

            if (player_mng.is_in_air)
            {
                rigid.AddForce(-Vector3.up * falling_speed);
                rigid.AddForce(move_dir * falling_speed / 5f);
            }

            Vector3 dir = move_dir;
            dir.Normalize();
            origin = origin + dir * ground_direction_ray_distance;

            target_position = my_transform.position;

            Debug.DrawRay(origin, -Vector3.up * min_Distance_needed_to_begin_fall, Color.red, 0.1f, false);
            if(Physics.Raycast(origin, -Vector3.up, out hit, min_Distance_needed_to_begin_fall, ignore_for_ground_check))
            {
                normal_vector = hit.normal;
                Vector3 t_p = hit.point;
                player_mng.is_ground = true;
                target_position.y = t_p.y;

                if (player_mng.is_in_air)
                {
                    if(in_air_timer > 0.5f)
                    {
                        Debug.Log("공중에 있었음" + in_air_timer);
                        animater_h.player_target_animation("JumpLand", true);
                        in_air_timer = 0;
                    }
                    else
                    {
                        animater_h.player_target_animation("Blend Tree", false);
                        in_air_timer = 0;
                    }
                    player_mng.is_in_air = false;
                }
            }
            else
            {
                if (player_mng.is_ground)
                {
                    player_mng.is_ground = false;
                }

                if (player_mng.is_in_air == false)
                {
                    if (player_mng.is_interacting == false)
                    {
                        animater_h.player_target_animation("falling", true);
                    }

                    Vector3 vel = rigid.velocity;
                    vel.Normalize();
                    rigid.velocity = vel * (movement_speed / 2);
                    player_mng.is_in_air = true;
                }
            }

            if (player_mng.is_ground)
            {
                /*
                if(player_mng.is_interacting || input_h.move_amount > 0)
                {
                    my_transform.position = Vector3.Lerp(my_transform.position, target_position, Time.deltaTime);
                }
                else
                {
                    my_transform.position = target_position;
                }*/
                my_transform.position = target_position;
            }
        }
        #endregion
    }
}

