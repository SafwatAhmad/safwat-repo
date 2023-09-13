using UnityEngine;
using UnityEngine.EventSystems;

internal class TouchFX : MonoBehaviour
{
    [SerializeField] private GameObject touchFXPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //if (EventSystem.current.currentSelectedGameObject != null)
            {
                Vector3 screenPos = Input.mousePosition;
                screenPos.z = 100f;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

                var fx = Instantiate(touchFXPrefab);
                fx.transform.position = worldPos;
                fx.transform.parent = transform;
                fx.AddComponent<DestroyOnAnimationComplete>();
            }
        }
    }
}