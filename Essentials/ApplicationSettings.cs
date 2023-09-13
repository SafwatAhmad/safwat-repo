using UnityEngine;

namespace Essentials
{
	internal class ApplicationSettings : MonoBehaviour
	{
		private void Awake()
		{
			Input.multiTouchEnabled = false;
			Application.targetFrameRate = 999;
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}
	}
}