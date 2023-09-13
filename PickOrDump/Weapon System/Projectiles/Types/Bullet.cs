using UnityEngine;

namespace BattleShip.WeaponSystem
{
    internal class Bullet : Projectile
    {
        [SerializeField] private int damage;
        [SerializeField] private float launchSpeed;
        [SerializeField] private float maxDistance;

        private bool isLaunched;
        private Vector3 direction;
        private Vector3 launchPosition;

        public override ReturnProjectileToPool ReturnToPool { get; set; }

        private void Update()
        {
            if (isLaunched)
            {
                transform.position = Vector3.MoveTowards(transform.position, direction * maxDistance, launchSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, launchPosition) >= maxDistance)
                    Reset();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITakeHit h))
            {
                h.TakeHit(damage);
                Reset();
            }
            else
            if (other.TryGetComponent(out IInteract i))
            {
                i.Interact();
                Reset();
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
            this.direction = direction;
            gameObject.SetActive(true);
        }

        private void Reset()
        {
            isLaunched = false;
            gameObject.SetActive(false);
            ((IProjectile)this).ReturnToPool?.Invoke(this);
        }
    }
}