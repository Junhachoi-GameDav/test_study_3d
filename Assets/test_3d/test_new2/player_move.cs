using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    Animator anime;
    Camera camera;
    CharacterController controller;

    public float walk_speed = 5f;
    public float run_speed = 9f;
    public float apply_spped;

    public bool toggle_camera_rotation; //배그 알트 기능 또는 다크소울 타겟팅 카메라;
    public float smoothness = 10f;

    //이동 값
    float f_num;
    float r_num;
    float break_time;

    //애니 시간 값
    float temp = 1;
    float cur_temp = 1;

    bool is_run;
    bool is_atk;
    void Start()
    {
        anime = FindObjectOfType<Animator>();
        camera = Camera.main;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            toggle_camera_rotation = !toggle_camera_rotation;
            //Debug.Log("dd");
        }

        p_move();
        p_attack();
    }
    
    private void LateUpdate()
    {
        //다크소울 타겟 기능으로 쓸거임
        if(toggle_camera_rotation == true)
        {
            //scale은 크기를 곱해준다.
            Vector3 player_rotate = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player_rotate), Time.deltaTime * smoothness); 
        }
    }
    void p_move()
    {
        apply_spped = is_run ? run_speed : walk_speed;
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(controller.velocity == Vector3.zero)
            {
                anime.SetFloat("is_run", 0f);
                is_run = false;
                return;
            }
            anime.SetFloat("is_run", 1f);
            is_run = true;
        }
        else
        {
            anime.SetFloat("is_run", 0f);
            is_run = false;
        }

        //배그 용
        //tronformdirection = 로컬에서 월드로 방향을 바꿔줌
        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //Vector3 right = transform.TransformDirection(Vector3.right);

        //다크소울 용
        Vector3 look_forward = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z).normalized;
        Vector3 look_right = new Vector3(camera.transform.right.x, 0, camera.transform.right.z).normalized;


        f_num = Input.GetAxisRaw("Vertical");
        r_num = Input.GetAxisRaw("Horizontal");

        float f_num_look = Input.GetAxis("Vertical");
        float r_num_look = Input.GetAxis("Horizontal");

        Vector3 move_dir = look_forward * f_num + look_right * r_num;
        Vector3 look_dir = look_forward * f_num_look + look_right * r_num_look;

        
        controller.Move(move_dir.normalized * apply_spped * Time.deltaTime);

        if (!toggle_camera_rotation)
        {
            if (f_num != 0 || r_num != 0)
            {
                break_time = 0;
                transform.forward = look_dir;
                anime.SetBool("is_walk", true);
                anime.SetBool("is_break", false);
                anime.SetInteger("is_walk_int", 1);
            }
            else
            {
                anime.SetBool("is_walk", false);
                if (break_time <= 4)
                {
                    break_time += Time.deltaTime;
                }
                else if( break_time >4 && break_time <= 7)
                {
                    break_time += Time.deltaTime;
                    anime.SetBool("is_break", true);   
                }
                else
                {
                    anime.SetBool("is_break", false);
                    break_time = 0;
                }
            }
        }
        else if (f_num != 0 || r_num != 0)
        {
            break_time = 0;
            anime.SetBool("is_break", false);
            anime.SetBool("is_walk", true);
            if (f_num == 1)
            {
                anime.SetInteger("is_walk_int", 1);
            }
            else if(f_num==-1)
            {
                anime.SetInteger("is_walk_int", 4);
            }

            if(r_num == 1)
            {
                anime.SetInteger("is_walk_int", 3);
            }
            else if(r_num == -1)
            {
                anime.SetInteger("is_walk_int", 2);
            }
        }
        else
        {
            anime.SetBool("is_walk", false);
            if (break_time <= 4)
            {
                break_time += Time.deltaTime;
            }
            else if (break_time > 4 && break_time <= 7)
            {
                break_time += Time.deltaTime;
                anime.SetBool("is_break", true);
            }
            else
            {
                anime.SetBool("is_break", false);
                break_time = 0;
            }
        }
    }

    void p_attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            temp = 1.5f;
            cur_temp = 1.8f;
            is_atk = true;
            break_time = 0;
            anime.SetLayerWeight(1, 1);
            anime.SetTrigger("do_atk");
            anime.SetBool("is_break", false);
        }
        if (is_atk)
        {
            if (temp > 0)
            {
                temp -= Time.deltaTime; //딜레이
            }
            else
            {
                if(cur_temp > 0)
                {
                    cur_temp -= Time.deltaTime;
                }
                else
                {
                    is_atk = false;
                }
                anime.SetLayerWeight(1, cur_temp);
            }

        }
    }
    
}
