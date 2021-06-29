using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Controller
{
    public class ThirdPersonCharacterController : MonoBehaviour
    {
        [Header("Gravity")]
        [SerializeField]
        private float gravity = -9.81f;
        [SerializeField]
        private Transform groundCheck;
        [SerializeField]
        private float groundDistance = 0.4f;
        [SerializeField]
        private LayerMask groundMask;
        [SerializeField]
        private bool isGrounded = false;


        [Header("Movement")]
        [SerializeField]
        private float speed = 5.0f;
        [SerializeField]
        private float turnSmoothTime = 0.1f;


        [Header("Dependencies")]
        [SerializeField]
        private Transform mainCamera;

        [Header("Debug Stuff")]
        private CharacterController controller;
        private float turnSmoothVelocity;
        private Vector3 velocity;
        private Vector3 direction;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            isGrounded = IsGrounded();

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2.0f;
            }

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            direction = new Vector3(horizontal, 0.0f, vertical).normalized;

            if (ShouldMove())
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

                Vector3 moveDirection = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        public bool IsGrounded()
        {
            return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }

        public bool ShouldMove()
        {
            return direction.magnitude >= 0.1;
        }
    }
}
