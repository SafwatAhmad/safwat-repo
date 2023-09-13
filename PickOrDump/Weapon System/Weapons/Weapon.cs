using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BattleShip.WeaponSystem
{
    internal class Weapon : MonoBehaviour
    {
        /////////////////////////////////////////////////////
        ////////////////// Serialized Vars //////////////////
        [SerializeField] private bool autoFire;
        [SerializeField] private float fireRate;
        [SerializeField] private float reloadTime;
        [SerializeField] private int clipSize;

        [SerializeField] private Crosshair crosshair;
        [SerializeField] private Transform firePoint;
        [SerializeField] private AudioSource fireSoundFX;
        [SerializeField] private GameObject muzzleFlashFX;
        [SerializeField] private Animation recoilAnimation;
        [SerializeField] private Projectile projectilePrefab;

        /////////////////////////////////////////////////////
        //////////////// Non Serialized Vars ////////////////
        private bool aiming;
        private float lastFireTime;
        private float reloadStartTime;
        private Queue<Projectile> projectiles = new();

        /////////////////////////////////////////////////////
        //////////////////// Properties /////////////////////
        internal bool IsAiming => aiming;
        internal bool IsAutomatic => autoFire;
        internal bool Reloading => reloadTime > Time.time - reloadStartTime;
        private bool CanFire => ProjectilesInClip > 0 && (Time.time - lastFireTime) > (1 / fireRate);
        internal int ProjectilesInClip { get; private set; }

        /////////////////////////////////////////////////////
        /////////////// MonoBehaviour Methods ///////////////

        private void Start()
        {
            ProjectilesInClip = clipSize;
        }

        /////////////////////////////////////////////////////
        //////////////// Weapon Functionality ///////////////

        // Aim
        internal void Aim()
        {
            aiming = true;
            crosshair.gameObject.SetActive(true);
            // Animate Transition
        }

        // Fire
        internal void Fire()
        {
            if (CanFire)
            {
                Vector3 direction = (crosshair.LineOfSight.direction * 100 - firePoint.position).normalized;
                GetProjectile().Launch(direction);

                // GetProjectile().Launch(crosshair.LineOfSight.direction);

                --ProjectilesInClip;
                lastFireTime = Time.time;

                // PlayFireSoundFX();
                // PlayMuzzleFlashFX();
                // PlayRecoilAnimation();
            }
        }

        // Play Sound FX
        private void PlayFireSoundFX()
        {
            fireSoundFX.Play();
        }

        // Muzzle Flash FX
        private void PlayMuzzleFlashFX()
        {
            muzzleFlashFX.SetActive(true);
        }

        // Recoil
        private void PlayRecoilAnimation()
        {
            recoilAnimation.Play();
        }

        // Reload
        public void Reload()
        {
            reloadStartTime = Time.time;
            StartCoroutine(ReloadCoroutine());
        }

        private IEnumerator ReloadCoroutine()
        {
            yield return new WaitUntil(() => Reloading == false);
            ProjectilesInClip = clipSize;
        }

        // Pooling
        private IProjectile GetProjectile()
        {
            if (!projectiles.TryDequeue(out Projectile projectile))
            {
                projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                projectile.ReturnToPool = ReturnToPool;
            }
            return projectile;
        }

        private void ReturnToPool(Projectile projectile)
        {
            projectiles.Enqueue(projectile);
            projectile.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }
    }
}