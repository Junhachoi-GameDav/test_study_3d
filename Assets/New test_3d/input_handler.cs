using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class input_handler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float move_amount;
        public float mouse_x;
        public float mouse_y;

        public bool a_input;
        public bool b_input;
        public bool r_b_input;
        public bool r_t_input;
        public bool jump_input;
        public bool inventory_input;

        public bool d_pad_up;
        public bool d_pad_down;
        public bool d_pad_right;
        public bool d_pad_left;

        public bool q_input;

        public bool combo_flag;
        public bool roll_flag;
        public bool sprint_flag;
        public bool inventory_flag;
        public float roll_input_timer;


        Player_controller inputActions;
        player_attack player_atk;
        player_inventory player_inve;
        player_manager player_m;
        ui_manager ui_mng;

        Vector2 movement_input;
        Vector2 camera_input;

        private void Awake()
        {
            player_atk = GetComponent<player_attack>();
            player_inve = GetComponent<player_inventory>();
            player_m = GetComponent<player_manager>();
            ui_mng = FindObjectOfType<ui_manager>();
        }
        public void OnEnable()
        {
            if(inputActions == null)
            {
                inputActions = new Player_controller();
                inputActions.playermovement.movement.performed += inputActions => movement_input = inputActions.ReadValue<Vector2>();
                inputActions.playermovement.camera.performed += i => camera_input = i.ReadValue<Vector2>();
            }
            inputActions.Enable();
        }
        private void OnDisable()
        {
            inputActions.Disable();
        }
        public void tick_input(float delta)
        {
            move_input(delta);
            handle_rolling_input(delta);
            handle_attack_input(delta);
            handle_quick_slots_input();
            handle_interacting_button_input();
            handle_jump_input();
            handle_inventory_input();
        }
        private void move_input(float delta)
        {
            horizontal = movement_input.x;
            vertical = movement_input.y;
            move_amount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

            mouse_x = camera_input.x;
            mouse_y = camera_input.y;
        }

        private void handle_rolling_input(float delta)
        {
            //왼쪽 피연산자가 오른쪽 피연산자와 같으면 참, 다르면 거짓
            //b_input = inputActions.playeractions.roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            //b_input = inputActions.playeractions.roll.triggered; //최신버전은 이것으로 해야함___ 이건 트리거라서 한번만 된다.
            b_input = inputActions.playeractions.roll.IsPressed(); //또는 이것
            if (b_input)
            {
                roll_input_timer += delta;
                sprint_flag = true;
            }
            else
            {
                if(roll_input_timer >0 && roll_input_timer < 0.5f)
                {
                    sprint_flag = false;
                    roll_flag = true;
                }

                roll_input_timer = 0;
            }
        }

        private void handle_attack_input(float delta)
        {
            inputActions.playeractions.RB.performed += i => r_b_input = true;
            inputActions.playeractions.RT.performed += i => r_t_input = true;

            if (r_b_input)
            {
                if (player_m.can_combo)
                {
                    combo_flag = true;
                    player_atk.handle_weapon_combo(player_inve.right_weapon);
                    combo_flag = false;
                }
                else
                {
                    if (player_m.is_interacting || player_m.can_combo)
                    {
                        return;
                    }

                    player_atk.handle_light_atk(player_inve.right_weapon);
                }
            }

            if (r_t_input)
            {
                player_atk.handle_heavy_atk(player_inve.right_weapon);
            }
        }

        private void handle_quick_slots_input()
        {
            inputActions.playerquickslots.DPadRight.performed += i => d_pad_right = true;
            inputActions.playerquickslots.DPadLeft.performed += i => d_pad_left = true;
            if (d_pad_right)
            {
                player_inve.change_right_weapon();
            }
            else if (d_pad_left)
            {
                player_inve.change_left_weapon();
            }

        }

        private void handle_interacting_button_input()
        {
            inputActions.playeractions.A.performed += i => a_input = true;
        }

        private void handle_jump_input()
        {
            inputActions.playeractions.JUMP.performed += i => jump_input = true;
        }

        private void handle_inventory_input()
        {
            inputActions.playeractions.inventory.performed += i => inventory_input = true;

            if (inventory_input)
            {
                inventory_flag = !inventory_flag;

                if (inventory_flag)
                {
                    ui_mng.open_select_window();
                    ui_mng.update_ui();
                    ui_mng.hud_window.SetActive(false);
                }
                else
                {
                    ui_mng.close_select_window();
                    ui_mng.close_all_inventory_windows();
                    ui_mng.hud_window.SetActive(true);

                }
            }
        }
    }

}
