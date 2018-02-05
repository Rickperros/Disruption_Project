using UnityEngine;

namespace Steerings
{
    public class SimpleObstacleAvoidance : SteeringBehaviour
    {
        private SSimpleObstacleAvoidance m_info;

        public void SetInfo(SSimpleObstacleAvoidance info)
        {
            m_info = info;
        }

        public void SetInfo(EFacingPolicy facingPolicy, float whiskerLenght, float avoidDistance)
        {
            m_info.m_avoidDistance = avoidDistance;
            m_info.m_facingPolicy = facingPolicy;
            m_info.m_whiskerLenght = whiskerLenght;
        }

        protected override SteeringOutput GetSteering()
        {
            SteeringOutput result = SimpleObstacleAvoidance.GetSteering(m_ownKS, m_info);
            base.ApplyFacingPolicy(m_info.m_facingPolicy, result);

            return result;
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, SSimpleObstacleAvoidance info)
        {
            Ray l_ray = new Ray(ownKS.m_position, ownKS.m_linearVelocity.normalized);
            RaycastHit l_hitInfo;

            if (!Physics.Raycast(l_ray, out l_hitInfo, info.m_avoidDistance))
                return NULL_STEERING;

            SURROGATE_TARGET.position = l_hitInfo.point + l_hitInfo.normal * info.m_avoidDistance;

            return Seek.GetSteering(ownKS, SURROGATE_TARGET);
        }
    }
}
