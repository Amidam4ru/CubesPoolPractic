using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]

public class ColorChanging : MonoBehaviour
{
    [SerializeField] private Color _startColor; 

    private MeshRenderer _meshRenderer;
    private ColorChanging _colorChanging;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _colorChanging = GetComponent<ColorChanging>();
    }

    private void OnEnable()
    {
        _meshRenderer.material.color = _startColor; 
    }

    public void ChangeColor()
    {
        _meshRenderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
}
