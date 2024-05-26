using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace theLastHope{

public class MCTSAgent : MonoBehaviour
{

    protected bossHealth boss;
    protected bossHealth player;
    protected volatile bool makeRequest = false;
    private MonteCarloManager monteCarloManager;
    private bool manualReward = true;
    private AgentRewards rewards;


    public void Start()
    {
        boss = GetComponent<bossHealth>();
        player = GetComponent<bossHealth>();
       // boss = GetComponent<bossHealth>();
        //player = GameObject.FindObjectOfType<movement>();

        //base.Start();
        this.monteCarloManager = new MonteCarloManager(boss, player, OnRewardReceived);
    }

    public void OnRewardReceived(float mctsReward)
	{
		// Add %50 of this value
		mctsReward+=mctsReward / 20f;

		// Request new decision
		this.makeRequest = true;
	}

    public void OnActionReceived(float[] vectorAction)
	{
		// MCTS action reward should be calculated with the state before action done
		// So save the current state of player and enemy
		bossHealth mctsBoss = boss.CloneBoss();
		bossHealth mctsPlayer = player.ClonePlayer();
		// Continue ordinary action process 
    	//base.OnActionReceived(vectorAction);

		// Reward for MCTS result
		this.monteCarloManager.CalculateReward(mctsBoss, mctsPlayer);
		}

    // Update is called once per frame
    void Update()
    {

        // Generate vector action (e.g., using your own logic)
        float[] vectorAction = GenerateVectorAction();

        // Call OnActionReceived to initiate MCTS and reward calculation
        OnActionReceived(vectorAction);

        // Reset makeRequest flag

    }

    private void GiveRewards()
        {
        if (player.isDead)
        {
            // Reward for win
            rewards.GetWinReward();
        }
        else if (boss.isDead)
        {
            // Reward for loose
            rewards.GetLoseReward();
        }
        else
        {
            rewards.GetStepReward();
            }
        }

    private float[] GenerateVectorAction()
{
    // Misalnya, kita memiliki 3 aksi yang dapat dipilih dengan nilai float antara -1 dan 1
    float action1 = UnityEngine.Random.Range(-1f, 10f);
    float action2 = UnityEngine.Random.Range(-1f, 20f);
    float action3 = UnityEngine.Random.Range(-1f, 30f);

    // Return array of actions
    return new float[] { action1, action2, action3 };
}
}
}
