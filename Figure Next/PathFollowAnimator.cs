using UnityEngine;
using DG.Tweening;

internal class PathFollowAnimator : MonoBehaviour
{
    [Header("Object To Animate")]
    [SerializeField] private Transform lookAtTarget;
    [SerializeField] private Transform objectToAnimate;

    [Header("Path Settings")]
    [SerializeField] private Transform[] path;
    [SerializeField] private float duration = 10;
    [SerializeField] private int resolution = 10;
    [SerializeField] private PathType pathType = PathType.CatmullRom;
    [SerializeField] private PathMode pathMode = PathMode.Full3D;

    private Vector3 startPosition;
    private Tween followPathAnimation;

    private Vector3[] GetPositionsAlongPath()
    {
        Vector3[] p = new Vector3[path.Length];
        for (int i = 0; i < p.Length; i++)
            p[i] = path[i].position;
        return p;
    }

    private void Awake() => startPosition = objectToAnimate.position;

    //Button
    public void AnimateAlongPath()
    {
        followPathAnimation?.Kill();
        objectToAnimate.position = startPosition;
        followPathAnimation = objectToAnimate.DOPath(GetPositionsAlongPath(),
                                                 duration, pathType, pathMode,
                                                 resolution, curveGizmoColor).
                                                 SetLookAt(lookAtTarget).SetLoops(-1);
    }

    #region Gizmos
    [Header("Gizmos")]
    [SerializeField] private bool objectGizmo;
    [SerializeField] private bool lineGizmo;
    [SerializeField] private bool lookAtGizmo;
    [SerializeField] private bool waypointGizmo;

    [SerializeField] private Color objectGizmoColor = Color.cyan;
    [SerializeField] private Color waypointGizmoColor = Color.blue;
    [SerializeField] private Color lineGizmoColor = Color.yellow;
    [SerializeField] private Color lookAtGizmoColor = Color.red;
    [SerializeField] private Color curveGizmoColor = Color.white;

    private void OnDrawGizmos()
    {
        if (objectGizmo)
        {
            Gizmos.color = objectGizmoColor;
            Gizmos.DrawSphere(objectToAnimate.position, 0.5f);
        }

        if (lookAtGizmo)
        {
            Gizmos.color = lookAtGizmoColor;
            Gizmos.DrawSphere(lookAtTarget.position, 0.5f);
        }

        for (int i = 0; i < path.Length; i++)
        {
            if (waypointGizmo)
            {
                Gizmos.color = waypointGizmoColor;
                Gizmos.DrawSphere(path[i].position, 0.5f);
            }

            if (lookAtGizmo)
            {
                Gizmos.color = lookAtGizmoColor;
                Gizmos.DrawLine(path[i].position, lookAtTarget.position);
            }

            if (lineGizmo)
            {
                if (i + 1 < path.Length)
                {
                    Gizmos.color = lineGizmoColor;
                    Gizmos.DrawLine(path[i].position, path[i + 1].position);
                }
            }
        }
        #endregion
    }
}