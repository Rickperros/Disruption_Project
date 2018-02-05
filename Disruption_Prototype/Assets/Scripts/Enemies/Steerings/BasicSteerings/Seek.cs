using UnityEngine;

namespace Steerings
{
    public class Seek : SteeringBehaviour
    {
        private SSeekParameters m_info;

        public void SetInfo(SSeekParameters info)
        {
            m_info = info;
        }

        public void SetInfo(EFacingPolicy facingPolicy, Transform target)
        {
            m_info.m_facingPolicy = facingPolicy;
            m_info.m_target = target;
        }

        protected override SteeringOutput GetSteering()
        {
            SteeringOutput result = Seek.GetSteering(m_ownKS, m_info.m_target);
            base.ApplyFacingPolicy(m_info.m_facingPolicy, result);

            return result;
        }


        public static SteeringOutput GetSteering(KinematicState ownKS, Transform target)
        {
            SteeringOutput result = new SteeringOutput();
            result.m_linearAcceleration = (target.position - ownKS.m_position).normalized * ownKS.m_maxLinearAcceleration;

            return result;
        }
    }
}


