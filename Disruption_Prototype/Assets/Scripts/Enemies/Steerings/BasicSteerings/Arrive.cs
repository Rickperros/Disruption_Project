using UnityEngine;
using Utils;

namespace Steerings
{
    public class Arrive : SteeringBehaviour
    {
        SArriveParameters m_info;

        public void SetInfo(SArriveParameters info)
        {
            m_info = info;
        }

        public void SetInfo(EFacingPolicy facingPolicy, Transform target, float closeEnoughRadius, float slowDownRadius, float timeToDesiredSpeed = 0.1f)
        {
            m_info.m_facingPolicy = facingPolicy;
            m_info.m_target = target;
            m_info.m_closeEnoughRadius = closeEnoughRadius;
            m_info.m_slowDownRadius = slowDownRadius;
            m_info.m_timeToDesiredSpeed = timeToDesiredSpeed;
        }

        protected override SteeringOutput GetSteering()
        {
            SteeringOutput result = Arrive.GetSteering(m_ownKS, m_info);
            base.ApplyFacingPolicy(m_info.m_facingPolicy, result);

            return result;
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, SArriveParameters info)
        {
            Vector3 l_distanceToTarget = info.m_target.position - ownKS.m_position;

            if (l_distanceToTarget.magnitude < info.m_closeEnoughRadius)
                return NULL_STEERING;

            if (l_distanceToTarget.magnitude > info.m_slowDownRadius)
                return Seek.GetSteering(ownKS, info.m_target);

            float l_desiredSpeed = ownKS.m_maxLinearSpeed * (l_distanceToTarget.magnitude / info.m_slowDownRadius);
            Vector3 l_desiredVelocity = l_distanceToTarget.normalized * l_desiredSpeed;

            Vector3 l_requiredAcceleration = (l_desiredVelocity - ownKS.m_linearVelocity) / info.m_timeToDesiredSpeed;
            l_requiredAcceleration = MathExtent.Clip(l_requiredAcceleration, ownKS.m_maxLinearAcceleration);

            SteeringOutput result = new SteeringOutput();
            result.m_linearAcceleration = l_requiredAcceleration;

            return result;
        }
    }
}
