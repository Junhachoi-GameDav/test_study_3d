using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anime_manager : MonoBehaviour
{
    public Animator anime;

    float temp = 1;

    //getcurrentanimatorstateinfo = 애니메이션 레이어
    //normalizedtime = 레이어의 시간을 나타냄 0 -> 1
    void Update()
    {
        if(anime.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f)
        {
            if(temp > 0)
            {
                temp -= Time.deltaTime;
            }
            anime.SetLayerWeight(1, temp); // 1번째 레이어를 0으로 바꿈 즉 조건문 이후 비활성화
        }
    }
}
