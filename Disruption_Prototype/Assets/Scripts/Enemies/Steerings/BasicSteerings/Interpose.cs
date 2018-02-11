using UnityEngine;

namespace Steerings
{
    public class Interpose : SteeringBehaviour
    {
        public SInterpose m_interposeInfo;
        public SArrive m_arriveInfo;

        protected override SteeringOutput GetSteering()
        {
            SteeringOutput result = Interpose.GetSteering(m_ownKS,m_interposeInfo, m_arriveInfo);
            base.ApplyFacingPolicy(m_interposeInfo.m_facingPolicy, result, m_interposeInfo.m_target.gameObject);

            return result;
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, SInterpose interposeInfo, SArrive arriveInfo)
        {
            Vector3 positionToKeep = (interposeInfo.m_target.position - interposeInfo.m_coveredObject.position).normalized * ((interposeInfo.m_target.position - interposeInfo.m_coveredObject.position).magnitude * interposeInfo.m_distanceToInterpose);
            SURROGATE_TARGET.position = interposeInfo.m_coveredObject.position + positionToKeep;

            arriveInfo.m_target = SURROGATE_TARGET;
            return Arrive.GetSteering(ownKS, arriveInfo);
        }
    }
}
