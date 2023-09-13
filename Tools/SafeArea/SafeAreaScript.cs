using UnityEngine;

internal class SafeAreaScript : MonoBehaviour
{
    private Vector2 minAnchor;
    private Vector2 maxAnchor;

    private Rect safeArea;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        safeArea = UnityEngine.Screen.safeArea;

        minAnchor = safeArea.position;
        maxAnchor = minAnchor + safeArea.size;

        minAnchor.x /= UnityEngine.Screen.width;
        minAnchor.y /= UnityEngine.Screen.height;
        maxAnchor.x /= UnityEngine.Screen.width;
        maxAnchor.y /= UnityEngine.Screen.height;

        rectTransform.anchorMin = minAnchor;
        rectTransform.anchorMax = maxAnchor;
    }
}