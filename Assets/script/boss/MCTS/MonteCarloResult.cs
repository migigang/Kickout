using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace theLastHope{

    public class MonteCarloResult
    {
        private int action;
		private float numberOfPlays;
		private float numberOfWins;

        public MonteCarloResult(int action, float numberOfPlays, float numberOfWins)
		{
			this.action = action;
			this.numberOfPlays = numberOfPlays;
			this.numberOfWins = numberOfWins;
		}

        public int GetAction()
		{
			return this.action;
		}

		public float GetNumberOfPlays()
		{
			return this.numberOfPlays;
		}

		public float GetNumberOfWins()
		{
			return this.numberOfWins;
		}



    }
}
