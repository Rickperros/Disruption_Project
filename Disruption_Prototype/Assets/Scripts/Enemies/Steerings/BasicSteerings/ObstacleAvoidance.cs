using Utils;
using UnityEngine;

namespace Steerings
{
    public class ObstacleAvoidance : SteeringBehaviour
    {
        public SObstacleAvoidance m_info;

        public void SetInfo(SObstacleAvoidance info)
        {
            m_info = info;
        }

        public void SetInfo(EFacingPolicy facingPolicy, float whiskerLenght, float secondaryWhiskerAngle, float ratioSecondaryWhisker)
        {
            m_info.m_facingPolicy = facingPolicy;
            m_info.m_primaryWhiskerLenght = whiskerLenght;
            m_info.m_AngleSecondWhisk = secondaryWhiskerAngle;
            m_info.m_RatioSecondWhisk = ratioSecondaryWhisker;
        }

        protected override SteeringOutput GetSteering()
        {
            SteeringOutput result = ObstacleAvoidance.GetSteering(m_ownKS, m_info);
            base.ApplyFacingPolicy(m_info.m_facingPolicy, result);

            return result;
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, SObstacleAvoidance info)
        {
            Ray l_ray = new Ray(ownKS.m_position, ownKS.m_linearVelocity.normalized);
            RaycastHit l_hitInfo;

            if(Physics.Raycast(l_ray, out l_hitInfo, info.m_avoidDistance))
            {
                SURROGATE_TARGET.position = l_hitInfo.point + l_hitInfo.normal * info.m_avoidDistance;
                return Seek.GetSteering(ownKS, SURROGATE_TARGET);
            }

            l_ray = new Ray(ownKS.m_position, MathExtent.AngleToVector(MathExtent.VectorToAngle(ownKS.m_linearVelocity.normalized) + info.m_AngleSecondWhisk));

            if(Physics.Raycast(l_ray, out l_hitInfo, info.m_primaryWhiskerLenght * info.m_RatioSecondWhisk))
            {
                SURROGATE_TARGET.position = l_hitInfo.point + l_hitInfo.normal * info.m_avoidDistance;
                return Seek.GetSteering(ownKS, SURROGATE_TARGET);
            }

            l_ray = new Ray(ownKS.m_position, MathExtent.AngleToVector(MathExtent.VectorToAngle(ownKS.m_linearVelocity.normalized) - info.m_AngleSecondWhisk));

            if (Physics.Raycast(l_ray, out l_hitInfo, info.m_primaryWhiskerLenght * info.m_RatioSecondWhisk))
            {
                SURROGATE_TARGET.position = l_hitInfo.point + l_hitInfo.normal * info.m_avoidDistance;
                return Seek.GetSteering(ownKS, SURROGATE_TARGET);
            }

            return NULL_STEERING;
        }

    }
}
