using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class ui_manager : MonoBehaviour
    {
        public GameObject select_window;

        public GameObject weapon_inve_slot_prefab;
        public Transform weapon_inve_slot_parent;
        weapon_inventory_slot[] weapon_Inve_slots;

        public void update_ui()
        {
            #region weapon inventory slots

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
    }
}

