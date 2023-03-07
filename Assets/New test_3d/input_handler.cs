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

        public bool b_input;
        public bool r_b_input;
        public bool r_t_input;

        public bool roll_flag;
        public bool sprint_flag;
        public float roll_input_timer;


        Player_controller inputActions;
        player_attack player_atk;
        player_inventory player_inve;

        Vector2 movement_input;
        Vector2 camera_input;

        private void Awake()
        {
            player_atk = GetComponent<player_attack>();
            player_inve = GetComponent<player_inventory>();
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
                player_atk.handle_light_atk(player_inve.right_weapon);
            }

            if (r_t_input)
            {
                player_atk.handle_heavy_atk(player_inve.right_weapon);
            }
        }
    }

}
