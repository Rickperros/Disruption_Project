public interface ISkill
{
    void Init(PlayerBlackboard blackboard); 
	bool TryToStart(); 
	void OnUse(); 
	void OnEnd(); 
	void DeInit(); 
}
