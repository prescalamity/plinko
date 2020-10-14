using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ColliderMessage : MonoBehaviour
{
    public bool Enable = true;
    [SerializeField]
    public UnityEvent OnTrigger = new UnityEvent();
    public bool
        onCollisionEnter,
        onCollisionEnter2D,
        onCollisionExit,
        onCollisionExit2D,
        onCollisionStay,
        onCollisionStay2D,
        onTriggerEnter,
        onTriggerEnter2D,
        onTriggerExit,
        onTriggerExit2D,
        onTriggerStay,
        onTriggerStay2D;

    public static Collision TriggerCollision;
    public static Collision2D TriggerCollision2D;
    public static Collider TriggerCollider;
    public static Collider2D TriggerCollider2D;

    public static MessageType messageType;
    public enum MessageType
    {
        None,
        CollisionEnter,
        CollisionEnter2D,
        CollisionExit,
        CollisionExit2D,
        CollisionStay,
        CollisionStay2D,
        TriggerEnter,
        TriggerEnter2D,
        TriggerExit,
        TriggerExit2D,
        TriggerStay,
        TriggerStay2D
    }
    void OnCollisionEnter(Collision collision)
    {
        if (Enable && onCollisionEnter)
        {
            messageType = MessageType.CollisionEnter;
            TriggerCollision = collision;
            OnTrigger.Invoke();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
		
        if (Enable && onCollisionEnter2D)
        {
            messageType = MessageType.CollisionEnter2D;
            TriggerCollision2D = collision;
            OnTrigger.Invoke();
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (Enable && onCollisionExit)
        {
            messageType = MessageType.CollisionExit;
            TriggerCollision = collision;
            OnTrigger.Invoke();
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (Enable && onCollisionExit2D)
        {
            messageType = MessageType.CollisionExit2D;
            TriggerCollision2D = collision;
            OnTrigger.Invoke();
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (Enable && onCollisionStay)
        {
            messageType = MessageType.CollisionStay;
            TriggerCollision = collision;
            OnTrigger.Invoke();
        }
    }
//    void OnCollisionStay2D(Collision2D collision)
//    {
//        if (Enable && onCollisionStay2D)
//        {
//            messageType = MessageType.CollisionStay2D;
//            TriggerCollision2D = collision;
//            OnTrigger.Invoke();
//        }
//    }
    void OnTriggerEnter(Collider collider)
    {
        if (Enable && onTriggerEnter)
        {
            messageType = MessageType.TriggerEnter;
            TriggerCollider = collider;
            OnTrigger.Invoke();
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Enable && onTriggerEnter2D)
        {
            messageType = MessageType.TriggerEnter2D;
            TriggerCollider2D = collider;
            OnTrigger.Invoke();
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (Enable && onTriggerExit)
        {
            messageType = MessageType.TriggerExit;
            TriggerCollider = collider;
            OnTrigger.Invoke();
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (Enable && onTriggerExit2D)
        {
            messageType = MessageType.TriggerExit2D;
            TriggerCollider2D = collider;
            OnTrigger.Invoke();
        }
    }
    void OnTriggerStay(Collider collider)
    {
        if (Enable && onTriggerStay)
        {
            messageType = MessageType.TriggerStay;
            TriggerCollider = collider;
            OnTrigger.Invoke();
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if (Enable && onTriggerStay2D)
        {
            messageType = MessageType.TriggerStay2D;
            TriggerCollider2D = collider;
            OnTrigger.Invoke();
        }
    }
}
