#if !UNITY_2019_3_OR_NEWER
#define CINEMACHINE_PHYSICS
#define CINEMACHINE_PHYSICS_2D
#endif

using UnityEngine;
using System.Collections.Generic;
using Cinemachine.Utility;
using System;

namespace Cinemachine
{
    public class CustomConfiner : CinemachineExtension
    {
        /// <summary>The volume within which the camera is to be contained.</summary>
        [Tooltip("The volume within which the camera is to be contained")]
        public Collider m_BoundingVolume;

        /// <summary>If camera is orthographic, screen edges will be confined to the volume.</summary>
        [Tooltip("If camera is orthographic, screen edges will be confined to the volume.  "
            + "If not checked, then only the camera center will be confined")]
        public bool m_ConfineScreenEdges = true;

        /// <summary>How gradually to return the camera to the bounding volume if it goes beyond the borders</summary>
        [Tooltip("How gradually to return the camera to the bounding volume if it goes beyond the borders.  "
            + "Higher numbers are more gradual.")]
        [Range(0, 10)]
        public float m_Damping = 0;

        /// <summary>See whether the virtual camera has been moved by the confiner</summary>
        /// <param name="vcam">The virtual camera in question.  This might be different from the
        /// virtual camera that owns the confiner, in the event that the camera has children</param>
        /// <returns>True if the virtual camera has been repositioned</returns>
        public bool CameraWasDisplaced(CinemachineVirtualCameraBase vcam)
        {
            return GetCameraDisplacementDistance(vcam) > 0;
        }

        /// <summary>See how far virtual camera has been moved by the confiner</summary>
        /// <param name="vcam">The virtual camera in question.  This might be different from the
        /// virtual camera that owns the confiner, in the event that the camera has children</param>
        /// <returns>True if the virtual camera has been repositioned</returns>
        public float GetCameraDisplacementDistance(CinemachineVirtualCameraBase vcam)
        {
            return GetExtraState<VcamExtraState>(vcam).confinerDisplacement;
        }
        
        private void OnValidate()
        {
            m_Damping = Mathf.Max(0, m_Damping);
        }

        /// <summary>
        /// Called when connecting to a virtual camera
        /// </summary>
        /// <param name="connect">True if connecting, false if disconnecting</param>
        protected override void ConnectToVcam(bool connect)
        {
            base.ConnectToVcam(connect);
        }

        class VcamExtraState
        {
            public Vector3 m_previousDisplacement;
            public float confinerDisplacement;
        };

        /// <summary>Check if the bounding volume is defined</summary>
        public bool IsValid
        {
            get
            {
                return m_BoundingVolume != null;
            }
        }

        /// <summary>
        /// Report maximum damping time needed for this component.
        /// </summary>
        /// <returns>Highest damping setting in this component</returns>
        public override float GetMaxDampTime() 
        { 
            return m_Damping;
        }

        /// <summary>
        /// Callback to do the camera confining
        /// </summary>
        /// <param name="vcam">The virtual camera being processed</param>
        /// <param name="stage">The current pipeline stage</param>
        /// <param name="state">The current virtual camera state</param>
        /// <param name="deltaTime">The current applicable deltaTime</param>
        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (IsValid && stage == CinemachineCore.Stage.Body)
            {
                var extra = GetExtraState<VcamExtraState>(vcam);
                Vector3 displacement;
                if (m_ConfineScreenEdges && state.Lens.Orthographic)
                    displacement = ConfineScreenEdges(vcam, ref state);
                else
                    displacement = ConfinePoint(state.CorrectedPosition);

                if (m_Damping > 0 && deltaTime >= 0 && VirtualCamera.PreviousStateIsValid)
                {
                    Vector3 delta = displacement - extra.m_previousDisplacement;
                    delta = Damper.Damp(delta, m_Damping, deltaTime);
                    displacement = extra.m_previousDisplacement + delta;
                }
                extra.m_previousDisplacement = displacement;
                state.PositionCorrection += displacement;
                extra.confinerDisplacement = displacement.magnitude;
            }
        }

        //Brings the camera back to the confinement point
        private Vector3 ConfinePoint(Vector3 camPos)
        {
            // 3D version
            return  m_BoundingVolume.ClosestPoint(camPos) - camPos;
        }

        // Camera must be orthographic
        private Vector3 ConfineScreenEdges(CinemachineVirtualCameraBase vcam, ref CameraState state)
        {
            Quaternion rot = Quaternion.Inverse(state.CorrectedOrientation);
            float dy = state.Lens.OrthographicSize;
            float dx = dy * state.Lens.Aspect;
            Vector3 vx = (rot * Vector3.right) * dx;
            Vector3 vy = (rot * Vector3.up) * dy;

            Vector3 displacement = Vector3.zero;
            Vector3 camPos = state.CorrectedPosition;
            Vector3 lastD = Vector3.zero;
            const int kMaxIter = 12;
            for (int i = 0; i < kMaxIter; ++i)
            {
                Vector3 d = ConfinePoint((camPos - vy) - vx);
                if (d.AlmostZero())
                    d = ConfinePoint((camPos + vy) + vx);
                if (d.AlmostZero())
                    d = ConfinePoint((camPos - vy) + vx);
                if (d.AlmostZero())
                    d = ConfinePoint((camPos + vy) - vx);
                if (d.AlmostZero())
                    break;
                if ((d + lastD).AlmostZero())
                {
                    displacement += d * 0.5f;  // confiner too small: center it
                    break;
                }
                displacement += d;
                camPos += d;
                lastD = d;
            }
            return displacement;
        }
    }
}

