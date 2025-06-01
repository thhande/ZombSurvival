
using UnityEngine;

public class SlashFx : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform _transform;
    public float delay = 0.3f;
    private void Start() => Destroy(gameObject, delay);
    private void Update()
    {
        if (_transform.position == null) return;
        transform.position = _transform.position;

    }
    public void SetTransform(Transform transform)
    {
        _transform = transform;
        spriteRenderer.flipX = _transform.localPosition.x < 0;
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
    }
}
