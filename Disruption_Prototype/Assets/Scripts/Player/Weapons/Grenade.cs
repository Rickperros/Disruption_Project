using UnityEngine;

public class Grenade : MonoBehaviour {

    [SerializeField] private float m_speed = 10f;
    [SerializeField] private float timeToExplode = 0f;
    [SerializeField] private float timeToExplodeLimit = 3f;

    private Vector3 m_direction;
    private Rigidbody l_myRigibody;

    // Use this for initialization
    void Start()
    {
        l_myRigibody.AddForce(m_direction * 4, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToExplode >= timeToExplodeLimit)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void GrenadeThrown(Vector3 direction)
    {
        m_direction = direction;
    }
}
