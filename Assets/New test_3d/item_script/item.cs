using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class item : ScriptableObject
    {
        [Header("Item Information")]
        public Sprite item_icon;
        public string item_name;
    }
}

