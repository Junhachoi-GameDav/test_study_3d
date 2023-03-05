using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class animater_handler : MonoBehaviour
    {
        public Animator anime;
        public input_handler input_h;
        public player_locomotion player_lo;
        int vertical;
        int horizontal;
        public bool can_rotate;

        public void initialize()
        {
            anime = GetComponent<Animator>();
            input_h = GetComponentInParent<input_handler>();
            player_lo = GetComponentInParent<player_locomotion>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");


        }

        public void updete_animation_value(float vertical_movement, float horizontal_movement, bool is_sprinting)
        {
            #region vertical
            float v = 0;
            if(vertical_movement > 0 && vertical_movement < 0.55f)
            {
                v = 0.5f;
            }
            else if(vertical_movement > 0.55f)
            {
                v = 1;
            }
            else if(vertical_movement < 0 && vertical_movement > -0.55f)
            {
                v = -0.5f;
            }
            else if(vertical_movement < -0.55f)
            {
                v = -1;
            }
            else
            {
                v = 0;
            }
            #endregion
            #region horizontal
            float h = 0;
            if (horizontal_movement > 0 && horizontal_movement < 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontal_movement > 0.55f)
            {
                h = 1;
            }
            else if (horizontal_movement < 0 && horizontal_movement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontal_movement < -0.55f)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            #endregion

            if (is_sprinting)
            {
                v = 2;
                h = horizontal_movement;
            }

            anime.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anime.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void player_target_animation(string target_anime, bool is_interacting)
        {
            anime.applyRootMotion = is_interacting;
            anime.SetBool("is_interacting", is_interacting);
            anime.CrossFade(target_anime, 0.2f);
        }

        public void canrotate()
        {
            can_rotate = true;
        }
        public void stoprotate()
        {
            can_rotate = false;
        }

        private void OnAnimatorMove() //애니메이션이랑 같이 움직임
        {
            if(input_h.is_interacting == false)
            {
                return;
            }

            float delta = Time.deltaTime;
            player_lo.rigid.drag = 0;
            Vector3 delta_position = anime.deltaPosition;
            delta_position.y = 0;
            Vector3 velocity = delta_position / delta;
            player_lo.rigid.velocity = velocity;
        }
    }
}
