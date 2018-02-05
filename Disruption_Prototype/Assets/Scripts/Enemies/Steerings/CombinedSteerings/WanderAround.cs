using UnityEngine;

namespace Steerings
{
    public class WanderAround : SteeringBehaviour
    {
        private float m_seekWeight;
        private SWander m_wanderInfo;
        private SSeek m_seekInfo;

        public void SetInfo(float seekWeight, SWander wanderInfo, SSeek seekInfo)
        {
            m_seekWeight = seekWeight;
            m_wanderInfo = wanderInfo;
            m_seekInfo = seekInfo;
        }

        protected override SteeringOutput GetSteering()
        {
            SteeringOutput result = WanderAround.GetSteering(m_ownKS, m_seekWeight, ref m_wanderInfo, m_seekInfo);
            base.ApplyFacingPolicy(m_wanderInfo.m_facingPolicy, result);

            return result;
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, float seekWeight, ref SWander wanderInfo, SSeek seekInfo)
        {
            SteeringOutput seekSteering = Seek.GetSteering(ownKS, seekInfo.m_target);
            SteeringOutput result = Wander.GetSteering(ownKS, ref wanderInfo);

            result.m_linearAcceleration = result.m_linearAcceleration * (1f - seekWeight) + seekSteering.m_linearAcceleration * seekWeight;

            return result;


        }
    }
}
