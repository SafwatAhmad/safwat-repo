using UnityEngine;

internal class DemoExplosion : MonoBehaviour
{
    [SerializeField] private float force = 1000;
    [SerializeField] private float radius = 10;
    [SerializeField] private LayerMask layer;

    private void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layer, QueryTriggerInteraction.Collide);

        foreach (Collider col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
    }
}