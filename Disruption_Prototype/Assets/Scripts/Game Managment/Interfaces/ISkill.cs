using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill {

    void Init(PlayerBlackboard blackboard);

    void UseSkill();

    void OnTriggerEnter(Collider other);

    void OnTriggerStay(Collider other);

    void OnTriggerExit(Collider other);

    void OnControllerCOlliderHit(ControllerColliderHit hit);

    void DeInit();
}
