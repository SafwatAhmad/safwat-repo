using UnityEngine;
using DG.Tweening;

internal class PathFollowAnimator : MonoBehaviour
{
    [SerializeField] private Transform objectToAnimate;

    [Header("Path Settings")]
    [SerializeField] private Transform[] path;
    [SerializeField] private int loops = -1;
    [SerializeField] private float duration = 10;
    [SerializeField] private int resolution = 10;
    [SerializeField][Range(0, 1)] private float lookAhead = 0.01f;
    [SerializeField] private PathType pathType = PathType.CatmullRom;
    [SerializeField] private PathMode pathMode = PathMode.Full3D;

    private Vector3 startPosition;
    private Tween tween;

    private Vector3[] GetPositionsAlongPath()
    {
        Vector3[] p = new Vector3[path.Length];
        for (int i = 0; i < p.Length; i++)
            p[i] = path[i].position;
        return p;
    }

    private void Awake() => startPosition = objectToAnimate.position;

    public void AnimateAlongPath()
    {
        tween?.Kill();
        objectToAnimate.position = startPosition;
        tween = objectToAnimate.DOPath(GetPositionsAlongPath(),
                                                 duration, pathType, pathMode,
                                                 resolution, tweenPathGizmoColor).
                                                 SetLookAt(lookAhead).SetLoops(loops);
    }

    #region Gizmos
    [Header("Gizmos")]
    [SerializeField] private bool lineGizmo;
    [SerializeField] private bool waypointGizmo;

    [SerializeField] private Color waypointGizmoColor = Color.blue;
    [SerializeField] private Color lineGizmoColor = Color.yellow;
    [SerializeField] private Color tweenPathGizmoColor = Color.white;

    private void OnDrawGizmos()
    {
        for (int i = 0; i < path.Length; i++)
        {
            if (waypointGizmo)
            {
                Gizmos.color = waypointGizmoColor;
                Gizmos.DrawSphere(path[i].position, 0.5f);
            }

            if (lineGizmo)
            {
                Gizmos.color = lineGizmoColor;
                if (i + 1 < path.Length)
                    Gizmos.DrawLine(path[i].position, path[i + 1].position);
                else if (loops != 0)
                    Gizmos.DrawLine(path[i].position, path[0].position);
            }
        }
        #endregion
    }
}