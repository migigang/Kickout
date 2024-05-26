using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace theLastHope
{
public class BossState
{
    public float simTickRate = 0.002f;//(0.16f = 60 fps), remplaces Time.deltaTime pour la simulation
    public bossHealth BossManager;

    public struct Agent
        {
            public Vector2 moveDir;
            public Vector2 BossPosition;
            public bool isAttacking;
            public bool canShot;
            public int currentHealth;
            public int playerHealth;
            public float moveSpeed;
            public float startBetweenMissile;
            public float startBetweenShots;
            public bool hasWon;
            public bool isAlive;
        }

    //public Point lastPositionPlayer, lastPositionBoss;
    private int id = 0;
    public int currentHealth;
    public int playerHealth;
    public float moveSpeed;
    public int stateResult;
    public float startBetweenMissile;
    public float startBetweenShots;
    public bossHealth BossNPC;
    public bossHealth Player;
    public PlayerKind playerKind;


    //Copy constructor
        public BossState(int id, bossHealth BossNPC, bossHealth Player, PlayerKind playerKind)
        {
            //this.currentHealth = currentHealth;
            //this.playerHealth = playerHealth;
            //this.moveSpeed = moveSpeed;
            //this.startBetweenMissile = startBetweenMissile;
            //this.startBetweenShots = startBetweenShots; 
            this.BossNPC = BossNPC;
            this.Player = Player;
            this.playerKind = playerKind;
			this.id = id;
           
        }
        public bool IsBoss()
		{
			return this.playerKind == PlayerKind.BOSS;
		}

		public PlayerKind GetTurn()
		{
			return this.playerKind;
		}

		public bossHealth GetPlayer()
		{
			return this.Player;
		}

		public bossHealth GetBoss()
		{
			return this.BossNPC;
		}

		public int GetId()
		{
			return this.id;
		}


    public List<inputAction> CheckInputsPossible(Agent self)//BossNPC opponent
        {
            List<inputAction> possible = new List<inputAction>();

            if (self.isAttacking ||  self.canShot)
            {   
                possible.Add(inputAction.idle);
                return possible;
            }
            
            possible.Add(inputAction.shotlaser);
            possible.Add(inputAction.walking);
            possible.Add(inputAction.goAway);
            possible.Add(inputAction.spiral);
            possible.Add(inputAction.doubleSpiral);
            //possible.Add(InputAction.Stab1);
            //possible.Add(InputAction.Throw1);

             if (currentHealth<0)
            {   
                possible.Add(inputAction.dead);
            }
            

            return possible;
        }

      //   public BossState PlayAction(inputAction input)
     //   {
          //  return BossManager.MyUpdate(this, input, simTickRate);
     //   }
       // public bool IsFinished()
       // {
        //    return BossNPC.hasWon || !BossNPC.isAlive;
       // }

      //  public bool HasWon()
      //  {
       //     return BossNPC.hasWon;
       // }

      //public BossState Clone()
  ////  {
    //    return new BossState(lastPositionPlayer, lastPositionBoss, currentHealth, playerHealth, moveSpeed, startBetweenMissile, startBetweenShots);
   // }

   // public BossState( Point lastPositionPlayer,Point lastPositionBoss, int currentHealth, int playerHealth, float moveSpeed,float startBetweenMissile, float startBetweenShots ){
      //  this.lastPositionPlayer = lastPositionPlayer;
     //   this.lastPositionBoss = lastPositionBoss;
     //   this.currentHealth = currentHealth;
     //   stateResult = bossHealth.RESULT_NONE;
     //   this.playerHealth = playerHealth;
     //   this.moveSpeed = moveSpeed;
     //   this.startBetweenMissile = startBetweenMissile;
     //   this.startBetweenShots=startBetweenShots;
    //}
}
}
