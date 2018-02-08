using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : IWeapon{

    [SerializeField] private float m_speed = 10f;
    [SerializeField] private float timeToExplode = 0f;
    [SerializeField] private float timeToExplodeLimit = 3f;

    private Vector3 m_direction;
    private Rigidbody l_myRigibody;

    private CharacterController m_characterController;
    private PlayerBlackboard m_playerBlackboard;
    public GameObject l_myGrenade;

    void IWeapon.Init(PlayerBlackboard blackboard)
    {
        m_playerBlackboard = blackboard;
        m_characterController = blackboard.m_characterController;
        l_myGrenade = GameObject.FindGameObjectWithTag("Player");
    }
    //void IWeapon.Init(PlayerBlackboard blackboard)
    //{
    //    m_playerBlackboard = blackboard;
    //    m_characterController = blackboard.m_characterController;
    //}
    // Use this for initialization
    void Start()
    {
        //l_myRigibody.AddForce(m_direction * 4, ForceMode.Impulse);
        l_myRigibody.AddForce(m_playerBlackboard.gameObject.transform.forward * 4, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        if (timeToExplode >= timeToExplodeLimit)
        {
            //this.gameObject.SetActive(false);
        }
    }

    public void GrenadeThrown(Vector3 direction)
    {
        m_direction = direction;
    }

  
    public void UseWeapon()
    {
    }



    public void OnTriggerEnter(Collider other)
    {
    }

    public void OnTriggerStay(Collider other)
    {
    }

    public void OnTriggerExit(Collider other)
    {
    }

    public void OnControllerCOlliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            //Physics.IgnoreCollision(this.gameObject, hit.gameObject, true);
        }
    }


    public void DeInit()
    {
    }
}
