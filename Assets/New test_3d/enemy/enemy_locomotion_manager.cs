using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace sg
{
    public class enemy_locomotion_manager : MonoBehaviour
    {
        enemy_manager enemy_mng;
        enemy_animation_manager en_anime_mng;
        
        

        
        public LayerMask detection_layer;

       
        private void Awake()
        {
            enemy_mng = GetComponent<enemy_manager>();
            en_anime_mng = GetComponent<enemy_animation_manager>();
        }
    }
}

