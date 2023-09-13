using UnityEngine;

namespace BulletMerge
{
    internal class CameraCorrecter : MonoBehaviour
    {
        [SerializeField] private float sensitivity = 1;

        private void LateUpdate() => transform.SetLocalPositionAndRotation(
                                     Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * sensitivity),
                                     Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime * sensitivity));
    }
}