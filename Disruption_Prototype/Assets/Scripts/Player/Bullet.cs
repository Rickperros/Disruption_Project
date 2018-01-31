using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_speed = 10f;
    [SerializeField] private float m_lenght = 0.25f;

    private Vector3 m_direction;
    private LineRenderer m_lineRenderer;
	
	void Awake ()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
	}
	
	void Update ()
    {
        Ray l_ray = new Ray(transform.position, m_direction);
        RaycastHit l_hitInfo;

        //StopsBullet
        if(Physics.Raycast(l_ray, out l_hitInfo, m_lenght))
        {
            if(l_hitInfo.collider.tag != "Player")
            //Maybe put some kind of decal
            this.gameObject.SetActive(false);
        }

        //Draw Bullet
        m_lineRenderer.SetPosition(0, transform.position);
        m_lineRenderer.SetPosition(1, transform.position + (m_direction.normalized * m_lenght));

        //Move Bullet
        transform.position += m_direction.normalized * m_speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 direction)
    {
        m_direction = direction;
    }
}
