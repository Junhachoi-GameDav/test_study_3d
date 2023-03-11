using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace sg
{
    public class quick_slot_ui : MonoBehaviour
    {
        public Image left_weapon_icon;
        public Image right_weapon_icon;

        public void update_weapon_quick_slots_ui(bool is_left, weapon_item weapon)
        {
            if (!is_left)
            {
                if(weapon.item_icon != null)
                {
                    right_weapon_icon.sprite = weapon.item_icon;
                    right_weapon_icon.enabled = true;
                }
                else
                {
                    right_weapon_icon.sprite = null;
                    right_weapon_icon.enabled = false;
                }
                
            }
            else
            {
                if (weapon.item_icon != null)
                {
                    left_weapon_icon.sprite = weapon.item_icon;
                    left_weapon_icon.enabled = true;
                }
                else
                {
                    left_weapon_icon.sprite = null;
                    left_weapon_icon.enabled = false;
                }
            }
        }
    }
}

