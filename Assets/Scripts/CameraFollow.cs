using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float lerpSpeed = 1.0f;

    private Vector3 _offset;

    private Vector3 _targetPos;

    private void Start()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _offset = transform.position - target.position;
    }

    private void Update()
    {
        if (!target) return;
        _targetPos = target.position + _offset;
        _targetPos.x = _targetPos.x <= -10.28f ? -10.28f : _targetPos.x >= 10.28f ? 10.28f : _targetPos.x;
        _targetPos.y = _targetPos.y <= -5 ? -5 : _targetPos.y >= 5 ? 5 : _targetPos.y;
        transform.position = Vector3.Lerp(transform.position, _targetPos, lerpSpeed * Time.deltaTime);
    }
}