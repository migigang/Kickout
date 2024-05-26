using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class knockBackFeedBack : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;

    [SerializeField] private float strength = 16, delay=0.15f;

    public UnityEvent OnBegin, OnDone;

    public void playFeedback(GameObject sender){
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rigidbody.AddForce(direction*strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset(){
        yield return new WaitForSeconds(delay);
        rigidbody.velocity = Vector3.zero;
        OnDone?.Invoke();
        
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
