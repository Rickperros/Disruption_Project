using UnityEngine;

namespace Steerings
{
    public class KeepPositionPlusAvoidObstacles : SteeringBehaviour
    {
        public SKeepPosition m_keepPositionInfo;
        public SObstacleAvoidance m_avoidObstacleInfo;
        public SArrive m_arriveInfo;
        public bool m_useArrive = false;

        protected override SteeringOutput GetSteering()
        {
            SteeringOutput result = KeepPositionPlusAvoidObstacles.GetSteering(m_ownKS, m_keepPositionInfo, m_avoidObstacleInfo, m_arriveInfo, m_useArrive);
            base.ApplyFacingPolicy(m_keepPositionInfo.m_facingPolicy, result);

            return result;
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, SKeepPosition keepPositionInfo, SObstacleAvoidance avoidObstacles, SArrive arriveInfo, bool useArrive)
        {
            SteeringOutput result = useArrive ? KeepPosition.GetSteering(ownKS, keepPositionInfo, arriveInfo) : KeepPosition.GetSteering(ownKS, keepPositionInfo);

            if (result != NULL_STEERING)
                return result;

            result = ObstacleAvoidance.GetSteering(ownKS, avoidObstacles);
            return result;
        }
    }
}
