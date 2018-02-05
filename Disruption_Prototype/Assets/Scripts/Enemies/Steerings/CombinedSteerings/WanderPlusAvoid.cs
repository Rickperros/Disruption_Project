using UnityEngine;

namespace Steerings
{
    public class WanderPlusAvoid : SteeringBehaviour
    {
        private SWander m_wanderInfo;
        private SObstacleAvoidance m_obstacleAvoidanceInfo;
        private bool obstacleAvoided;

        public void SetInfo(SWander wander, SObstacleAvoidance obstacleAvoidance)
        {
            m_wanderInfo = wander;
            m_obstacleAvoidanceInfo = obstacleAvoidance;
            obstacleAvoided = false;
        }

        protected override SteeringOutput GetSteering()
        {
            SteeringOutput result = WanderPlusAvoid.GetSteering(m_ownKS, ref m_wanderInfo, m_obstacleAvoidanceInfo, ref obstacleAvoided);
            base.ApplyFacingPolicy(obstacleAvoided ? m_obstacleAvoidanceInfo.m_facingPolicy : m_wanderInfo.m_facingPolicy, result);

            return result;
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, ref SWander wanderInfo, SObstacleAvoidance obstacleInfo, ref bool obstacleAvoided)
        {
            SteeringOutput obstacleAvoidSteering = ObstacleAvoidance.GetSteering(ownKS, obstacleInfo);

            if (obstacleAvoidSteering != NULL_STEERING)
            {
                obstacleAvoided = true;
                return obstacleAvoidSteering;
            }

            obstacleAvoided = false;
            return Wander.GetSteering(ownKS, ref wanderInfo);
        }
    }
}
