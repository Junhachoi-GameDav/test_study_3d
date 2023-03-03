using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class player_locomotion : MonoBehaviour
    {
        Transform camera_obj;
        input_handler input_h;

        Vector3 move_dir;

        [HideInInspector]
        public Transform my_transform;

        public new Rigidbody rigid;
        public GameObject normal_camera;

        [Header("Stats")]
        [SerializeField]
        float movement_speed = 5f;
        [SerializeField]
        float rotation_speed = 10f;



        void Start()
        {
            rigid = GetComponent<Rigidbody>();
            input_h = GetComponent<input_handler>();

            camera_obj = Camera.main.transform;
            my_transform = transform;

        }

        public void Update()
        {
            float delta = Time.deltaTime;

            input_h.tick_input(delta);

            move_dir = camera_obj.forward * input_h.vertical;
            move_dir += camera_obj.right * input_h.horizontal;
            move_dir.Normalize();

            float speed = movement_speed;
            move_dir *= speed;

            Vector3 projected_velocity = Vector3.ProjectOnPlane(move_dir, normal_vector);
            rigid.velocity = projected_velocity;

        }

        #region movement
        Vector3 normal_vector;
        Vector3 target_position;

        private void handle_rotation(float delta)
        {
            Vector3 target_dir = Vector3.zero;

            float move_override = input_h.move_amount;

            target_dir = camera_obj.forward * input_h.vertical;
            target_dir += camera_obj.right * input_h.horizontal;

            target_dir.Normalize();
            target_dir.y = 0;

            if(target_dir == Vector3.zero)
            {
                target_dir = my_transform.forward;
            }

            float r_s = rotation_speed;
            Quaternion t_r = Quaternion.LookRotation(target_dir);
            Quaternion target_rotation = Quaternion.Slerp(my_transform.rotation, t_r, r_s * delta);

            my_transform.rotation = target_rotation;
        }
        #endregion
    }
}

