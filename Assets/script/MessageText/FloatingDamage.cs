using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class FloatingDamage : MonoBehaviour, IInGameMessage
{
    private Rigidbody2D _rigibody;
    private TMP_Text _damageValue;

    public float InitialYVelocity = 7f;
    public float initialXVelocityRange = 3f;
    public float LifeTime = 1f;


    private void Awake()
    {
       _rigibody = GetComponent<Rigidbody2D>(); 
       _damageValue = GetComponentInChildren<TMP_Text>();

    }

    private void Start() {
        _rigibody.velocity  = 
            new Vector2(Random.Range(-initialXVelocityRange, initialXVelocityRange), InitialYVelocity);
        Destroy(gameObject,LifeTime);
    }

    public void SetMessage(string msg){
        _damageValue.SetText(msg);
    }
}
