using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackboard : MonoBehaviour {


    // Use this for initialization
    public float m_movementSpeed = 1;
    public Vector3 m_movementDirection;
    public CharacterController m_characterController;
    public PlayerController m_playerController;
    public float m_GravityScale = -2;


    //public enum IMoveState {Normal, Dash, Impact}
    public PlayerMovement m_MoveNormal;
    public PlayerDashing m_MoveDashing;
    public PlayerHitMovement m_MoveHit;

    //public Skills
    public PlayerDashSkill m_SkillDash;

    //Weapons
    public Grenade m_weaponGrenade;
    public Bullet m_weaponBullet;

    //Variables
    public bool m_PlayerDashing; //evita el movement
    public float l_dashVelocity = 0.1f;

    public float m_hitPushValue = 1;




    void Awake ()
    {
        m_MoveNormal = new PlayerMovement();
        m_MoveDashing = new PlayerDashing();
        m_SkillDash = new PlayerDashSkill();
        m_MoveHit = new PlayerHitMovement();

        m_characterController = GetComponent<CharacterController>();
        m_playerController = GetComponent<PlayerController>();
    }


    // Update is called once per frame
    void Update ()
    {

    }
}
