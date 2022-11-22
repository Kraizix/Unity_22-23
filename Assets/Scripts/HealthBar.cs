using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private MaterialPropertyBlock _matBlock;
    private MeshRenderer _meshRenderer;
    private Damageable _damageable;
    private static readonly int Fill = Shader.PropertyToID("_Fill");

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _matBlock = new MaterialPropertyBlock();
        _damageable = GetComponentInParent<Damageable>();
    }

    private void Update()
    {
        if (_damageable.currentHealth < _damageable.maxHealth)
        {
            _meshRenderer.enabled = true;
            UpdateParams();
        }
        else
        {
            _meshRenderer.enabled = false;
        }
    }

    private void UpdateParams()
    {
        _meshRenderer.GetPropertyBlock(_matBlock);
        _matBlock.SetFloat(Fill, _damageable.currentHealth / (float)_damageable.maxHealth);
        _meshRenderer.SetPropertyBlock(_matBlock);
    }
}
