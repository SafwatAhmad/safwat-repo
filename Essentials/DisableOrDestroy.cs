using UnityEngine;

public class DisableOrDestroy : MonoBehaviour
{
    public bool destroy;
    [Range(0.01f, 5)] private float checkDelay = 1f;

    private void OnEnable() => Invoke(nameof(Check), checkDelay);

    private void Check()
    {
        if (destroy)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }
}