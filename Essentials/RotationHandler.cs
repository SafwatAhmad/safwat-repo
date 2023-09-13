using System;
using UnityEngine;

namespace BattleShip.WeaponSystem.Camera
{
    internal class RotationHandler : MonoBehaviour
    {
        [SerializeField] private Transform xAxisPivot;
        [SerializeField] private Transform yAxisPivot;
        [SerializeField] private RotationRanges rotationRanges;

        internal void HandleXAxisRotation(float value)
        {
            if (value < 0 && rotationRanges.xAxis.current > rotationRanges.xAxis.min)
                rotationRanges.xAxis.current -= rotationRanges.xAxis.sensitivity * Mathf.Abs(value);

            if (value > 0 && rotationRanges.xAxis.current < rotationRanges.xAxis.max)
                rotationRanges.xAxis.current += rotationRanges.xAxis.sensitivity * Mathf.Abs(value);

            float xCalmped = Mathf.Clamp(
                                    rotationRanges.xAxis.current,
                                    rotationRanges.xAxis.min,
                                    rotationRanges.xAxis.max);

            xAxisPivot.localRotation = Quaternion.Euler(xCalmped, 0, 0);
        }

        internal void HandleYAxisRotation(float value)
        {
            if (value < 0 && rotationRanges.yAxis.current > rotationRanges.yAxis.min)
                rotationRanges.yAxis.current -= rotationRanges.yAxis.sensitivity * Mathf.Abs(value);

            if (value > 0 && rotationRanges.yAxis.current < rotationRanges.yAxis.max)
                rotationRanges.yAxis.current += rotationRanges.yAxis.sensitivity * Mathf.Abs(value);

            float yCalmped = Mathf.Clamp(
                                    rotationRanges.yAxis.current,
                                    rotationRanges.yAxis.min,
                                    rotationRanges.yAxis.max);

            yAxisPivot.localRotation = Quaternion.Euler(0, yCalmped, 0);
        }

        internal void ResetPivots()
        {
            rotationRanges.xAxis.current =
            rotationRanges.yAxis.current = 0;

            xAxisPivot.localRotation =
            yAxisPivot.localRotation = Quaternion.identity;
        }

        internal void ChangeSensitivity(float value)
        {
            rotationRanges.yAxis.sensitivity =
            rotationRanges.xAxis.sensitivity = value;
        }

        [Serializable]
        internal struct RotationRanges
        {
            [Serializable]
            internal struct Range
            {
                public float min;
                public float max;
                internal float current;
                [Range(0.1f, 2)] public float sensitivity;
            }

            public Range xAxis;
            public Range yAxis;
        }
    }
}