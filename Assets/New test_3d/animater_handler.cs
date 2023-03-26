using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class animater_handler : animator_manager
    {
        player_manager player_mng;

        input_handler input_h;
        player_locomotion player_lo;
        int vertical;
        int horizontal;
        public bool can_rotate;

        public void initialize()
        {
            player_mng = GetComponentInParent<player_manager>();
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


        public void canrotate()
        {
            can_rotate = true;
        }
        public void stoprotate()
        {
            can_rotate = false;
        }

        public void enable_combo()
        {
            anime.SetBool("can_combo", true);
        }
        public void disable_combo()
        {
            anime.SetBool("can_combo", false);
        }

        private void OnAnimatorMove() //애니메이션이랑 같이 움직임
        {
            if(player_mng.is_interacting == false)
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
