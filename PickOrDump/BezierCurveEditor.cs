// using UnityEngine;
// using System.Collections.Generic;

// [ExecuteInEditMode]
// public class BezierCurveEditor : MonoBehaviour
// {
//     public Transform path;
//     private BezierPath bezierPath;

//     [Header("Gizmos")]
//     [Range(0, 5)] public float wpGizmoSize = 1f;
//     [Range(0, 5)] public float cpGizmoSize = 1f;
//     [Range(0, 5)] public float pathGizmoSize = 1f;

//     public Vector3[] GetPositionsAlongPath
//     {
//         get
//         {
//             List<Vector3> positions = new();
//             for (int i = 0; i < path.childCount; i++)
//             {
//                 Transform wp = path.GetChild(i);
//                 Transform cp1 = wp.GetChild(0);
//                 Transform cp2 = wp.GetChild(1);

//                 positions.Add(wp.position);
//                 positions.Add(cp1.position);
//                 positions.Add(cp2.position);
//             }
//             return positions.ToArray();
//         }
//     }

//     private void Update() => bezierPath = new BezierPath(GetPositionsAlongPath);

//     private void OnDrawGizmos()
//     {
//         if (bezierPath == null)
//             return;

//         Gizmos.color = Color.red;
//         for (float t = 0; t <= 1; t += 0.05f)
//             Gizmos.DrawSphere(bezierPath.GetPoint(t), pathGizmoSize);

//         for (int i = 0; i < bezierPath.waypoints.Count; i++)
//         {
//             Gizmos.color = Color.blue;
//             Gizmos.DrawSphere(bezierPath.waypoints[i], wpGizmoSize);
//         }

//         for (int i = 0; i < bezierPath.controlPoints.Count; i += 2)
//         {
//             Gizmos.color = Color.green;
//             Gizmos.DrawSphere(bezierPath.controlPoints[i], cpGizmoSize);
//             Gizmos.color = Color.green;
//             Gizmos.DrawSphere(bezierPath.controlPoints[i + 1], cpGizmoSize);
//             Gizmos.color = Color.yellow;
//             Gizmos.DrawLine(bezierPath.controlPoints[i], bezierPath.controlPoints[i + 1]);
//         }
//     }
// }

// public class BezierPath
// {
//     public List<Vector3> waypoints = new();
//     public List<Vector3> controlPoints = new();

//     public BezierPath(Vector3[] points)
//     {
//         waypoints.Clear();
//         controlPoints.Clear();

//         for (int i = 0; i < points.Length; i++)
//         {
//             if (i % 3 == 0)
//                 waypoints.Add(points[i]);
//             else
//                 controlPoints.Add(points[i]);
//         }
//     }

//     public Vector3 GetPoint(float t)
//     {
//         int i;
//         if (t >= 1f)
//         {
//             t = 1f;
//             i = waypoints.Count - 2;
//         }
//         else
//         {
//             t = Mathf.Clamp01(t) * (waypoints.Count - 1);
//             i = (int)t;
//             t -= i;
//         }
//         return Bezier.GetPoint(waypoints[i], controlPoints[2 * i], controlPoints[2 * i + 1], waypoints[i + 1], t);
//     }
// }

// public static class Bezier
// {
//     public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
//     {
//         t = Mathf.Clamp01(t);
//         float oneMinusT = 1f - t;
//         return oneMinusT * oneMinusT * oneMinusT * p0 +
//             3f * oneMinusT * oneMinusT * t * p1 +
//             3f * oneMinusT * t * t * p2 +
//             t * t * t * p3;
//     }
// }