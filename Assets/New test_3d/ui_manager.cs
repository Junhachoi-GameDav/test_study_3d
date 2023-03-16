using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class ui_manager : MonoBehaviour
    {
        public GameObject select_window;

        public void open_select_window()
        {
            select_window.SetActive(true);
        }

        public void close_select_window()
        {
            select_window.SetActive(false);
        }
    }
}

