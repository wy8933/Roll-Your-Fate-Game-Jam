using System;
using UnityEngine;

namespace Utility
{
    public class ContactBroadcaster : MonoBehaviour
    {
        protected IContactReceiver Receiver;
        protected void Awake()
        {
            Receiver = GetComponentInParent<IContactReceiver>();
        }

        protected virtual void OnCollisionEnter(Collision other)
        {
            ContactContext context = new ContactContext(this, other, null, ContactType.OnCollisionEnter);
            Receiver?.HandleReceivedContact(context);
        }

        protected virtual void OnCollisionStay(Collision other)
        {
            ContactContext context = new ContactContext(this, other, null, ContactType.OnCollisionStay);
            Receiver?.HandleReceivedContact(context);
        }

        protected virtual void OnCollisionExit(Collision other)
        {
            ContactContext context = new ContactContext(this, other, null, ContactType.OnCollisionExit);
            Receiver?.HandleReceivedContact(context);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            ContactContext context = new ContactContext(this, null, other, ContactType.OnTriggerEnter);
            Receiver?.HandleReceivedContact(context);
        }

        protected virtual void OnTriggerStay(Collider other)
        {
            ContactContext context = new ContactContext(this, null, other, ContactType.OnTriggerStay);
            Receiver?.HandleReceivedContact(context);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            ContactContext context = new ContactContext(this, null, other, ContactType.OnTriggerExit);
            Receiver?.HandleReceivedContact(context);
        }
    }
    
    public interface IContactReceiver
    {
        void HandleReceivedContact(ContactContext context);
    }

    public struct ContactContext
    {
        public ContactBroadcaster sender;
        public Collision otherCollision;
        public Collider otherCollider;
        public ContactType contactType;

        public ContactContext(ContactBroadcaster sender, Collision otherCollision, Collider otherCollider, ContactType contactType)
        {
            this.sender = sender;
            this.otherCollision = otherCollision;
            this.otherCollider = otherCollider;
            this.contactType = contactType;
        }
    }

    public enum ContactType
    {
        OnCollisionEnter,
        OnCollisionStay,
        OnCollisionExit,
        OnTriggerEnter,
        OnTriggerStay,
        OnTriggerExit
    }
}
