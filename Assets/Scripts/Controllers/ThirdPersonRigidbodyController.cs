using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Controller
{
    public class ThirdPersonRigidbodyController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private float speed = 5.0f;
        [SerializeField]
        private float turnSmoothTime = 0.1f;
        [SerializeField]
        private Transform mainCamera;
        [Header("Debug Stuff")]
        private Rigidbody _rigidbody;
        private float turnSmoothVelocity;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

            if (direction.magnitude >= 0.1)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                _rigidbody.MoveRotation(Quaternion.Euler(0.0f, angle, 0.0f));

                Vector3 moveDirection = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
                _rigidbody.MovePosition(moveDirection.normalized * speed * Time.deltaTime);
            }
        }
    }
}
