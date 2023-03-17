using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class ui_equipment : MonoBehaviour
    {
        public bool right_hand_slot_1_selected;
        public bool right_hand_slot_2_selected;
        public bool left_hand_slot_1_selected;
        public bool left_hand_slot_2_selected;

        public void select_right_hand_slot_1()
        {
            right_hand_slot_1_selected = true;
        }
        public void select_right_hand_slot_2()
        {
            right_hand_slot_2_selected = true;
        }
        public void select_left_hand_slot_1()
        {
            left_hand_slot_1_selected = true;
        }
        public void select_left_hand_slot_2()
        {
            left_hand_slot_2_selected = true;
        }
    }
}
