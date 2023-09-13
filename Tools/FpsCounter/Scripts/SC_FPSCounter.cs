using UnityEngine;
using UnityEngine.UI;

public class SC_FPSCounter : MonoBehaviour
{
    private int frames;
    private float fps;
    private float accum;
    private float timeleft;
    [SerializeField] private float updateInterval = 0.5f;
    [SerializeField] private Text UIText;
    private GUIStyle textStyle = new GUIStyle();

    private void Awake() => DontDestroyOnLoad(this);

    private void Start()
    {
        timeleft = updateInterval;
        textStyle.fontSize = 50;
        textStyle.fontStyle = FontStyle.Bold;
        textStyle.normal.textColor = Color.green;
    }

    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;
        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            fps = (accum / frames);
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
        UIText.text = "FPS : " + fps.ToString("F0");
    }
}