namespace Steerings
{
    public class ArrivePlusAvoid : SteeringBehaviour
    {
        public SArrive m_arriveInfo;
        public SObstacleAvoidance m_obstacleAvoidanceInfo;
        public bool obstacleAvoided;

        public void SetInfo(SArrive arrive, SObstacleAvoidance obstacleAvoidance)
        {
            m_arriveInfo = arrive;
            m_obstacleAvoidanceInfo = obstacleAvoidance;
        }

        protected override SteeringOutput GetSteering()
        {
            SteeringOutput result = ArrivePlusAvoid.GetSteering(m_ownKS, m_arriveInfo, m_obstacleAvoidanceInfo, ref obstacleAvoided);
            base.ApplyFacingPolicy(obstacleAvoided ? m_obstacleAvoidanceInfo.m_facingPolicy : m_arriveInfo.m_facingPolicy, result);

            return result;
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, SArrive arriveInfo, SObstacleAvoidance obstacleInfo, ref bool obstacleAvoided)
        {
            SteeringOutput obstacleAvoidSteering = ObstacleAvoidance.GetSteering(ownKS, obstacleInfo);

            if (obstacleAvoidSteering != NULL_STEERING)
            {
                obstacleAvoided = true;
                return obstacleAvoidSteering;
            }

            obstacleAvoided = false;
            return Arrive.GetSteering(ownKS, arriveInfo);
        }
    }
}
