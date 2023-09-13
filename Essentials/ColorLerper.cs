using UnityEngine;
using UnityEngine.UI;

internal class ColorLerper : MonoBehaviour
{
    private int colorIndex = -1;
    [SerializeField] private Text target;
    [SerializeField] private float delay = 10f;
    [SerializeField] private float time = 5f;
    [SerializeField] private bool pickInOrder = true;
    [SerializeField]
    private Color[] colors =
        new Color[6] { Color.white, Color.cyan, Color.yellow,
                       Color.green, Color.red, Color.blue };

    private void Start() => SwitchColor();

    private void SwitchColor() =>
        iTween.ValueTo(gameObject,
        iTween.Hash(
                    "from", target.color,
                    "to", GetColor(),
                    "time", time,
                    "delay", delay,
                    "onupdate", nameof(UpdateColor),
                    "oncomplete", nameof(SwitchColor),
                    "onupdatetarget", gameObject,
                    "oncompletetarget", gameObject
                    ));

    private Color GetColor() => colors[colorIndex = pickInOrder ?
                                      (colorIndex + 1) % colors.Length :
                                       Random.Range(0, colors.Length)];

    private void UpdateColor(Color c) => target.color = c;
}