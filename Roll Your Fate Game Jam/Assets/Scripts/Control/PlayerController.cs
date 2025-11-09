using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using Utility;

namespace Control
{
    public class PlayerController : MonoBehaviour
    {
        const double eps = 1e-4;

        #region Components
        [SerializeField] private Transform characterTransform;
        private CinemachineCamera playerCamera;
        private Rigidbody RB;
        private Animator animator;
        #endregion
        
        #region Kinematic
        [SerializeField]
        private CharacterMovementParameter movementParam;
        private float Acceleration => movementParam.acceleration;
        private float Acceleration_Air => movementParam.acceleration_Air;
        private float MaxSpeed => movementParam.maxSpeed;
        private float Gravity => movementParam.gravity;
        private float BrakeFactor => movementParam.brakeFactor;
        private float RotateSpeed => movementParam.rotateSpeed;
        private Vector3 GroundDetectionCenter => movementParam.groundDetectionCenter;
        private float GroundDetectionRadius => movementParam.groundDetectionRadius;
        
        #endregion

        #region Flags

        public bool isOnGround;
        #endregion

        #region AudioClips

        #endregion
        
        private void Awake()
        {
            RB = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            PlayerInputHandler.Instance.Enable();
        }

        private void OnDestroy()
        {
        }

        private void Update()
        {
 
        }

        private void FixedUpdate()
        {
            GroundCheck();
            Move();
        }

        #region Movement

        void Move()
        {
            Vector2 targetDirection = PlayerInputHandler.Instance.InputVector.normalized;
            Vector3 velocity = RB.linearVelocity;
            Vector2 horizontalVel = new Vector2(velocity.x, velocity.z);
            if (isOnGround)
            {
                if (targetDirection.magnitude > eps)
                {
                    horizontalVel = Vector2.ClampMagnitude(horizontalVel + targetDirection * Acceleration * Time.fixedDeltaTime, MaxSpeed * PlayerInputHandler.Instance.InputVector.magnitude); 
                }
                else
                {
                    horizontalVel *= BrakeFactor;
                    if (horizontalVel.magnitude < eps)
                        horizontalVel = Vector2.zero;
                }
            }
            else
            {
                if (targetDirection.magnitude > eps)
                {
                    horizontalVel = Vector2.ClampMagnitude(horizontalVel + targetDirection * Acceleration_Air * Time.fixedDeltaTime, MaxSpeed * PlayerInputHandler.Instance.InputVector.magnitude); 
                }
            }
            animator.SetFloat("Speed", horizontalVel.magnitude);
            velocity = new Vector3(horizontalVel.x, velocity.y - Gravity * Time.fixedDeltaTime, horizontalVel.y);
            RB.linearVelocity = velocity;
            
            if (targetDirection.magnitude > eps)
            {
                Vector3 targetDir3 = new Vector3(targetDirection.x, 0, targetDirection.y);
                Quaternion targetRotation = Quaternion.LookRotation(targetDir3, Vector3.up);
                characterTransform.rotation = Quaternion.Lerp(
                    characterTransform.rotation,
                    targetRotation,
                    RotateSpeed * Time.fixedDeltaTime); // smooth rotation
            }
            
        }

        public void GroundCheck()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position + GroundDetectionCenter, GroundDetectionRadius);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Player"))
                    continue;
                isOnGround = true;
                return;
            }
            isOnGround = false;
        }

        #endregion

        public void EnableControl()
        {
            PlayerInputHandler.Instance.Enable();
        }
        
        public void DisableControl()
        {
            PlayerInputHandler.Instance.Disable();
        }

        #region Gizmos

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + GroundDetectionCenter, GroundDetectionRadius);
        }

        #endregion
    }


    [Serializable]
    public class CharacterMovementParameter
    {
        public float acceleration;
        public float acceleration_Air;
        public float maxSpeed;
        public float brakeFactor;
        public float rotateSpeed;
        public float gravity;
        public Vector3 groundDetectionCenter;
        public float groundDetectionRadius;
    }
}
