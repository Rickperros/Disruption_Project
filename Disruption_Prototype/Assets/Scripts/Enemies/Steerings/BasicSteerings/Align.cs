using Utils;
using UnityEngine;

namespace Steerings
{
    public class Align : SteeringBehaviour
    {
        private SAlignParameters m_info;

        public void SetInfo(SAlignParameters info)
        {
            m_info = info;
        }

        public void SetInfo(Transform target, float closeEnoughRadius, float slowDownRadius, float timeToDesiredSpeed = 0.1f)
        {
            m_info.m_closeEnoughAngle = closeEnoughRadius;
            m_info.m_slowDownAngle = slowDownRadius;
            m_info.m_target = target;
            m_info.m_timeToDesiredAngularSpeed = timeToDesiredSpeed;
        }

        protected override SteeringOutput GetSteering()
        {
            return Align.GetSteering(m_ownKS, m_info);
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, SAlignParameters info)
        {
            float l_requiredAngularSpeed;
            float l_requiredAngularAcceleration;
            float l_angleSize;

            float l_requiredRotation = info.m_target.rotation.eulerAngles.y - ownKS.m_orientation;

            if (l_requiredRotation < 0f)
                l_requiredRotation += 360f;

            if (l_requiredRotation > 180f)
                l_requiredRotation = -(360f - l_requiredRotation);

            l_angleSize = Mathf.Abs(l_requiredRotation);

            if (l_angleSize <= info.m_closeEnoughAngle)
                return NULL_STEERING;

            if (l_angleSize > info.m_slowDownAngle)
                l_requiredAngularSpeed = ownKS.m_maxAngularSpeed;
            else
                l_requiredAngularSpeed = ownKS.m_maxAngularSpeed * (l_angleSize - info.m_slowDownAngle);

            l_requiredAngularSpeed *= Mathf.Sign(l_requiredRotation);
            l_requiredAngularAcceleration = (l_requiredAngularSpeed - ownKS.m_angularSpeed) / info.m_timeToDesiredAngularSpeed;

            l_requiredAngularAcceleration = MathExtent.Clip(l_requiredAngularAcceleration, ownKS.m_maxAngularAcceleration, true);

            SteeringOutput result = new SteeringOutput();
            result.m_angularAcceleration = l_requiredAngularAcceleration;
            return result;

        }
    }
}
