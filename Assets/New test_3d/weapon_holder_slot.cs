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
    }
}

