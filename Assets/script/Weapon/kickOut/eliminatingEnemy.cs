using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class eliminatingEnemy : MonoBehaviour
{
   
   [SerializeField] private List<Transform> enemyDead = new List<Transform>();
   [SerializeField] private float delay=0.15f;
   
   [SerializeField]
    private GameObject kickOutBtn;

   private movement player;
   private EnemyHealthBar enemy;

   public UnityEvent OnBegin, OnDone;


   void Update() {
    player = FindObjectOfType<movement>();
    enemy = FindObjectOfType<EnemyHealthBar>();
}

   private void OnTriggerEnter2D(Collider2D other) {

       if(other.gameObject.TryGetComponent<EnemyHealthBar>(out var enemys)){
          if(enemys.Hp <= 0){
          enemyDead.Add(enemys.transform);
          kickOutBtn.SetActive(true);
          }
       }
    }

     private void OnTriggerExit2D(Collider2D other) {
        
       if(other.gameObject.TryGetComponent<EnemyHealthBar>(out var enemys)){
          if(enemys.Hp <= 0){
          enemyDead.Remove(enemys.transform);
           if(enemyDead.Count == 0){
                kickOutBtn.SetActive(false);
            }
          }
       }
    }

    public void kickOuts(){
      OnBegin?.Invoke();
      Debug.Log("KICKOUT!");
       List<Transform> enemyDeadCopy = new List<Transform>(enemyDead);
    foreach(Transform enemyTransform in enemyDeadCopy){
        StartCoroutine(Reset(enemyTransform));
    }
    enemyDead.Clear();
    }


    private IEnumerator Reset(Transform enemyTransform){
        Vector3 flydir = (enemy.GetPosition() - player.GetPosition()).normalized;
        kickOut.Create(enemyTransform, enemy.GetPosition(), flydir);
        Destroy(enemyTransform.gameObject);
        yield return new WaitForSeconds(delay);
        OnDone?.Invoke();
        
    }


}
