using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class camera_move : MonoBehaviour
{
    public Transform player_obj;
    public Transform obj_enemy;
    public float follow_speed=10; //속도
    [Range(100, 800)]public float mouse_sensitivity= 300f; //감도
    public float clamp_angle = 70f; // 각도

    float rot_x;
    float rot_y;

    public Transform real_camera;
    public Vector3 dir_normalized; //방향/
    public Vector3 final_dir; //최종 위치
    public float min_distance; //최소 거리
    public float max_distance;
    public float final_distance;
    public float smoothness = 10;

    player_move player;

    void Start()
    {
        player = FindObjectOfType<player_move>();
        //초기화
        rot_x = transform.localRotation.eulerAngles.x;
        rot_y = transform.localRotation.eulerAngles.y;

        dir_normalized = real_camera.localPosition.normalized; // 노멀라이즈 크기가 0이되서 방향만 저장됨
        final_distance = real_camera.localPosition.magnitude; // magnitude는 길이의 크기를 의미함 

        Cursor.lockState = CursorLockMode.Locked; //마우스 고정
        Cursor.visible = false; //마우스 가려짐
    }

    // Update is called once per frame
    void Update()
    {
        //마우스 떄문에 반대로 받는다.
        rot_x += -(Input.GetAxis("Mouse Y")) * mouse_sensitivity * Time.deltaTime; //위아래는 반대여서 
        rot_y += Input.GetAxis("Mouse X") * mouse_sensitivity * Time.deltaTime;

        //clamp 막대기로 고정이라는 뜻이다
        rot_x = Mathf.Clamp(rot_x, -clamp_angle, clamp_angle); // x에서 ,최소(70도), 최대(70도)

        //회전관련 함수
        Quaternion rot = Quaternion.Euler(rot_x, rot_y, 0);
        transform.rotation = rot;
    }
    private void LateUpdate()
    {
        if (player.toggle_camera_rotation)
        {
            Vector3 target_en = Vector3.Scale(obj_enemy.position, new Vector3(1, 0, 1));
            transform.LookAt(obj_enemy);
            player.transform.LookAt(target_en);
        }
        transform.position = Vector3.MoveTowards(transform.position, player_obj.position, follow_speed * Time.deltaTime);


        //transformpoint = 로컬 포지션에서 월드 포지션으로 바꿔줌
        final_dir = transform.TransformPoint(dir_normalized * max_distance);



        //카메라 앞 캐릭터 뒤에 물체가 있을시
        RaycastHit hit;
        //라인으로 그린다는 뜻 (최소, 최대거리)
        if(Physics.Linecast(transform.position, final_dir, out hit)) //뭐가 있다.
        {
            final_distance = Mathf.Clamp(hit.distance, min_distance, max_distance);
        }
        else
        {
            final_distance = max_distance;
        }
        real_camera.localPosition = Vector3.Lerp(real_camera.localPosition, dir_normalized * final_distance, Time.deltaTime * smoothness);
    }
}
