using Utils;
using UnityEngine;

namespace Steerings
{
    public class InterposePlusAvoidObstacles : SteeringBehaviour
    {
        public SInterpose m_interposeInfo;
        public SArrive m_arriveInfo;
        public SObstacleAvoidance m_obstacleAvoidanceInfo;
        private bool m_obstacleAvoided;

        protected override SteeringOutput GetSteering()
        {
            SteeringOutput result = InterposePlusAvoidObstacles.GetSteering(m_ownKS, m_interposeInfo, m_obstacleAvoidanceInfo, m_arriveInfo, ref m_obstacleAvoided);
            base.ApplyFacingPolicy(m_obstacleAvoided ? m_obstacleAvoidanceInfo.m_facingPolicy : m_interposeInfo.m_facingPolicy, result);

            return result;
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, SInterpose interposeInfo, SObstacleAvoidance obstacleInfo, SArrive arriveInfo, ref bool obstacleAvoided)
        {
            SteeringOutput obstacleAvoidSteering = ObstacleAvoidance.GetSteering(ownKS, obstacleInfo);

            if (obstacleAvoidSteering != NULL_STEERING)
            {
                obstacleAvoided = true;
                return obstacleAvoidSteering;
            }

            obstacleAvoided = false;
            return Interpose.GetSteering(ownKS, interposeInfo, arriveInfo);
        }
    }
}
