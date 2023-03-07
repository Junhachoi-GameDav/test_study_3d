using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace sg
{
    public class health_bar : MonoBehaviour
    {
        public Slider slider;

        private void Start()
        {
            slider = GetComponent<Slider>();
        }
        public void set_max_health(int max_health)
        {
            slider.maxValue = max_health;
            slider.value = max_health;
        }

        public void set_cur_health(int cur_health)
        {
            slider.value = cur_health;
        }
    }
}

