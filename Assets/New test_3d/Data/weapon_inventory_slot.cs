using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace sg
{
    public class weapon_inventory_slot : MonoBehaviour
    {
        player_inventory player_inve;
        weapon_slot_manager w_s_mng;
        ui_manager ui_mng;

        public Image icon;
        weapon_item w_item;

        public void Awake()
        {
            player_inve = FindObjectOfType<player_inventory>();
            w_s_mng = FindObjectOfType<weapon_slot_manager>();
            ui_mng = FindObjectOfType<ui_manager>();
        }

        public void add_item(weapon_item new_item)
        {
            w_item = new_item;
            icon.sprite = w_item.item_icon;
            gameObject.SetActive(true);
            icon.enabled = true;
        }

        public void clear_inventory_slot()
        {
            w_item = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public void equip_this_itme()
        {
            if (ui_mng.right_hand_slot_01_selected)
            {
                player_inve.weapon_inventory.Add(player_inve.weapons_in_right_hand_slots[0]);
                player_inve.weapons_in_right_hand_slots[0] = w_item;
                player_inve.weapon_inventory.Remove(w_item);
                player_inve.right_weapon = player_inve.weapons_in_right_hand_slots[player_inve.cur_right_weapon_index];
            }
            else if (ui_mng.right_hand_slot_02_selected)
            {
                player_inve.weapon_inventory.Add(player_inve.weapons_in_right_hand_slots[1]);
                player_inve.weapons_in_right_hand_slots[1] = w_item;
                player_inve.weapon_inventory.Remove(w_item);

            }
            else if (ui_mng.left_hand_slot_01_selected)
            {
                player_inve.weapon_inventory.Add(player_inve.weapons_in_left_hand_slots[0]);
                player_inve.weapons_in_left_hand_slots[0] = w_item;
                player_inve.weapon_inventory.Remove(w_item);

            }
            else if(ui_mng.left_hand_slot_02_selected)
            {
                player_inve.weapon_inventory.Add(player_inve.weapons_in_left_hand_slots[1]);
                player_inve.weapons_in_left_hand_slots[1] = w_item;
                player_inve.weapon_inventory.Remove(w_item);

            }
            else
            {
                return;
            }

            player_inve.right_weapon = player_inve.weapons_in_right_hand_slots[player_inve.cur_right_weapon_index];
            player_inve.left_weapon = player_inve.weapons_in_left_hand_slots[player_inve.cur_left_weapon_index];

            w_s_mng.load_weapon_on_slot(player_inve.right_weapon, false);
            w_s_mng.load_weapon_on_slot(player_inve.left_weapon, true);

            ui_mng.equipment_window.load_weapons_on_equipment_screen(player_inve);
            ui_mng.reset_all_selected_slots();
        }
    }
}
