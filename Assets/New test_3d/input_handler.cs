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
    }

}
