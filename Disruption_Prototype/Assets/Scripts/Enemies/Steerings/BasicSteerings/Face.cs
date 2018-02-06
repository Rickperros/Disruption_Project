using Utils;
using UnityEngine;

namespace Steerings
{
    public class Face : SteeringBehaviour
    {
        public SAlign m_info;

        public void SetInfo(SAlign info)
        {
            m_info = info;
        }

        public void SetInfo(Transform target, float closeEnoughRadius, float slowDownRadius, float timeToDesiredSpeed = 0.1f)
        {
            m_info.m_closeEnoughAngle = closeEnoughRadius;
            m_info.m_slowDownAngle = slowDownRadius;
            m_info.m_target = target;
            m_info.m_timeToDesiredAngularSpeed = timeToDesiredSpeed;
        }

        protected override SteeringOutput GetSteering()
        {
            return Face.GetSteering(m_ownKS, m_info);
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, SAlign info)
        {
            SURROGATE_TARGET.rotation = Quaternion.Euler(SURROGATE_TARGET.eulerAngles.x, MathExtent.VectorToAngle(info.m_target.position - ownKS.m_position), SURROGATE_TARGET.eulerAngles.z);
            info.m_target = SURROGATE_TARGET;

            return Align.GetSteering(ownKS, info);
        }
    }
}
