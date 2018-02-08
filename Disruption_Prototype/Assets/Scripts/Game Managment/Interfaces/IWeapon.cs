using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon {

    void Init(PlayerBlackboard blackboard);

    void UseWeapon();

    void OnTriggerEnter(Collider other);

    void OnTriggerStay(Collider other);

    void OnTriggerExit(Collider other);

    void OnControllerCOlliderHit(ControllerColliderHit hit);

    void DeInit();
}

