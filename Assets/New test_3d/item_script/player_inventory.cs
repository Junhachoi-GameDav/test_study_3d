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

        private void Awake()
        {
            w_slot_mng = GetComponentInChildren<weapon_slot_manager>();
        }

        private void Start()
        {
            w_slot_mng.load_weapon_on_slot(right_weapon, false);
            w_slot_mng.load_weapon_on_slot(left_weapon, true);
        }
    }
}