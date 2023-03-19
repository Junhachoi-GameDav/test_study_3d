using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class camera_handler : MonoBehaviour
    {
        input_handler input_h;

        public Transform target_transform;
        public Transform camera_transform;
        public Transform camera_pivot_transform;

        private Transform my_transform;
        private Vector3 camera_transform_pos;
        public LayerMask ignore_layers;
        private Vector3 camera_follow_velocity = Vector3.zero;


        public static camera_handler cam_singleton;

        public float look_speed = 0.1f;
        public float pivot_speed = 0.03f;
        public float follow_speed = 0.1f;

        private float target_position;
        private float default_position;
        private float look_angle;
        private float pivot_angle;
        public float min_pivot = -35f;
        public float max_pivot = 35f;

        public float camera_sphere_radius = 0.2f;
        public float camera_collision_offset = 0.2f;
        public float min_collision_offset = 0.2f;

        public Transform cur_lock_on_target;


        List<character_manager> available_targets = new List<character_manager>();
        public Transform nearest_lock_on_target;
        public Transform left_lock_target;
        public Transform right_lock_target;
        public float max_lock_on_distance = 30;

        private void Awake()
        {
            cam_singleton = this;
            my_transform = transform;
            default_position = camera_transform.localPosition.z;
            ignore_layers = ~(1 << 8 | 1 << 9 | 1 << 10);
            target_transform = FindObjectOfType<player_manager>().transform;
            input_h = FindObjectOfType<input_handler>();
        }

        public void follow_target(float delta)
        {
            Vector3 target_position = Vector3.SmoothDamp(my_transform.position, target_transform.position, ref camera_follow_velocity, delta / follow_speed);
            //Vector3 target_position = Vector3.Lerp(my_transform.position, target_transform.position, delta / follow_speed);
            my_transform.position = target_position;

            handle_camera_collisions(delta);
        }

        public void handle_camera_rotation(float delta, float mouse_x_input, float mouse_y_input)
        {
            if(input_h.lock_on_flag == false && cur_lock_on_target == null)
            {
                look_angle += mouse_x_input * look_speed * delta;
                pivot_angle -= mouse_y_input * pivot_speed * delta;
                pivot_angle = Mathf.Clamp(pivot_angle, min_pivot, max_pivot);

                Vector3 rotation = Vector3.zero;
                rotation.y = look_angle;
                Quaternion target_rotation = Quaternion.Euler(rotation);
                my_transform.rotation = target_rotation;

                rotation = Vector3.zero;
                rotation.x = pivot_angle;

                target_rotation = Quaternion.Euler(rotation);
                camera_pivot_transform.localRotation = target_rotation;
            }
            else
            {
                float velocity = 0;

                Vector3 dir = cur_lock_on_target.position - transform.position;
                dir.Normalize();
                dir.y = 0;

                Quaternion target_rotation = Quaternion.LookRotation(dir);
                transform.rotation = target_rotation;

                dir = cur_lock_on_target.position - camera_pivot_transform.position;
                dir.Normalize();

                target_rotation = Quaternion.LookRotation(dir);
                Vector3 euler_angle = target_rotation.eulerAngles;
                euler_angle.y = 0;
                camera_pivot_transform.localEulerAngles = euler_angle;
            }
        }

        private void handle_camera_collisions(float delta)
        {
            target_position = default_position;
            RaycastHit hit;
            Vector3 direction = camera_transform.position - camera_pivot_transform.position;
            direction.Normalize();

            if(Physics.SphereCast(camera_pivot_transform.position, camera_sphere_radius, direction, out hit, Mathf.Abs(target_position), ignore_layers))
            {
                float dis = Vector3.Distance(camera_pivot_transform.position, hit.point);
                target_position = -(dis - camera_collision_offset);
            }

            if(Mathf.Abs(target_position) < min_collision_offset)
            {
                target_position = -min_collision_offset;
            }

            camera_transform_pos.z = Mathf.Lerp(camera_transform.localPosition.z, target_position, delta / 0.2f);
            camera_transform.localPosition = camera_transform_pos;
        }

        public void handle_lock_on()
        {
            float shortest_distance = Mathf.Infinity; //z축 방향으로 무한하게
            float shortest_distance_of_left_target = Mathf.Infinity;
            float shortest_distance_of_right_target = Mathf.Infinity;

            // 주변에 있는 모든 콜라이더(적)을 추출 (target_transform.position에서 부터 26 길이 만큼)
            Collider[] colliders = Physics.OverlapSphere(target_transform.position, 26);

            for (int i = 0; i < colliders.Length; i++)
            {
                character_manager character = colliders[i].GetComponent<character_manager>();

                if(character != null)
                {
                    Vector3 lock_target_dir = character.transform.position - target_transform.position;
                    float distance_form_target = Vector3.Distance(target_transform.position, character.transform.position);
                    float viewable_angle = Vector3.Angle(lock_target_dir, camera_transform.forward);
                    
                    //root는 계층구조를 의미한다. 자식으로 있거나 부모로 있는것을 비교
                    if(character.transform.root != target_transform.transform.root 
                        && viewable_angle > -50 && viewable_angle < 50
                        && distance_form_target <= max_lock_on_distance)
                    {
                        available_targets.Add(character);
                    }
                }
            }

            for (int k = 0; k < available_targets.Count; k++)
            {
                float distance_form_target = Vector3.Distance(target_transform.position, available_targets[k].transform.position);

                if(distance_form_target < shortest_distance)
                {
                    shortest_distance = distance_form_target;
                    nearest_lock_on_target = available_targets[k].lock_on_tranform;
                }

                if (input_h.lock_on_flag)
                {
                    //InverseTransformPoint 는 스타크래프트와 같은 RTS 에서 장르에서 어떤 객체에게 이동 명령을 내릴 경우
                    //클릭한 이동지점으로 이동하는 동시에 바라보는 방향 또한 이동 지점쪽으로 바꾸는데
                    //이런 경우를 처리할시 유용
                    //월드 공간 기준 targetPos 을 로컬 공간(플레이어의) 기준의 위치로 바꾼후
                    //z축(플레이어의 앞 방향)
                    Vector3 relative_enemy_position = cur_lock_on_target.InverseTransformPoint(available_targets[k].transform.position);
                    var distance_from_left_target = cur_lock_on_target.transform.position.x + available_targets[k].transform.position.x;
                    var distance_from_right_target = cur_lock_on_target.transform.position.x - available_targets[k].transform.position.x;

                    if(relative_enemy_position.x > 0.00 && distance_from_left_target < shortest_distance_of_left_target)
                    {
                        shortest_distance_of_left_target = distance_from_left_target;
                        left_lock_target = available_targets[k].lock_on_tranform;
                    }

                    if(relative_enemy_position.x < 0.00 && distance_from_right_target < shortest_distance_of_right_target)
                    {
                        shortest_distance_of_right_target = distance_from_right_target;
                        right_lock_target = available_targets[k].lock_on_tranform;
                    }
                }
            }
        }

        public void clear_lock_on_target()
        {
            //add로 넣어준것을 clear로 없애기
            available_targets.Clear();
            nearest_lock_on_target = null;
            cur_lock_on_target = null;
        }
    }
}

