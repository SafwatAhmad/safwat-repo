using UnityEngine;

namespace Safwat.Essentials
{
    internal class ApplicationSettings : MonoBehaviour
    {
        [SerializeField] private int targetFrameRate = 300;
        [SerializeField] private bool multiTouchEnabled = true;

        private void Awake()
        {
            Input.multiTouchEnabled = multiTouchEnabled;
            Application.targetFrameRate = targetFrameRate;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}