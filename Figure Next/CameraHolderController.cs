using UnityEngine;

namespace BulletMerge
{
    public class CameraHolderController : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        private Vector3 velocity;

        [SerializeField] private float smoothTime = 1;
        [SerializeField] private Transform target;

        private void LateUpdate()
        {
            Vector3 desiredPosition = target.TransformPoint(offset);
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
            transform.LookAt(target);
        }
    }
}