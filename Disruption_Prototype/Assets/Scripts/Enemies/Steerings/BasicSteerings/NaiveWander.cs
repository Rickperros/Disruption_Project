using Utils;
using UnityEngine;

namespace Steerings
{
    public class NaiveWander : SteeringBehaviour
    {
        private SNaiveWander m_info;

        public void SetInfo(SNaiveWander info)
        {
            m_info = info;
        }

        public void SetInfo(EFacingPolicy facingPolicy, float wanderRate)
        {
            m_info.m_facingPolicy = facingPolicy;
            m_info.m_wanderRate = wanderRate;
        }

        protected override SteeringOutput GetSteering()
        {
            return NaiveWander.GetSteering(m_ownKS, m_info);
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, SNaiveWander info)
        {
            ownKS.m_orientation += info.m_wanderRate * MathExtent.Binomial();

            return GoWhereYouLook.GetSteering(ownKS);
        }
    }
}
