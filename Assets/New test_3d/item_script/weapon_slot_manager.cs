using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class weapon_slot_manager : MonoBehaviour
    {
        weapon_holder_slot left_hand_slot;
        weapon_holder_slot right_hand_slot;

        private void Awake()
        {
            weapon_holder_slot[] w_holder_slots = GetComponentsInChildren<weapon_holder_slot>();

            foreach (weapon_holder_slot w_slots in w_holder_slots)
            {
                if (w_slots.is_left_hand_slot)
                {
                    left_hand_slot = w_slots;
                }
                else if (w_slots.is_right_hand_slot)
                {
                    right_hand_slot = w_slots;
                }
            }
        }

        public void load_weapon_on_slot(weapon_item w_item, bool is_left)
        {
            if (is_left)
            {
                left_hand_slot.load_weapon_model(w_item);
            }
            else
            {
                right_hand_slot.load_weapon_model(w_item);
            }
        }
    }
}

