using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

internal class PathFollowAnimator : MonoBehaviour
{
    [SerializeField] private Transform transformToMove;
    [SerializeField] private Transform[] path;
    [SerializeField] private float time = 10f;
    [SerializeField] private float startDelay = 0f;
    [SerializeField][Range(0, 1)] private float lookAhead = 0.01f;
    [SerializeField] private int loopCount = -1;
    [SerializeField] private Ease easeType = Ease.Linear;
    [SerializeField] private PathMode pathMode = PathMode.Full3D;
    [SerializeField] private PathType pathType = PathType.CatmullRom;

    private Vector3 startPosition;
    private Tween followPathAnimation;
    private BezierCurveEditor bezierCurveEditor;

    private Vector3[] GetPositionsAlongPath
    {
        get
        {
            int count = path.Length;
            Vector3[] positions = new Vector3[count];
            for (int i = 0; i < count; i++)
                positions[i] = path[i].position;
            return positions;
        }
    }

    private void Awake()
    {
        if (pathType == PathType.CubicBezier)
            bezierCurveEditor = GetComponent<BezierCurveEditor>();
    }

    private void Start()
    {
        //Test
        StartFollowingPath();
        startPosition = transformToMove.position;
    }

    public void StartFollowingPath()
    {
        StopFollowingPath();
        transformToMove.position = startPosition;

        Vector3[] positions = pathType == PathType.CubicBezier ?
                                          bezierCurveEditor.GetPositionsAlongPath :
                                          GetPositionsAlongPath;

        int pathResolution = 10;  //default
        followPathAnimation = transformToMove.DOPath(positions,
                                        time, pathType, pathMode,
                                        pathResolution, Color.magenta).
                                        SetLookAt(lookAhead).SetLoops(loopCount).
                                        SetEase(easeType).SetDelay(startDelay);
    }

    public void StopFollowingPath() => followPathAnimation?.Kill();

    #region Gizmos
    [Header("Gizmos")]
    [SerializeField] private float waypointGizmoSize = 1f;

    [SerializeField] private bool lineGizmo;
    [SerializeField] private bool waypointGizmo;

    [SerializeField] private Color waypointGizmoColor = Color.blue;
    [SerializeField] private Color lineGizmoColor = Color.yellow;

    private void OnDrawGizmos()
    {
        if (lineGizmo && path.Length > 0)
        {
            Gizmos.color = lineGizmoColor;
            Gizmos.DrawLine(transformToMove.position, path[0].position);
        }

        for (int i = 0; i < path.Length; i++)
        {
            if (waypointGizmo)
            {
                Gizmos.color = waypointGizmoColor;
                Gizmos.DrawSphere(path[i].position, waypointGizmoSize);
            }

            if (lineGizmo)
            {
                Gizmos.color = lineGizmoColor;
                if (i + 1 < path.Length)
                    Gizmos.DrawLine(path[i].position, path[i + 1].position);
                else
                if (loopCount != 0)
                    Gizmos.DrawLine(path[i].position, path[0].position);
            }
        }
    }
    #endregion
}