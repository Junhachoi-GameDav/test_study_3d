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
        interactable_ui interactable_Ui;

        public GameObject interactable_Ui_obj;

        public bool is_interacting;

        [Header("Player Flags")]
        public bool is_sprinting;
        public bool is_in_air;
        public bool is_ground;
        public bool can_combo;

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
            interactable_Ui = FindObjectOfType<interactable_ui>();
        }

        void Update()
        {
            delta = Time.deltaTime;
            
            is_interacting = anime.GetBool("is_interacting");
            can_combo = anime.GetBool("can_combo");


            input_h.tick_input(delta);
            player_lo.handle_movement(delta);
            player_lo.handle_rolling_sprinting(delta);
            player_lo.handle_falling(delta, player_lo.move_dir);

            check_for_interactable_object();
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
            input_h.d_pad_up = false;
            input_h.d_pad_down = false;
            input_h.d_pad_right = false;
            input_h.d_pad_left = false;
            input_h.a_input = false;

            if (is_in_air)
            {
                player_lo.in_air_timer = player_lo.in_air_timer + Time.deltaTime;
            }
        }

        public void check_for_interactable_object()
        {
            RaycastHit hit;

            if(Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cam_handler.ignore_layers))
            {
                if (hit.collider.tag == "interactable")
                {
                    interactable interactable_obj = hit.collider.GetComponent<interactable>();

                    if(interactable_obj != null)
                    {
                        string interactable_text = interactable_obj.interactable_text;
                        interactable_Ui.interactable_text.text = interactable_text;
                        interactable_Ui_obj.SetActive(true);
                        if (input_h.a_input)
                        {
                            hit.collider.GetComponent<interactable>().interact(this);
                        }
                    }
                }
            }
            else
            {
                if(interactable_Ui_obj != null)
                {
                    interactable_Ui_obj.SetActive(false);
                }
            }
        }
    }
}

