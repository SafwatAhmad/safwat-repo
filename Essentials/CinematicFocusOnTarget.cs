using UnityEngine;
using DG.Tweening;

namespace BattleShip.WeaponSystem.Camera
{
    internal class CinematicFocusOnTarget : MonoBehaviour
    {
        [SerializeField] private float time = 5;
        [SerializeField] private float stopDistance = 20;

        internal void FocusOnTarget(Transform target, TweenCallback onReachedCallback)
        {
            Vector3 direction = (target.position - transform.position).normalized; //normalized vector from A to B
            Vector3 point = target.position - direction * stopDistance; //point at distance from B

            transform.LookAt(target);
            transform.DOMove(point, time).OnComplete(onReachedCallback).SetEase(Ease.InOutSine);
        }
    }
}