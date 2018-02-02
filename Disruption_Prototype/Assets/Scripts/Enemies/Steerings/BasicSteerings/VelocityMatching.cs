using UnityEngine;
using Utils;

namespace Steerings
{
    public class VelocityMatching : SteeringBehaviour
    {
        SVelocityMatchingParameters m_info;
        
        public void SetInfo(SVelocityMatchingParameters info)
        {
            m_info = info;
        }

        public void SetInfo(EFacingPolicy facingPolicy, Vector3 targetLinearVelocity, float timeToDesiredSpeed)
        {
            m_info.m_facingPolicy = facingPolicy;
            m_info.m_targetLinearVelocity = targetLinearVelocity;
            m_info.m_timeToDesiredVelocity = timeToDesiredSpeed;
        }

        protected override SteeringOutput GetSteering()
        {
            SteeringOutput l_result = VelocityMatching.GetSteering(m_ownKS, m_info);

            ApplyFacingPolicy(m_info.m_facingPolicy, l_result);
            return l_result;
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, SVelocityMatchingParameters info)
        {
            if (info.m_targetLinearVelocity == null)
                return NULL_STEERING;

            Vector3 l_requiredAcceleration = (info.m_targetLinearVelocity - ownKS.m_linearVelocity) / info.m_timeToDesiredVelocity;
            l_requiredAcceleration = MathExtent.Clip(l_requiredAcceleration, ownKS.m_maxLinearAcceleration);

            SteeringOutput l_result = new SteeringOutput();
            l_result.m_linearAcceleration = l_requiredAcceleration;

            return l_result;
        }
    }
}