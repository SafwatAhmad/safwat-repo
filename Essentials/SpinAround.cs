using UnityEngine;

namespace Essentials
{
    enum Axis { x, y, z }

    internal class SpinAround : MonoBehaviour
    {
        [SerializeField] private Axis axis;
        [SerializeField] private float speed = 360;

        private Vector3 GetAxis => axis switch
        {
            Axis.x => Vector3.right,
            Axis.y => Vector3.up,
            Axis.z => Vector3.forward,
            _ => Vector3.zero
        };

        private void LateUpdate() => transform.Rotate(GetAxis, speed * Time.deltaTime);
    }
}