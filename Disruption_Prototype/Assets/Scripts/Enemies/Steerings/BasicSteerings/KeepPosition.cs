using Utils;
using UnityEngine;

namespace Steerings
{
    public class KeepPosition : SteeringBehaviour
    {
        public SKeepPosition m_keepPositioninfo;
        public SArrive m_arriveInfo;

        public void SetInfo(SKeepPosition info, SArrive arriveInfo)
        {
            m_keepPositioninfo = info;
            m_arriveInfo = arriveInfo;
        }

        protected override SteeringOutput GetSteering()
        {
            SteeringOutput result = KeepPosition.GetSteering(m_ownKS, m_keepPositioninfo, m_arriveInfo);
            base.ApplyFacingPolicy(m_keepPositioninfo.m_facingPolicy, result);

            return result;
        }

        public static SteeringOutput GetSteering(KinematicState me, SKeepPosition info, SArrive arriveInfo)
        {
            float targetOrientation = info.m_target.transform.eulerAngles.y;
            targetOrientation += info.m_requiredAngle; 

            Vector3 finalTargetPosition = MathExtent.AngleToVector(targetOrientation).normalized; 

            finalTargetPosition *= info.m_requiredDistance; 

            SURROGATE_TARGET.position = info.m_target.position + finalTargetPosition;
            arriveInfo.m_target = SURROGATE_TARGET;

            return Arrive.GetSteering(me, arriveInfo); 
        }
    }
}
