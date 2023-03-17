using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace sg
{
    public class weapon_inventory_slot : MonoBehaviour
    {
        Image icon;
        weapon_item w_item;

        public void add_item(weapon_item new_item)
        {
            w_item = new_item;
            icon.sprite = w_item.item_icon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void clear_inventory_slot()
        {
            w_item = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }


    }
}
