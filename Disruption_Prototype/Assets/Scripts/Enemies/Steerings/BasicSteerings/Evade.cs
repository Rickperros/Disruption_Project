using UnityEngine;
using Utils;

namespace Steerings
{
    public class Evade : SteeringBehaviour
    {
        public SEvade m_info;

        public void SetInfo(SEvade info)
        {
            m_info = info;
        }

        public void SetInfo(EFacingPolicy facingPolicy, Transform target, float maxPredictionTime, Vector3 targetLinearVelocity)
        {
            m_info.m_facingPolicy = facingPolicy;
            m_info.m_maxPredictionTime = maxPredictionTime;
            m_info.m_targetLinearVelocity = targetLinearVelocity;
            m_info.m_target = target;
        }

        protected override SteeringOutput GetSteering()
        {
            SteeringOutput l_result = Evade.GetSteering(m_ownKS, m_info);
            base.ApplyFacingPolicy(m_info.m_facingPolicy, l_result);

            return l_result;
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, SEvade info)
        {
            if (info.m_targetLinearVelocity == null)
                return NULL_STEERING;

            float l_distanceToMe = (ownKS.m_position - info.m_target.position).magnitude;
            float l_predictedCollisionTime = l_distanceToMe / info.m_targetLinearVelocity.magnitude;

            l_predictedCollisionTime = MathExtent.Clip(l_predictedCollisionTime, info.m_maxPredictionTime);

            SURROGATE_TARGET.position = info.m_target.position + info.m_targetLinearVelocity * l_predictedCollisionTime;

            return Flee.GetSteering(ownKS, SURROGATE_TARGET);
        }
    }
}
