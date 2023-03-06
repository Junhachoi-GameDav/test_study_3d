using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace sg
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class weapon_item : item
    {
        public GameObject model_prefab;
        public bool is_unarmed;
    }
}

