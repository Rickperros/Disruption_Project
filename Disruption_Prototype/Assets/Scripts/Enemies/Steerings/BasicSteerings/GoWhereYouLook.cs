using Utils;
using UnityEngine;

namespace Steerings
{
    public class GoWhereYouLook : SteeringBehaviour
    {
        protected override SteeringOutput GetSteering()
        {
            SteeringOutput result = GoWhereYouLook.GetSteering(m_ownKS);
            base.ApplyFacingPolicy(EFacingPolicy.LWYGI, result);

            return result;
        }

        public static SteeringOutput GetSteering(KinematicState ownKS)
        {
            Vector3 l_direction = MathExtent.AngleToVector(ownKS.m_orientation);
            SURROGATE_TARGET.position = ownKS.m_position + l_direction;

            return Seek.GetSteering(ownKS, SURROGATE_TARGET);
        }
    }
}