using UnityEngine;

namespace Steerings
{
    public class Flee : SteeringBehaviour
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
            SteeringOutput result = Flee.GetSteering(m_ownKS, m_info.m_target);
            base.ApplyFacingPolicy(m_info.m_facingPolicy, result);

            return result;
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, Transform target)
        {
            SteeringOutput result = Seek.GetSteering(ownKS, target);
            result.m_linearAcceleration *= -1;

            return result;
        }
    }
}
