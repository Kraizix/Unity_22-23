using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private GameObject _enemy;
    private List<Vector3> _spawnPos;
    private float _nextTime;
    private float _cd = 2f;

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
        if (_nextTime < Time.time)
        {
            _nextTime = Time.time + _cd;
            Spawn();
        }
    }

    private void SetSpawns()
    {
        for (float i = -1; i < 2; i += 0.2f)
        {
            for (float j = -1; j < 2; j += 0.2f)
            {
                if (!(i is > 0 and < 1 && j is > 0 and < 1))
                {
                    _spawnPos.Add(new Vector3(i,j));
                }
            }
        }
    }
}
