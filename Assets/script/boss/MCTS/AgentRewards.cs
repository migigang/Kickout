using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theLastHope{

public class AgentRewards : MCTSAgent
{

    private static float WIN_REWARD = 1;
	private static float LOSE_REWARD = -1;
	private static float STEP_REWARD;
	private static float DAMAGE_REWARD = 0.2f;
    // Start is called before the first frame update
    //public void PlayerRewards(int maxStep)
	//{
	///	STEP_REWARD = -1f / maxStep;
//	}
    public float GetWinReward()
	{
		return WIN_REWARD;
	}
    public float GetLoseReward()
	{
		return LOSE_REWARD;
	}

    public float GetStepReward()
	{
		return STEP_REWARD;
	}

	public float GetDamageReward(bossHealth boss, movement player)
	{
		return DAMAGE_REWARD * (boss.currentHealth - player.health);
	}
}
}
