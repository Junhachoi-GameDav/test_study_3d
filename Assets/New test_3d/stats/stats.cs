using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public abstract class stats : MonoBehaviour
    {
        public abstract stats tick(enemy_manager enemy_mng,
            enemy_stats en_stats,
            enemy_animation_manager en_anime_mng);
    }
}

