using UnityEngine;
using UnityEngine.UI;

namespace BattleShip.WeaponSystem
{
    internal class Crosshair : MonoBehaviour
    {
        [SerializeField] private int maxRaycastRange = 100;
        [SerializeField] private Image crosshairIcon;
        [SerializeField] private UnityEngine.Camera weaponCamera;

        [SerializeField] private LayerMask hostilesLayerMask;
        [SerializeField] private LayerMask friendlyLayerMask;

        [SerializeField] private Color hostilesCrosshairColor;
        [SerializeField] private Color friendlyCrosshairColor;
        [SerializeField] private Color neutralsCrosshairColor;

        internal Ray LineOfSight { get; private set; }

        private void Update()
        {
            UpdateLineOfSight();
            UpdateCrosshairColor();
        }

        private void UpdateLineOfSight() => LineOfSight = weaponCamera.ScreenPointToRay(crosshairIcon.rectTransform.position);

        private void UpdateCrosshairColor()
        {
            Color crosshairColor = neutralsCrosshairColor;

            if (Physics.Raycast(LineOfSight, maxRaycastRange, hostilesLayerMask))
                crosshairColor = hostilesCrosshairColor;
            else
            if (Physics.Raycast(LineOfSight, maxRaycastRange, friendlyLayerMask))
                crosshairColor = friendlyCrosshairColor;

            crosshairIcon.color = crosshairColor;
        }

        private void OnDrawGizmos()
        {
            Ray ray = weaponCamera.ScreenPointToRay(crosshairIcon.rectTransform.position);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(ray.origin, ray.direction * maxRaycastRange);
        }
    }
}