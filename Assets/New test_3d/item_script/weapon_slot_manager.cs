using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class weapon_slot_manager : MonoBehaviour
    {
        weapon_holder_slot left_hand_slot;
        weapon_holder_slot right_hand_slot;

        damage_collider left_h_dmg_collider;
        damage_collider right_h_dmg_collider;

        private void Awake()
        {
            weapon_holder_slot[] w_holder_slots = GetComponentsInChildren<weapon_holder_slot>();

            foreach (weapon_holder_slot w_slots in w_holder_slots)
            {
                if (w_slots.is_left_hand_slot)
                {
                    left_hand_slot = w_slots;
                    load_left_weapon_damage_collider();
                }
                else if (w_slots.is_right_hand_slot)
                {
                    right_hand_slot = w_slots;
                    load_right_weapon_damage_collider();
                }
            }
        }

        public void load_weapon_on_slot(weapon_item w_item, bool is_left)
        {
            if (is_left)
            {
                left_hand_slot.load_weapon_model(w_item);
                load_left_weapon_damage_collider();
            }
            else
            {
                right_hand_slot.load_weapon_model(w_item);
                load_right_weapon_damage_collider();
            }
        }


        #region handle weapon;s damage collider
        private void load_left_weapon_damage_collider()
        {
            left_h_dmg_collider = left_hand_slot.current_weapon_model.GetComponentInChildren<damage_collider>();

        }
        private void load_right_weapon_damage_collider()
        {
            right_h_dmg_collider = right_hand_slot.current_weapon_model.GetComponentInChildren<damage_collider>();
        }

        public void open_right_damage_collider()
        {
            right_h_dmg_collider.enable_damage_collider();
        }
        public void open_left_damage_collider()
        {
            left_h_dmg_collider.enable_damage_collider();
        }
        public void close_right_damage_collider()
        {
            right_h_dmg_collider.disable_damage_collider();
        }
        public void close_left_damage_collider()
        {
            left_h_dmg_collider.disable_damage_collider();
        }
        #endregion
    }
}

