using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Control
{
    public class PlayerController : MonoBehaviour, IContactReceiver
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
        private PlayerInputHandler playerInput;
        #endregion

        private void Awake()
        {
            RB = GetComponent<Rigidbody>();
            playerInput = GetComponent<PlayerInputHandler>();
        }

        private void Start()
        {
            playerInput.Enable();
            playerInput.Interact += Interact;
        }

        private void OnDestroy()
        {
            playerInput.Interact -= Interact;
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
            Vector3 targetDirection = new Vector3(playerInput.InputVector.normalized.x, 0, playerInput.InputVector.normalized.y);
            
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
            playerInput.Enable();
        }
        
        public void DisableControl()
        {
            playerInput.Disable();
        }

        #region Interaction
        
        List<Interactable> interactionTargets = new List<Interactable>();
        public void HandleReceivedContact(ContactContext context) // Handle contacts sent from the child called detection box
        {
            GameObject senderObj = context.sender.gameObject;
            
            Interactable interactableObject = senderObj.GetComponent<Interactable>();
            if (interactableObject != null)
            {
                if (context.contactType == ContactType.OnTriggerEnter)
                {
                    interactionTargets.Add(interactableObject);
                }
                else if(context.contactType == ContactType.OnTriggerExit)
                {
                    interactionTargets.Remove(interactableObject);
                }
            }
        }

        public void Interact()
        {
            if(interactionTargets.Count > 0)
                interactionTargets[0].Interacted();
        }

        #endregion
    }

    public interface Interactable // Just a placeholder
    {
        void Interacted();
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
