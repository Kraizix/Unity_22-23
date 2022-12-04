using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public float cd = 1.0f;
    public float damage = 10f;
    public float range = 1f;
    private float _cd;
    private int _level;
    private int _exp;
    private int _expThreshold = 5;
    public float _timer;
    private Animator _animator;
    private List<Upgrade> _upgrades = new();
    private Vector3 _lastDir = Vector3.right;
    [SerializeField] private MenuManager _menu;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text lvl;
    [SerializeField] private TMP_Text timerTxt;
    [SerializeField] private GameObject rangeSprite;
    private static readonly int UpKey = Animator.StringToHash("UpKey");
    private static readonly int DownKey = Animator.StringToHash("DownKey");
    private static readonly int RightKey = Animator.StringToHash("RightKey");
    private static readonly int LeftKey = Animator.StringToHash("LeftKey");
    private GameObject _bullet;

    private void Start()
    {
        _bullet = Resources.Load("Prefabs/Bullet") as GameObject;
        _animator = GetComponent<Animator>();
        _upgrades.Add(new Upgrade("Speed", () =>PerformUpgrade(ref speed, Random.Range(0.2f,0.5f))));
        _upgrades.Add(new Upgrade("Cooldown", () =>PerformUpgrade(ref cd, Random.Range(-0.1f,-0.2f))));
        _upgrades.Add(new Upgrade("Range", () =>PerformUpgrade(ref range, Random.Range(0.2f,0.5f))));
        _upgrades.Add(new Upgrade("Damage", () =>PerformUpgrade(ref damage, Random.Range(1,5))));
    }

    void Update()
    {
        _timer += Time.deltaTime;
        UpdateTimer();
        if (Input.GetKey(GameManager.gm.Forward))
        {
            transform.position += Vector3.up * (Time.deltaTime * speed);
            _lastDir = Vector3.zero;
            _animator.SetBool(UpKey, true);
        }
        if (Input.GetKey(GameManager.gm.Backward))
        {
            transform.position += Vector3.down * (Time.deltaTime * speed);
            _lastDir = Vector3.down * 180;
            _animator.SetBool(DownKey, true);
        }
        if (Input.GetKey(GameManager.gm.Right))
        {
            transform.position += Vector3.right * (Time.deltaTime * speed);
            _lastDir = Vector3.right * 90;
            _animator.SetBool(RightKey, true);
        }
        if (Input.GetKey(GameManager.gm.Left))
        {
            transform.position += Vector3.left * (Time.deltaTime * speed);
            _lastDir = Vector3.left * 90;
            _animator.SetBool(LeftKey, true);
        }
        CheckPos();

        if (_animator != null)
        {
            if (Input.GetKeyUp(GameManager.gm.Forward))
            {
                _animator.SetBool(UpKey, false);
            }
            if (Input.GetKeyUp(GameManager.gm.Backward))
            {
                _animator.SetBool(DownKey, false);
            }
            if (Input.GetKeyUp(GameManager.gm.Right))
            {
                _animator.SetBool(RightKey, false);
            }
            if (Input.GetKeyUp(GameManager.gm.Left))
            {
                _animator.SetBool(LeftKey, false);
            }
        }

        if (_cd <= Time.time)
        {
            Shoot();
            _cd = Time.time + cd;
            CircleAttack();
        }
    }

    private void UpdateTimer()
    {
        timerTxt.text = $"{((int)_timer / 60):00}:{(int)_timer % 60:00}";
    }

    private void Shoot()
    {
        Instantiate(_bullet, transform.position, Quaternion.Euler(0,0, -(_lastDir.x + _lastDir.y)));
    }
    private void CircleAttack()
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
            var healthScript = GetComponent<Damageable>();
            healthScript.currentHealth = healthScript.maxHealth; 
            _exp -= _expThreshold;
            _level += 1;
            _expThreshold += 5;
            lvl.text = $"Lvl {_level}";
            Upgrade();
            rangeSprite.transform.localScale = new Vector3(range,range, 0);
        }

        slider.value = _exp / (float)_expThreshold;
    }

    private void Upgrade()
    {
        List<Upgrade> temp= new();
        for (int i = 0; i < 2; i++)
        {
            temp.Add(_upgrades[Random.Range(0,_upgrades.Count)]);
        }
        _menu.Upgrade(temp);
    }

    private void PerformUpgrade(ref float stat, float val)
    {
        stat += val;
    }
}