using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _damage;

    private void Start()
    {
        _damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().damage * 4;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
            col.GetComponent<Damageable>().TakeDamage(_damage);
    }

    private void Update()
    {
        transform.Translate(0, 0.025f , 0);
    }
    
    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
