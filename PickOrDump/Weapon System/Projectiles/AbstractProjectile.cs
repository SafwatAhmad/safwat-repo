using UnityEngine;

namespace BattleShip.WeaponSystem
{
    public delegate void ReturnProjectileToPool(Projectile projectile);

    public interface IProjectile
    {
        ReturnProjectileToPool ReturnToPool { get; set; }
        void Launch(Vector3 direction);
    }

    public abstract class Projectile : MonoBehaviour, IProjectile
    {   //Using abstraction as a work around for unity serialization
        public abstract ReturnProjectileToPool ReturnToPool { get; set; }
        public abstract void Launch(Vector3 direction);
    }
}