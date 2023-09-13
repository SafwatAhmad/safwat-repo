using UnityEngine;

namespace BattleShip.WeaponSystem
{
    internal class Shell : Projectile
    {
        [SerializeField] private int damage;
        [SerializeField] private int launchForce;
        [SerializeField] private float explosionRadius;
        [SerializeField] private Rigidbody m_rigidbody;
        [SerializeField] private float maxDistance;

        private bool isLaunched;
        private Vector3 launchPosition;

        public override ReturnProjectileToPool ReturnToPool { get; set; }

        private void Update()
        {
            if (isLaunched)
                if (Vector3.Distance(transform.position, launchPosition) >= maxDistance)
                    Reset();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITakeHit h))
            {
                Explode();
            }
            else
            if (other.TryGetComponent(out IInteract i))
            {
                i.Interact();
                Explode();
            }
            else
            if (other.TryGetComponent(out ITrigger t))
            {
                t.Trigger();
            }
        }

        public override void Launch(Vector3 direction)
        {
            isLaunched = true;
            gameObject.SetActive(true);
            launchPosition = transform.position;
            m_rigidbody.AddForce(direction * launchForce, ForceMode.Impulse);
        }

        private void Explode()
        {
            // Disable Mesh & Collider
            // Explode
            // Deal Damage
            Debug.LogError($"Exploded");
            Reset();
        }

        private void Reset()
        {
            isLaunched = false;
            gameObject.SetActive(false);
            ((IProjectile)this).ReturnToPool?.Invoke(this);
        }
    }
}