using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class interactable : MonoBehaviour
    {
        public float radius = 0.6f;
        public string interactable_text;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public virtual void interact(player_manager player_m)
        {
            Debug.Log("your interacted!!!~~~");
        }
    }
}

