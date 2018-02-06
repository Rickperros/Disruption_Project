using UnityEngine;
using Utils;

namespace Steerings
{
    public class Intercept : SteeringBehaviour
    {
        public SIntercept m_info;

        public void SetInfo(SIntercept info)
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
            SteeringOutput l_result = Intercept.GetSteering(m_ownKS, m_info);
            base.ApplyFacingPolicy(m_info.m_facingPolicy, l_result);

            return l_result;
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, SIntercept info)
        {
            if (info.m_targetLinearVelocity == null)
                return NULL_STEERING;

            float l_predictedTimeToTarget = (info.m_target.position - ownKS.m_position).magnitude / ownKS.m_linearVelocity.magnitude;

            l_predictedTimeToTarget = MathExtent.Clip(l_predictedTimeToTarget, info.m_maxPredictionTime);

            SURROGATE_TARGET.position = info.m_target.position + (info.m_targetLinearVelocity * l_predictedTimeToTarget);

            return Seek.GetSteering(ownKS, SURROGATE_TARGET);
        }
    }
}
