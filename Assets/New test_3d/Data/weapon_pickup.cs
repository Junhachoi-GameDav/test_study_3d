using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace sg
{
    public class weapon_pickup : interactable
    {
        public weapon_item weapon;

        public override void interact(player_manager player_m)
        {
            base.interact(player_m);

            pickup_item(player_m);
        }

        private void pickup_item(player_manager player_m)
        {
            player_inventory player_Inventory;
            player_locomotion player_Locomotion;
            animater_handler anime_h;


            player_Inventory = player_m.GetComponent<player_inventory>();
            player_Locomotion = player_m.GetComponent<player_locomotion>();
            anime_h = player_m.GetComponentInChildren<animater_handler>();

            player_Locomotion.rigid.velocity = Vector3.zero; // ���� ������ �ֿﶧ
            anime_h.player_target_animation("pick_up_item", true);
            player_Inventory.weapon_inventory.Add(weapon);
            player_m.item_Ui_obj.GetComponentInChildren<Text>().text = weapon.item_name;
            player_m.item_Ui_obj.GetComponentInChildren<RawImage>().texture = weapon.item_icon.texture;
            player_m.item_Ui_obj.SetActive(true);

            Destroy(gameObject);
        }
    }
}

