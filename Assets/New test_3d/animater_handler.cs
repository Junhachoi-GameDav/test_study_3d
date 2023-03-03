using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class animater_handler : MonoBehaviour
    {
        public Animator anime;
        int vertical;
        int horizontal;
        public bool can_rotate;

        public void initialize()
        {
            anime = GetComponent<Animator>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");


        }

        public void updete_animation_value(float vertical_movement, float horizontal_movement)
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
            else if(vertical_movement < 0 && vertical_movement < -0.55f)
            {
                v = -0.5f;
            }
            else if(vertical_movement > -0.55f)
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
            else if (horizontal_movement < 0 && horizontal_movement < -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontal_movement > -0.55f)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            #endregion

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
    }
}
