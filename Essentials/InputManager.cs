using UnityEngine;
using ControlFreak2;

namespace Safwat.Essentials
{
    internal class InputManager : MonoBehaviour
    {
        internal float HorizontalSwipeDelta => (edgeMoveDelta != 0) ? edgeMoveDelta : CF2Input.GetAxis("Horizontal");

        internal float VerticalSwipeDelta => CF2Input.GetAxis("Vertical");

        internal bool Pressed => CF2Input.GetButtonDown("Touch");

        internal bool Holding => CF2Input.GetButton("Touch");

        internal bool Released => CF2Input.GetButtonUp("Touch");

        private float edgeMoveDelta;

        public void OnEnterEdge(float moveDelta) => edgeMoveDelta = moveDelta;

        public void OnExitEdge() => edgeMoveDelta = 0;
    }
}