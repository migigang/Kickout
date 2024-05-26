using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theLastHope{

public class BossControl
{
   private int stateId;
	private BossState initialState;
	private float fixedDeltaTime;
	private BossHelper BossHelper;
	private BossHelper PlayerHelper;

    private System.Random random = new System.Random();

    public int RandomNumber(int min, int max)
	{
		return random.Next(min, max);
	}
    public BossControl(bossHealth boss, bossHealth player, float fixedDeltaTime)
	{
		this.stateId = 0;
		this.BossHelper = new BossHelper(boss);
		this.PlayerHelper = new BossHelper(player);
		this.fixedDeltaTime = fixedDeltaTime;
	}
    public void Reset(bossHealth boss, bossHealth player)
	{
	// Initial state owner is player
	this.stateId = 0;
	this.initialState = new BossState(stateId++, boss, player, PlayerKind.BOSS);
	}

    public BossState GetInitialState()
	{
		return this.initialState;
	}

    public bool[] LegalActions(BossState state)
	{
		if (state.GetTurn() == PlayerKind.BOSS)
		{
			BossHelper.FillAvailableActions(state.GetBoss(), state.GetPlayer());
			return BossHelper.allActions;
		}
        return new bool[0];
	}

    public int RandomLegalAction(bossHealth boss, bossHealth player, PlayerKind turn){
        int legalActionCount;
        if(turn==PlayerKind.BOSS){
            legalActionCount = BossHelper.FillAvailableActions(boss,player);
            return RandomAction(BossHelper.allActions, legalActionCount);
        }
        return 0;
    }

    public int RandomAction(bool[] allActions, int legalActionCount)
	{
		int action = -1;
		if (legalActionCount > 0)
		{
			int rn = RandomNumber(0, legalActionCount);
			for (int i = 0; i < allActions.Length; i++)
			{
				if (allActions[i])
				{
					if (rn == 0)
					{
						action = i;
						break;
					}
					rn--;
				}
			}
		}

		return action;
	}
    public int RandomAction(List<int> actions)
	{
		if (actions.Count > 0)
		{
			int rn = RandomNumber(0, actions.Count);
			return actions[rn];
		}

		return -1;
	}

    public BossState NextState(BossState state, int play)
	{
		// Clone player and enemy to new state
		bossHealth newBoss = state.GetBoss().CloneBoss();
		bossHealth newPlayer = state.GetPlayer().ClonePlayer();

		// Make action
		PlayerKind newTurn = MakeAction(newBoss, newPlayer, play, state.GetTurn());
			
		// Update Agents
		//UpdateAgents(newBoss, newPlayer);

		// Create new state with new player and enemy
		return new BossState(stateId++, newBoss, newPlayer, newTurn);
	}

    public PlayerKind MakeAction(bossHealth boss, bossHealth player, int play, PlayerKind turn)
		{
			// Make action
		if (turn == PlayerKind.BOSS)
		{
			BossHelper.MakeAction(boss, player, play);
			Debug.Log("KIYOMASA");
		}
		else
		{
			PlayerHelper.MakeAction(player,boss,play);
			Debug.Log("yooo");
		}

		return ChangeTurn(turn);
		}
        //public void UpdateAgents(bossHealth boss, movement player)
		//{
		//	boss.UpdateAgents(fixedDeltaTime);
		//	player.UpdateAgents(fixedDeltaTime);
		//}

        public PlayerKind Winner(BossState state)
		{
			return Winner(state.GetBoss(), state.GetPlayer());
		}

        public PlayerKind Winner(bossHealth boss, bossHealth player)
		{
			return boss.isDead ? PlayerKind.PLAYER : player.isDead ? PlayerKind.BOSS : PlayerKind.NONE;
		}

		private PlayerKind ChangeTurn(PlayerKind currentTurn)
		{
			return currentTurn == PlayerKind.BOSS ? PlayerKind.PLAYER : PlayerKind.BOSS;
		}

        //public ISet<int> ConvertMCTSActions(float[] vectorAction)
		//{
		//	ISet<int> correspondingActions = new HashSet<int>();
			// Convert skill actions
		//	if (vectorAction[PlayerAgent.SKILL_BRANCH_INDEX] != 0)
		//	{
	//			correspondingActions.Add(playerHelper.SKILL_START_INDEX + (int)vectorAction[PlayerAgent.SKILL_BRANCH_INDEX] - 1);
	//		}
			// Convert potion actions
		//	if (vectorAction[PlayerAgent.POTION_BRANCH_INDEX] != 0)
		//	{
		//		correspondingActions.Add(playerHelper.HEALTH_POTION_INDEX + (int)vectorAction[PlayerAgent.POTION_BRANCH_INDEX] - 1);
		//	}
	//		return correspondingActions;
	//	}


        public List<int> GetActionsWithoutMove(MonteCarloNode node)
		{
			List<int> allActions = new List<int>();
            
			foreach (MonteCarloNode child in node.children.Values)
			{
				// eliminate unvisited actions and move action
				if (child != null && child.action != BossHelper.MOVE_INDEX)
				{
					allActions.Add(child.action);
				}
			}

			return allActions;
		}

        public bool ActionsHasOnlyMove(List<int> actions)
		{
			bool result = true;
			foreach (int action in actions)
			{
				if (action != BossHelper.MOVE_INDEX)
				{
					result = false;
					break;
				}
			}
			return result;
		}

        


}
}
