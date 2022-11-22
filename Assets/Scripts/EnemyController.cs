using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float damage = 5.0f;

    public float speed = 5f;

    // public float targetDistance = 0.1f;

    private Rigidbody2D _rgbd;

    private Transform _target;
    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rgbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (_target.position - transform.position).normalized;
        _rgbd.velocity = dir * speed;
        //transform.Translate( GetMovementIntention() * 0.005f);
    }

    /*public Vector3 GetMovementIntention()
    {
        var position1 = transform.position;
        Vector3 intention = Vector3.zero;
        Vector3 dir = (_target.position - position1).normalized;
        float dist = Vector3.Distance(_target.position, position1);
        float springStrength = (dist - targetDistance);
        intention += dir * springStrength;
        foreach (EnemyController other in GameManager.gm.mobs)
        {
            if (other == this) continue;
            var position = other.transform.position;
            dir = (position - position1).normalized;
            dist = Vector3.Distance(position, transform.position);
            springStrength = 1f / (1f + dist * dist * dist);
            intention -= dir * (0.2f * springStrength);
        }
        if (intention.magnitude < 0.5f)
        {
            return Vector3.zero;
        }
        return intention.normalized;
    }*/
}
