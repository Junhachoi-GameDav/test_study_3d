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

        ui_handle_equitment_slot[] ui_handle_e_slot;

        private void Awake()
        {
            ui_handle_e_slot = GetComponentsInChildren<ui_handle_equitment_slot>();
        }
        
        public void load_weapons_on_equipment_screen(player_inventory player_Inve)
        {

            foreach (var item in ui_handle_e_slot)
            {
                if (item.right_hand_slot_1)
                {
                    item.add_itme(player_Inve.weapons_in_right_hand_slots[0]);
                }
                /*
                    else if(ui_handle_e_slot[i].right_hand_slot_2)
                    {
                        ui_handle_e_slot[i].add_itme(player_Inve.weapons_in_right_hand_slots[1]);
                    }
                    else if (ui_handle_e_slot[i].left_hand_slot_1)
                    {
                        ui_handle_e_slot[i].add_itme(player_Inve.weapons_in_left_hand_slots[0]);
                    }
                    else
                    {
                        ui_handle_e_slot[i].add_itme(player_Inve.weapons_in_left_hand_slots[1]);
                    }
                    */
            }
        }
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
