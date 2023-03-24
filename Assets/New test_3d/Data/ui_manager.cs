using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class ui_manager : MonoBehaviour
    {
        public player_inventory p_inventory;
        public ui_equipment equipment_window;

        [Header("ui windows")]
        public GameObject hud_window;
        public GameObject select_window;
        public GameObject weapon_inve_window;
        public GameObject equipment_inve_window;


        [Header("weapon inventory")]
        public GameObject weapon_inve_slot_prefab;
        public Transform weapon_inve_slot_parent;
        weapon_inventory_slot[] weapon_Inve_slots;

        private void Awake()
        {
            
        }
        private void Start()
        {
            weapon_Inve_slots = weapon_inve_slot_parent.GetComponentsInChildren<weapon_inventory_slot>();
            equipment_window.load_weapons_on_equipment_screen(p_inventory);
            equipment_inve_window.SetActive(false);
        }
        public void update_ui()
        {
            #region weapon inventory slots
            for (int i = 0; i < weapon_Inve_slots.Length; i++)
            {
                if(i < p_inventory.weapon_inventory.Count)
                {
                    if(weapon_Inve_slots.Length < p_inventory.weapon_inventory.Count)
                    {
                        Instantiate(weapon_inve_slot_prefab, weapon_inve_slot_parent);
                        weapon_Inve_slots = weapon_inve_slot_parent.GetComponentsInChildren<weapon_inventory_slot>();

                    }
                    weapon_Inve_slots[i].add_item(p_inventory.weapon_inventory[i]);
                }
                else
                {
                    weapon_Inve_slots[i].clear_inventory_slot();
                }
            }
            #endregion
        }
        public void open_select_window()
        {
            select_window.SetActive(true);
        }

        public void close_select_window()
        {
            select_window.SetActive(false);
        }

        public void close_all_inventory_windows()
        {
            weapon_inve_window.SetActive(false);
            equipment_inve_window.SetActive(false);
        }
    }
}

