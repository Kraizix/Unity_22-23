using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private GameObject _enemy;
    private List<Vector3> _spawnPos;
    private float _nextTime;
    [SerializeField]
    private float _cd = 2f;
    private float _cdDecrement = 0.05f;
    private float _timeIncrease = 60;
    private float _spawnRateCountdown;

    private Camera _main;


    private void Awake()
    {
        _enemy = Resources.Load("Prefabs/Enemy") as GameObject;
        _spawnPos = new List<Vector3>();
        _main = Camera.main;
        SetSpawns();
    }

    private void Spawn()
    {
        var basePos = _main.ViewportToWorldPoint(_spawnPos[Random.Range(0, _spawnPos.Count - 1)]);
        basePos.z = 0;
        Instantiate(_enemy, basePos, Quaternion.Euler(0,0,0), transform);
    }

    private void Update()
    {
        _spawnRateCountdown -= Time.deltaTime;
        if (_spawnRateCountdown <= 0)
        {
            _cd -= _cdDecrement;
            _spawnRateCountdown += _timeIncrease;
        }
        if (_nextTime < Time.time)
        {
            _nextTime = Time.time + _cd;
            Spawn();
        }
    }

    private void SetSpawns()
    {
        for (float i = -0.4f; i < 1.6f; i += 0.2f)
        {
            for (float j = -0.4f; j < 1.6f; j += 0.2f)
            {
                if (!(i is > 0 and < 1 && j is > 0 and < 1))
                {
                    _spawnPos.Add(new Vector3(i,j));
                }
            }
        }
    }
}
