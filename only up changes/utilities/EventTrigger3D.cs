using UnityEngine;
using UnityEngine.Events;

namespace Safwat.Essentials
{
    public class EventTrigger3D : MonoBehaviour
    {
        // #region Trigger
        // [SerializeField] private UnityEvent<Collider> onTriggerEnter;
        // [SerializeField] private UnityEvent<Collider> onTriggerExit;
        // [SerializeField] private UnityEvent<Collider> onTriggerStay;

        // private void OnTriggerEnter(Collider other) => onTriggerEnter?.Invoke(other);

        // private void OnTriggerStay(Collider other) => onTriggerStay?.Invoke(other);

        // private void OnTriggerExit(Collider other) => onTriggerExit?.Invoke(other);
        // #endregion

        // #region Collision
        // [SerializeField] private UnityEvent<Collision> onCollisionEnter;
        // [SerializeField] private UnityEvent<Collision> onCollisionExit;
        // [SerializeField] private UnityEvent<Collision> onCollisionStay;

        // private void OnCollisionEnter(Collision collision) => onCollisionEnter?.Invoke(collision);

        // private void OnCollisionStay(Collision collision) => onCollisionStay?.Invoke(collision);

        // private void OnCollisionExit(Collision collision) => onCollisionExit?.Invoke(collision);
        // #endregion

        [SerializeField] private UnityEvent onEnter;
        [SerializeField] private UnityEvent onExit;
        [SerializeField] private UnityEvent onStay;

        private void OnTriggerEnter(Collider other) => onEnter?.Invoke();

        private void OnTriggerStay(Collider other) => onStay?.Invoke();

        private void OnTriggerExit(Collider other) => onExit?.Invoke();

        private void OnValidate()
        {
            if (!TryGetComponent(out Collider _))
                Debug.LogError($"Collider Missing!");
        }
    }
}