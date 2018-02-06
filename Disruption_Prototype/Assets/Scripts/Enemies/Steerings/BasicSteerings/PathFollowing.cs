using System.Collections.Generic;
using UnityEngine;

namespace Steerings
{
    public class PathFollowing : SteeringBehaviour
    {
        public SPathFollowing m_info;

        public void SetInfo(SPathFollowing info)
        {
            m_info = info;
        }

        public void SetInfo(EFacingPolicy facingPolicy, float closeEnoughRadius, int currentWaypointIndex, List<Transform> path)
        {
            m_info.m_facingPolicy = facingPolicy;
            m_info.m_closeEnoughRadius = closeEnoughRadius;
            m_info.m_currentWaypointIndex = currentWaypointIndex;
            m_info.m_path = path;
        }

        protected override SteeringOutput GetSteering()
        {
            return base.GetSteering();
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, SPathFollowing info)
        {
            if (info.m_currentWaypointIndex >= info.m_path.Count)
                return NULL_STEERING;

            if (Vector3.Distance(info.m_path[info.m_currentWaypointIndex].position, ownKS.m_position) <= info.m_closeEnoughRadius)
                info.m_currentWaypointIndex++;

            if (info.m_currentWaypointIndex == info.m_path.Count)
                return NULL_STEERING;

            SURROGATE_TARGET.position = info.m_path[info.m_currentWaypointIndex].position;

            return Seek.GetSteering(ownKS, SURROGATE_TARGET);
        }
    }
}
