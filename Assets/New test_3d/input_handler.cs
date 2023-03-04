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
        public bool roll_flag;

        Player_controller inputActions;

        camera_handler cam_handler;

        Vector2 movement_input;
        Vector2 camera_input;

        private void Awake()
        {
            cam_handler = camera_handler.cam_singleton;
        }

        private void FixedUpdate()
        {
            float delta = Time.deltaTime;

            if(cam_handler != null)
            {
                cam_handler.follow_target(delta);
                cam_handler.handle_camera_rotation(delta, mouse_x, mouse_y);
            }
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
            b_input = inputActions.playeractions.roll.triggered; //최신버전은 이것으로 해야함
            if (b_input)
            {
                roll_flag = true;
            }
        }
    }

}
