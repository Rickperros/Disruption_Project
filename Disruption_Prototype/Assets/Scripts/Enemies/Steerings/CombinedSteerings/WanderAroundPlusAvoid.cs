using UnityEngine;

namespace Steerings
{
    public class WanderAroundPlusAvoid : SteeringBehaviour
    {

        private float m_seekWeight;
        private SWander m_wanderInfo;
        private SSeek m_seekInfo;
        private SObstacleAvoidance m_obstacleAvoidanceInfo;
        private bool obstacleAvoided;

        public void SetInfo(float seekWeight, SWander wander, SObstacleAvoidance obstacleAvoidance, SSeek seekInfo)
        {
            m_seekWeight = seekWeight;
            m_seekInfo = seekInfo;
            m_wanderInfo = wander;
            m_obstacleAvoidanceInfo = obstacleAvoidance;
            obstacleAvoided = false;
        }

        protected override SteeringOutput GetSteering()
        {
            SteeringOutput result = WanderAroundPlusAvoid.GetSteering(m_ownKS, m_seekWeight, ref m_wanderInfo, m_obstacleAvoidanceInfo, m_seekInfo, ref obstacleAvoided);
            base.ApplyFacingPolicy(obstacleAvoided ? m_obstacleAvoidanceInfo.m_facingPolicy : m_wanderInfo.m_facingPolicy, result);

            return result;
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, float seekWeight, ref SWander wanderInfo, SObstacleAvoidance obstacleInfo, SSeek seekInfo, ref bool obstacleAvoided)
        {
            SteeringOutput obstacleAvoidSteering = ObstacleAvoidance.GetSteering(ownKS, obstacleInfo);

            if (obstacleAvoidSteering != NULL_STEERING)
            {
                obstacleAvoided = true;
                return obstacleAvoidSteering;
            }

            obstacleAvoided = false;
            return WanderAround.GetSteering(ownKS, seekWeight, ref wanderInfo, seekInfo);
        }
    }
}
