using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class player_inventory : MonoBehaviour
    {
        weapon_slot_manager w_slot_mng;
        public weapon_item right_weapon;
        public weapon_item left_weapon;

        public weapon_item heavy_weapon_weapon;
        public weapon_item unarmed_weapon;


        public weapon_item[] weapons_in_right_hand_slots = new weapon_item[1];
        public weapon_item[] weapons_in_left_hand_slots = new weapon_item[1];

        public GameObject sword;

        public int cur_right_weapon_index = -1;
        public int cur_left_weapon_index = -1;

        public List<weapon_item> weapon_inventory;
        
        private void Awake()
        {
            w_slot_mng = GetComponentInChildren<weapon_slot_manager>();
            
        }

        private void Start()
        {
            
            right_weapon = weapons_in_right_hand_slots[cur_right_weapon_index];
            left_weapon = weapons_in_left_hand_slots[cur_left_weapon_index];
            w_slot_mng.load_weapon_on_slot(right_weapon, false);
            w_slot_mng.load_weapon_on_slot(left_weapon, true);
            
            //right_weapon = unarmed_weapon;
            //left_weapon = unarmed_weapon;
            sword.SetActive(false);
        }

        public void change_right_weapon()
        {
            cur_right_weapon_index = cur_right_weapon_index + 1;
            
            if (cur_right_weapon_index > weapons_in_right_hand_slots.Length - 1)
            {
                cur_right_weapon_index = -1;
                right_weapon = unarmed_weapon;
                w_slot_mng.load_weapon_on_slot(unarmed_weapon, false);
                sword.SetActive(true);
            }
            else if (weapons_in_right_hand_slots[cur_right_weapon_index] != null)
            {
                
                right_weapon = weapons_in_right_hand_slots[cur_right_weapon_index];
                w_slot_mng.load_weapon_on_slot(weapons_in_right_hand_slots[cur_right_weapon_index], false);
                sword.SetActive(false);
            }
            else
            {
                cur_right_weapon_index = cur_right_weapon_index + 1;
            }
        }
        public void pick_down_weapon()
        {
            sword.SetActive(true);
        }
        public void pick_up_weapon()
        {
            sword.SetActive(false);
        }
        public void change_left_weapon()
        {
            cur_left_weapon_index = cur_left_weapon_index + 1;
            
            if (cur_left_weapon_index > weapons_in_left_hand_slots.Length - 1)
            {
                cur_left_weapon_index = -1;
                left_weapon = unarmed_weapon;
                w_slot_mng.load_weapon_on_slot(unarmed_weapon, false);
                //sword.SetActive(true);
            }
            else if (weapons_in_left_hand_slots[cur_left_weapon_index] != null)
            {
                left_weapon = weapons_in_left_hand_slots[cur_left_weapon_index];
                //w_slot_mng.load_weapon_on_slot(left_weapon, true);
                w_slot_mng.load_weapon_on_slot(weapons_in_left_hand_slots[cur_left_weapon_index], false);
                //sword.SetActive(false);
            }
            else
            {
                cur_left_weapon_index = cur_left_weapon_index + 1;
            }

        }
    }
}