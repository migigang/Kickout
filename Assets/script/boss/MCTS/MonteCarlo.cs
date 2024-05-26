using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theLastHope{

    public class MonteCarlo 
    {
        private BossControl bossControl;
        private int UCB1ExploreParam;
        private Dictionary<int, MonteCarloNode> nodes;
        private int maxSimulation;
        private int maxDepth;

        public MonteCarlo(BossControl bossControl, int maxSimulation, int maxDepth, int UCB1ExploreParam){
            this.bossControl = bossControl;
            this.maxSimulation = maxSimulation;
            this.maxDepth = maxDepth;
            this.UCB1ExploreParam = UCB1ExploreParam;
            this.nodes = new Dictionary<int, MonteCarloNode>();
        }
        public void Reset(bossHealth boss, bossHealth player)
		{
            this.nodes.Clear();
            this.bossControl.Reset(boss, player);
        }

        public void MakeNode(BossState state)
        {
            if (!this.nodes.ContainsKey(state.GetId()))
            {
                bool[] unexpandedActions = this.bossControl.LegalActions(state);
                MonteCarloNode node = new MonteCarloNode(null, -1, state, unexpandedActions);
                this.nodes[state.GetId()] = node;
            }
        }

        public void RunSearch()
        {
            BossState state = bossControl.GetInitialState();

            this.MakeNode(state);
            int totalSims = 0;
			// Run until time runs out
            while (totalSims <= maxSimulation)
            {
                MonteCarloNode node = this.Select(state);
                PlayerKind winner = this.bossControl.Winner(node.state);

                if (node.IsLeaf() == false && winner == PlayerKind.NONE)
                {
                    node = this.Expand(node);
                    winner = this.Simulate(node, maxDepth);
                }
                this.Backpropagate(node, winner);

                totalSims++;
            }
        }

        /*
		 * Phase 1: Selection
		 * Select until EITHER not fully expanded OR leaf node
		 */
        public MonteCarloNode Select(BossState state)
        {
            if (!this.nodes.ContainsKey(state.GetId()))
            {
                UnityEngine.Debug.LogError("Key not found in the map: " + string.Join(",", this.nodes.Keys) + ", key = " + state.GetId());
            }
            MonteCarloNode node = this.nodes[state.GetId()];

            while (node.IsFullyExpanded() && !node.IsLeaf())
            {
                List<int> actions = node.AllActions();
                int bestAction = -1;
                double bestUCB1 = double.NegativeInfinity;

                foreach (int action in actions)
                {
                    double childUCB1 = node.ChildNode(action).GetUCB1(this.UCB1ExploreParam);
                    if (childUCB1 > bestUCB1)
                    {
                        bestAction = action;
                        bestUCB1 = childUCB1;
                    }
                }
                node = node.ChildNode(bestAction);
            }
            return node;
        }

        /*
		 * Phase 2: Expansion
		 * Of the given node, expand a random unexpanded child node
		 */
        public MonteCarloNode Expand(MonteCarloNode node)
        {
            // Select random action
            List<int> actions = node.UnexpandedActions();
            int action = this.bossControl.RandomAction(actions);
            BossState childState = this.bossControl.NextState(node.state, action);
            bool[] childActions = this.bossControl.LegalActions(childState);
            MonteCarloNode childNode = node.Expand(action, childState, childActions);
            this.nodes[childState.GetId()] = childNode;
            return childNode;
        }

        /*
		 * Phase 3: Simulation
		 * From given node, play the game until a terminal state, then return winner
		 */
        public PlayerKind Simulate(MonteCarloNode node, int maxDepth)
        {
            BossState state = node.state;
            bossHealth boss = state.GetBoss().CloneBoss();
            bossHealth player = state.GetPlayer().ClonePlayer();
            PlayerKind turn = state.GetTurn();
            PlayerKind winner;
            int depth = 0;

            // Continue until someone wins, specific depth is reached or time is up
            while ((winner = this.bossControl.Winner(boss, player)) == PlayerKind.NONE && depth < maxDepth)
            {
                int action = this.bossControl.RandomLegalAction(boss, player, turn);
                turn = this.bossControl.MakeAction(boss, player, action, turn);
                //this.bossControl.UpdatePlayers(player, enemy);
                depth++;
            }

            // Calculate winner manually if no one wins
            if (winner == PlayerKind.NONE)
            {	
                winner = CalculateWinner(state.GetBoss(), boss, state.GetPlayer(), player);	
            }	

            return winner;	
        }


        /*
		 * Phase 4: Backpropagation
		 * From given node, propagate plays and winner to ancestors' statistics
		 */
        public void Backpropagate(MonteCarloNode node, PlayerKind winner)
        {
            while (node != null)
            {
                node.numberOfPlays++;
                if (winner == PlayerKind.BOSS)
                {
                    node.numberOfWins++;	
                }
                // Update parent
                node = node.parent;
            }
        }
        // Utility methods

        /*
		 * Get root node.
		 */
        public MonteCarloNode GetRootNode()
        {
            return this.nodes[bossControl.GetInitialState().GetId()];
        }

        /*
		 * Get all legal actions except move action from root node.
		 */
		public List<int> GetActionsWithoutMove(MonteCarloNode node)
		{
            return this.bossControl.GetActionsWithoutMove(node);
		}

        private PlayerKind CalculateWinner(bossHealth BossFirstState, bossHealth BossLastState,
			bossHealth PlayerFirstState, bossHealth PlayerLastState)
        {
            // Calculate the remaining time with dividing remaining health to health difference
            float BossHealthDiff = BossFirstState.currentHealth - BossLastState.currentHealth;
            float PlayerHealthDiff = PlayerFirstState.playerHealth - PlayerLastState.playerHealth;
            // Prevent divide by zero
            BossHealthDiff = BossHealthDiff == 0 ? 1 : BossHealthDiff;
            PlayerHealthDiff = PlayerHealthDiff == 0 ? 1 : PlayerHealthDiff;
            // Return remaining time difference
            float BossRemainingTime = BossLastState.currentHealth / BossHealthDiff;
            float PlayerRemainingTime = PlayerLastState.playerHealth / PlayerHealthDiff;
            return BossRemainingTime >= PlayerRemainingTime ? PlayerKind.BOSS : PlayerKind.PLAYER;
        }

        //private PlayerKind CalculateWinner2(IPlayer playerFirstState, IPlayer playerLastState,
         //   IPlayer enemyFirstState, IPlayer enemyLastState)
        //{
         //   float playerTotalHealth = playerLastState.GetHealth() + playerLastState.GetHealthPotionCount() * PlayerProperties.HEALTH_POTION_FILL;
          //  float playerTotalMana = playerLastState.GetMana() + playerLastState.GetManaPotionCount() * PlayerProperties.MANA_POTION_FILL;
         //   float enemyTotalHealth = enemyLastState.GetHealth() + enemyLastState.GetHealthPotionCount() * PlayerProperties.HEALTH_POTION_FILL;
        //    float enemyTotalMana = enemyLastState.GetMana() + enemyLastState.GetManaPotionCount() * PlayerProperties.MANA_POTION_FILL;
        //    return playerTotalHealth + playerTotalMana >= enemyTotalHealth + enemyTotalMana ? PlayerKind.PLAYER : PlayerKind.ENEMY;
        //}

        public bool ActionsHasOnlyMove(System.Collections.Generic.List<int> actions)
		{
			return bossControl.ActionsHasOnlyMove(actions);
		}

        
    
    }

}
