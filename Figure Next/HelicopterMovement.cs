using UnityEngine;

namespace AirAttack
{
    internal class HelicopterMovement : MonoBehaviour
    {
        [SerializeField] private Transform gfx;
        [SerializeField] private Transform target;

        [SerializeField] private float movementSpeed = 5.0f;
        [SerializeField] private float rotationSpeed = 5.0f;
        [SerializeField] private float maxTiltAmount = 20.0f;

        private void Update()
        {
            Vector3 direction = target.position - transform.position;
            Vector3 normalizedDirection = direction.normalized;

            // Calculate the tilt based on how far the helicopter is facing in y rotation from the target
            float angle = Vector3.Angle(transform.forward, normalizedDirection);
            float tiltAmount = Mathf.InverseLerp(0, 180, angle) * maxTiltAmount;
            float horizontalTilt = normalizedDirection.x * tiltAmount;
            float verticalTilt = normalizedDirection.z * tiltAmount;
            float step = Time.deltaTime * rotationSpeed;

            Quaternion playerTargetRotation = Quaternion.LookRotation(direction);
            Vector3 playerTargetPosition = Vector3.Slerp(transform.forward, normalizedDirection, Time.deltaTime * movementSpeed);

            Vector3 tilt = new(verticalTilt, 0, -horizontalTilt);
            Quaternion gfxTargetRotation = Quaternion.Euler(tilt);

            if (Vector3.Distance(transform.position, target.position) < 1f)
                transform.position = target.position;
            else
                transform.position += Time.deltaTime * movementSpeed * playerTargetPosition;

            if (Quaternion.Angle(transform.rotation, playerTargetRotation) < 1f)
                transform.rotation = playerTargetRotation;
            else
                transform.rotation = Quaternion.Slerp(transform.rotation, playerTargetRotation, step);

            if (tilt.magnitude < 1f)
                gfx.localRotation = gfxTargetRotation;
            else
                gfx.localRotation = Quaternion.Slerp(gfx.localRotation, gfxTargetRotation, step);
        }
    }
}