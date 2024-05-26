using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

namespace theLastHope{

public class MonteCarloManager
{
   public float fixedDeltaTime = 10f;
   public float searchTimeout = 0.15f;
   public int maxSimulation = 10;
   public int maxDepth = 5;
   public int UCB1ExploreParam = 2;
   private MonteCarlo mcts;
   private Action<float> rewardCallback;

   public MonteCarloManager(bossHealth boss, bossHealth player, Action<float> rewardCallback)
	{
		this.mcts = new MonteCarlo(new BossControl(boss, player, fixedDeltaTime), maxSimulation, maxDepth, UCB1ExploreParam);
		this.rewardCallback = rewardCallback;
	}

    public void CalculateReward(bossHealth boss, bossHealth player)
		{
			// Reset the MCTS
			this.mcts.Reset(boss, player);
			Thread t = new Thread(() => {
				this.mcts.RunSearch();
				float reward = CalculateMCTSActionReward();
				this.rewardCallback(reward);
			});
			//t.Priority = ThreadPriority.Highest;
			t.Start();
		}
    
    public float CalculateMCTSActionReward()
		{
			float mctsReward = 0;
			MonteCarloNode rootNode = mcts.GetRootNode();
			List<int> mctsActions = mcts.GetActionsWithoutMove(rootNode);
			//ISet<int> annActions = mcts.ConvertMCTSActions(vectorAction);

			double MCTSBestUCB = Double.NegativeInfinity;
			double UCBMin = Double.PositiveInfinity;
			double UCB = 0;
			// Find best UCB for MCTS and ANN actions
			foreach (int action in mctsActions)
			{
				MonteCarloNode childNode = rootNode.ChildNode(action);
				if (childNode != null)
				{
					UCB = childNode.GetUCB1(UCB1ExploreParam);
					// Set MCTS action max UCB
					if (UCB > MCTSBestUCB)
					{
						MCTSBestUCB = UCB;
					}
					// Set ANN action max UCB
					//if (annActions.Contains(action) && UCB > ANNBestUCB)
					//{
				//		ANNBestUCB = UCB;
				//	}
					// Set min UCB
					if (UCB < UCBMin)
					{
						UCBMin = UCB;
					}
				}
			}

			// No reward will be given if suitable action not found
			// Move actions eliminated here
			if (MCTSBestUCB != Double.NegativeInfinity)
			{
					mctsReward = 1;
				}
				else if(mctsActions.Count>0){
					mctsReward = -1;
				
				/*
				// Prevent divide by zero assign too little values
				UCBMin = UCBMin == MCTSBestUCB ? 0 : UCBMin;
				MCTSBestUCB = MCTSBestUCB == 0 ? 000000000.1d : MCTSBestUCB;
				ANNBestUCB = ANNBestUCB == 0 ? 000000000.1d : ANNBestUCB;
				// Normalize the ANN UCB [0,1] -> (currentValue - minValue) / (maxValue - minValue)
				double normalizedANNRate = (ANNBestUCB - UCBMin) / (MCTSBestUCB - UCBMin);
				double differenceFromMax = 1 - normalizedANNRate;
				double diffSquare = Math.Pow(differenceFromMax, 2);
				mctsReward = (float)(1.3d * Math.Exp(-5.84d * diffSquare) - 0.01d);
				*/
			}

				// Give negative reward for non move actions that mcts does not recommend

			//if (name.Equals(BattleArena.RED_AGENT_TAG) && mctsReward != 0)
			//{
			//	Debug.Log(name + " " + mctsReward + " reward given: vectorActions=" + vectorAction[0] + "," + vectorAction[1] + "," + vectorAction[2] +
			//		" convertedActions=" + String.Join(",", annActions) + " mctsActions=" + String.Join(",", mctsActions));
			//}
			return mctsReward;
		}
}
}

