using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace sg
{
    public class player_manager : MonoBehaviour
    {
        input_handler input_h;
        Animator anime;
        camera_handler cam_handler;
        player_locomotion player_lo;

        public bool is_interacting;

        [Header("Player Flags")]
        public bool is_sprinting;
        public bool is_in_air;
        public bool is_ground;

        float delta;

        private void Awake()
        {
            cam_handler = FindObjectOfType<camera_handler>();
        }
        void Start()
        {
            input_h = GetComponent<input_handler>();
            anime = GetComponentInChildren<Animator>();
            player_lo = GetComponent<player_locomotion>();
        }

        void Update()
        {
            delta = Time.deltaTime;
            
            is_interacting = anime.GetBool("is_interacting");

            input_h.tick_input(delta);
            player_lo.handle_movement(delta);
            player_lo.handle_rolling_sprinting(delta);
            player_lo.handle_falling(delta, player_lo.move_dir);
        }
        private void FixedUpdate()
        {
            float delta = Time.deltaTime;

            if (cam_handler != null)
            {
                cam_handler.follow_target(delta);
                cam_handler.handle_camera_rotation(delta, input_h.mouse_x, input_h.mouse_y);
            }
        }
        private void LateUpdate()
        {
           
            input_h.roll_flag = false;
            input_h.sprint_flag = false;
            input_h.r_b_input = false;
            input_h.r_t_input = false;

            if (is_in_air)
            {
                player_lo.in_air_timer = player_lo.in_air_timer + Time.deltaTime;
            }
        }
    }
}

