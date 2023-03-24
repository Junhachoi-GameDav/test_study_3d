using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace sg
{
    public class ui_handle_equitment_slot : MonoBehaviour
    {
        ui_manager ui_mng;
        public Image icon;
        weapon_item weapon;

        public bool right_hand_slot_01;
        public bool right_hand_slot_02;
        public bool left_hand_slot_01;
        public bool left_hand_slot_02;


        private void Awake()
        {
            ui_mng = FindObjectOfType<ui_manager>();
        }

        public void add_itme(weapon_item new_weapon)
        {
            weapon = new_weapon;
            icon.sprite = weapon.item_icon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void clear_itme()
        {
            weapon = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public void select_this_slot()
        {
            if (right_hand_slot_01)
            {
                ui_mng.right_hand_slot_01_selected = true;
            }
            else if (right_hand_slot_01)
            {
                ui_mng.right_hand_slot_02_selected = true;
            }
            else if (left_hand_slot_01)
            {
                ui_mng.left_hand_slot_01_selected = true;
            }
            else
            {
                ui_mng.left_hand_slot_02_selected = true;
            }
        }
    }
}

