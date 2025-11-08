using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Control
{
    public class PlayerController : MonoBehaviour
    {
        const double eps = 1e-4;

        #region Kinematic
        [SerializeField]
        private CharacterMovementParameter movementParam;
        private float MaxAcceleration => movementParam.maxAcceleration;
        private float MaxSpeed => movementParam.maxSpeed;
        private float BrakeFactor => movementParam.brakeFactor;

        private Vector3 velocity = Vector3.zero;
        #endregion

        #region Components
        private Rigidbody RB;
        #endregion

        private void Awake()
        {
            RB = GetComponent<Rigidbody>();
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
            Move();
        }

        #region Movement

        void Move()
        {
            Vector3 targetDirection = new Vector3(PlayerInputHandler.Instance.InputVector.normalized.x, 0, PlayerInputHandler.Instance.InputVector.normalized.y);
            
            if (targetDirection.magnitude > eps)
            {
                velocity = Vector3.ClampMagnitude(velocity + targetDirection * MaxAcceleration, MaxSpeed); 
            }
            else
            {
                velocity *= BrakeFactor * Time.fixedDeltaTime; // brake in a fixed length of time
                if (velocity.magnitude < eps)
                    velocity = Vector3.zero;
            }
            Vector3 newPosition = RB.position + velocity * Time.fixedDeltaTime;

            Quaternion newRotation;
            if (targetDirection.magnitude > eps)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
                newRotation = Quaternion.Lerp(
                    transform.rotation,
                    targetRotation,
                    movementParam.rotateRate * Time.fixedDeltaTime); // smooth rotation
            }
            else
            {
                newRotation = RB.rotation;
            }
            
            RB.Move(newPosition, newRotation);
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

        #region Interaction
        
        // List<IInteractable> interactionTargets = new List<IInteractable>();
        // public void HandleReceivedContact(ContactContext context) // Handle contacts sent from the child called detection box
        // {
        //     GameObject senderObj = context.sender.gameObject;
        //     
        //     IInteractable interactableObject = senderObj.GetComponent<IInteractable>();
        //     if (interactableObject != null)
        //     {
        //         if (context.contactType == ContactType.OnTriggerEnter)
        //         {
        //             interactionTargets.Add(interactableObject);
        //         }
        //         else if(context.contactType == ContactType.OnTriggerExit)
        //         {
        //             interactionTargets.Remove(interactableObject);
        //         }
        //     }
        // }

        public void Interact()
        {
            
        }

        #endregion
    }


    [Serializable]
    public class CharacterMovementParameter
    {
        public float maxAcceleration;
        public float maxSpeed;
        public float brakeFactor;
        public float rotateRate;
        
    }
}
