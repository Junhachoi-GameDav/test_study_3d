using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class stats_idle : stats
    {
        public override stats tick(enemy_manager enemy_mng, enemy_stats en_stats, enemy_animation_manager en_anime_mng)
        {
            return this;
        }
    }
}

