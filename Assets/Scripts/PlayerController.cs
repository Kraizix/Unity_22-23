using System;
using System.Collections;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public float hp = 100f;
    public float speed = 3.0f;
    public float cd = 1.0f;
    public float damage = 10f;
    public float range = 1f;
    private float _cd;
    void Update()
    {
        if (Input.GetKey(GameManager.gm.Forward))
        {
            transform.position += Vector3.up * (Time.deltaTime * speed);
        }
        if (Input.GetKey(GameManager.gm.Backward))
        {
            transform.position += Vector3.down * (Time.deltaTime * speed);
        }
        if (Input.GetKey(GameManager.gm.Right))
        {
            transform.position += Vector3.right * (Time.deltaTime * speed);
        }
        if (Input.GetKey(GameManager.gm.Left))
        {
            transform.position += Vector3.left * (Time.deltaTime * speed);
        }

        if (_cd <= Time.time)
        {
            _cd = Time.time + cd;
            Attack();
        }
    }

    private void Attack()
    {
        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy")))
        {
            col.GetComponent<Damageable>().TakeDamage(damage);
        }
    }
}