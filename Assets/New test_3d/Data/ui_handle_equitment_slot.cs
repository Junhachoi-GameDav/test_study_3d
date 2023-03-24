using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace sg
{
    public class ui_handle_equitment_slot : MonoBehaviour
    {
        public Image icon;
        weapon_item weapon;

        public bool right_hand_slot_01;
        public bool right_hand_slot_02;
        public bool left_hand_slot_01;
        public bool left_hand_slot_02;

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

            }
            else if (right_hand_slot_01)
            {

            }
            else if (left_hand_slot_01)
            {

            }
            else
            {

            }
        }
    }
}

