using UnityEngine;

internal class DestroyOnAnimationComplete : MonoBehaviour
{
    private Animation m_animation;

    private void Awake() => m_animation = GetComponent<Animation>();

    private void LateUpdate()
    {
        if (!m_animation.isPlaying)
            Destroy(gameObject);
    }
}