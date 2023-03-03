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

        Player_controller inputActions;

        Vector2 movement_input;
        Vector2 camera_input;

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
        }
        private void move_input(float delta)
        {
            horizontal = movement_input.x;
            vertical = movement_input.y;
            move_amount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

            mouse_x = camera_input.x;
            mouse_y = camera_input.y;
        }
    }

}
