using UnityEngine;
using Safwat.Essentials;
using BattleShip.WeaponSystem;
using BattleShip.WeaponSystem.Camera;

namespace BattleShip
{
    internal class Player : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private RotationHandler cameraRotationHandler;
        [SerializeField] private InputManager inputManager;

        private void Update()
        {
            //Handle Camera
            cameraRotationHandler.HandleXAxisRotation(-inputManager.VerticalSwipeDelta); //inverted mouse y
            cameraRotationHandler.HandleYAxisRotation(inputManager.HorizontalSwipeDelta);

            //Handle Weapon
            if (inputManager.Pressed)
            {
                if (!weapon.IsAiming)
                    weapon.Aim();
            }
            else
            if (inputManager.Released)
            {
                if (!weapon.Reloading && weapon.ProjectilesInClip <= 0)
                    weapon.Reload();
                else
                if (!weapon.IsAutomatic)
                    weapon.Fire();
            }
            else
            if (inputManager.Holding)
            {
                if (weapon.IsAutomatic)
                    if (weapon.IsAiming)
                        weapon.Fire();
            }
        }
    }
}