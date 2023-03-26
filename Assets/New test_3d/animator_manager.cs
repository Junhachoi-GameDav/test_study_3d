using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class animator_manager : MonoBehaviour
    {
        public Animator anime;
        
        public void player_target_animation(string target_anime, bool is_interacting)
        {
            anime.applyRootMotion = is_interacting;
            anime.SetBool("is_interacting", is_interacting);
            anime.CrossFade(target_anime, 0.2f);
        }
    }
}

