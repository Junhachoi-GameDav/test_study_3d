using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class weapon_holder_slot : MonoBehaviour
    {

        public Transform parent_override;
        public bool is_left_hand_slot;
        public bool is_right_hand_slot;

        public GameObject current_weapon_model;

        public void unload_weapon()
        {
            if(current_weapon_model != null)
            {
                current_weapon_model.SetActive(false);
            }
        }

        public void unload_weapon_and_destroy()
        {
            if(current_weapon_model != null)
            {
                Destroy(current_weapon_model);
            }
        }

        public void load_weapon_model(weapon_item w_item)
        {

            unload_weapon_and_destroy();

            if (w_item == null)
            {
                unload_weapon();
                return;
            }

            GameObject model = Instantiate(w_item.model_prefab) as GameObject;
            if(model != null)
            {
                if(parent_override != null)
                {
                    model.transform.parent = parent_override;
                }
                else
                {
                    model.transform.parent = transform;
                }

                //Á¦ÀÚ¸®
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }

            current_weapon_model = model;
        }
    }
}

