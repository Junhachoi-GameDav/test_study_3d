using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sg
{
    public class camera_handler : MonoBehaviour
    {
        public Transform target_transform;
        public Transform camera_transform;
        public Transform camera_pivot_transform;

        private Transform my_transform;
        private Vector3 camera_transform_pos;
        private LayerMask ignore_layers;
        private Vector3 camera_follow_velocity = Vector3.zero;


        public static camera_handler cam_singleton;

        public float look_speed = 0.1f;
        public float follow_speed = 0.1f;
        public float pivot_speed = 0.03f;

        private float target_position;
        private float default_position;
        private float look_angle;
        private float pivot_angle;
        public float min_pivot = -35f;
        public float max_pivot = 35f;

        public float camera_sphere_radius = 0.2f;
        public float camera_collision_offset = 0.2f;
        public float min_collision_offset = 0.2f;

        private void Awake()
        {
            cam_singleton = this;
            my_transform = transform;
            default_position = camera_transform.localPosition.z;
            ignore_layers = ~(1 << 8 | 1 << 9 | 1 << 10);
        }

        public void follow_target(float delta)
        {
            Vector3 target_position = Vector3.SmoothDamp(my_transform.position, target_transform.position, ref camera_follow_velocity, delta / follow_speed);
            my_transform.position = target_position;

            handle_camera_collisions(delta);
        }

        public void handle_camera_rotation(float delta, float mouse_x_input, float mouse_y_input)
        {
            look_angle += (mouse_x_input * look_speed) / delta;
            pivot_angle -= (mouse_y_input * pivot_speed) / delta;
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
    }
}

