using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    [CreateAssetMenu(menuName = "A.I / enemy_ations/attack")]
    public class enemy_attack_ations : enemy_ations
    {
        public int attack_score = 3;
        public float recovery_time = 2;

        public float max_attack_angle = 35;
        public float min_attack_angle = -35;

        public float min_distance_needed_to_atk = 0;
        public float max_distance_needed_to_atk = 3;
    }
}

