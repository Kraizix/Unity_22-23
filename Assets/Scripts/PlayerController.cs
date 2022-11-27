using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    private int _level;
    private int _exp;
    private int _expThreshold = 5;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(GameManager.gm.Forward))
        {
            transform.position += Vector3.up * (Time.deltaTime * speed);
            _animator.SetBool("UpKey", true);
        }
        if (Input.GetKey(GameManager.gm.Backward))
        {
            transform.position += Vector3.down * (Time.deltaTime * speed);
            _animator.SetBool("DownKey", true);
        }
        if (Input.GetKey(GameManager.gm.Right))
        {
            transform.position += Vector3.right * (Time.deltaTime * speed);
            _animator.SetBool("RightKey", true);
        }
        if (Input.GetKey(GameManager.gm.Left))
        {
            transform.position += Vector3.left * (Time.deltaTime * speed);
            _animator.SetBool("LeftKey", true);
        }
        CheckPos();

        if (_animator != null)
        {
            if (Input.GetKeyUp(GameManager.gm.Forward))
            {
                _animator.SetBool("UpKey", false);
            }
            if (Input.GetKeyUp(GameManager.gm.Backward))
            {
                _animator.SetBool("DownKey", false);
            }
            if (Input.GetKeyUp(GameManager.gm.Right))
            {
                _animator.SetBool("RightKey", false);
            }
            if (Input.GetKeyUp(GameManager.gm.Left))
            {
                _animator.SetBool("LeftKey", false);
            }
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

    private void CheckPos()
    {
        var pos = transform.position;
        pos.x = pos.x <= -20.75f ? -20.75f : pos.x >= 20.75f ? 20.75f : pos.x;
        pos.y = pos.y <= -9.525f ? -9.525f : pos.y >= 9.525f ? 9.525f : pos.y;
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Exp"))
        {
            Destroy(col.gameObject);
            _exp += 1;
            CalculateLevel();
        }
    }

    private void CalculateLevel()
    {
        if (_exp >= _expThreshold)
        {
            _exp -= _expThreshold;
            _level += 1;
            _expThreshold += 5;
        }
    }
}