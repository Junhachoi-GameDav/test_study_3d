using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace sg
{
    [CreateAssetMenu(menuName = "Items/Weapons Item")]
    public class weapon_item : item
    {
        public GameObject model_prefab;
        public bool is_unarmed;

        [Header("idle_animations")]
        public string right_hand_idle;
        public string left_hand_idle;

        [Header("one handed attack animations")]
        public string o_h_light_atk_1;
        public string o_h_light_atk_2;
        public string o_h_light_atk_3;
        public string o_h_heavy_atk_1;
    }
}

