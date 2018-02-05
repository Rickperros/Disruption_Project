using Utils;
using UnityEngine;

namespace Steerings
{
    public class Wander : SteeringBehaviour
    {
        private SWander m_info;

        public void SetInfo(SWander info)
        {
            m_info = info;
        }

        public void SetInfo(EFacingPolicy facingPolicy, float targetOrientation, float wanderOffset, float wanderRate, float wanderRadius)
        {
            m_info.m_facingPolicy = facingPolicy;
            m_info.m_targetOrientation = targetOrientation;
            m_info.m_wanderOffset = wanderOffset;
            m_info.m_wanderRate = wanderRate;
            m_info.m_wanderRadius = wanderRadius;
        }

        protected override SteeringOutput GetSteering()
        {
            SteeringOutput result = Wander.GetSteering(m_ownKS, ref m_info);
            base.ApplyFacingPolicy(m_info.m_facingPolicy, result);

            return result;
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, ref SWander info)
        {
            info.m_targetOrientation += info.m_wanderRate * MathExtent.Binomial();
            SURROGATE_TARGET.transform.position = MathExtent.AngleToVector(info.m_targetOrientation) * info.m_wanderRadius;

            SURROGATE_TARGET.transform.position += ownKS.m_position + MathExtent.AngleToVector(ownKS.m_orientation) * info.m_wanderOffset;

            return Seek.GetSteering(ownKS, SURROGATE_TARGET);
        }
    }
}
