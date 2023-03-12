using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace sg
{
    public class stamina_bar : MonoBehaviour
    {
        public Slider slider;

        private void Start()
        {
            slider = GetComponent<Slider>();
        }
        public void set_max_stamina(int max_stamina)
        {
            slider.maxValue = max_stamina;
            slider.value = max_stamina;
        }

        public void set_cur_stamina(int cur_stamina)
        {
            slider.value = cur_stamina;
        }
    }
}
