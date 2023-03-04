using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace sg
{
    public class player_manager : MonoBehaviour
    {
        input_handler input_h;
        Animator anime;

        void Start()
        {
            input_h = GetComponent<input_handler>();
            anime = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            input_h.is_interacting = anime.GetBool("is_interacting");
            input_h.roll_flag = false;
        }
    }
}

