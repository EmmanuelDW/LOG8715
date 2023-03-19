using UnityEngine;
using UnityEngine.Serialization;

public class Circle : MonoBehaviour
{
    [FormerlySerializedAs("I")]
    [HideInInspector]
    public int i;

    [FormerlySerializedAs("J")]
    [HideInInspector]
    public int j;

    public float Health { get; private set; }

    private const float BaseHealth = 1000;

    private const float HealingPerSecond = 1;
    private const float HealingRange = 3;

    private SpriteRenderer _spriteRenderer;
    private Grid _grid;
    private readonly Collider2D[] _nearbyColliders = new Collider2D[13];

    // Start is called before the first frame update
    private void Start()
    {
        Health = BaseHealth;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _grid = GameObject.FindObjectOfType<Grid>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateColor();
        HealNearbyShapes();
    }

    private void UpdateColor()
    {
        _spriteRenderer.color = _grid.Colors[i, j] * Health / BaseHealth;
    }

    private void HealNearbyShapes()
    {
        var count = Physics2D.OverlapCircleNonAlloc(transform.position, HealingRange, _nearbyColliders);
        var healingPerFrame = HealingPerSecond * Time.deltaTime;
        for (int i = 0; i < count; i++)
        {
            if (!_nearbyColliders[i])
                continue;

            var circle = _nearbyColliders[i].GetComponent<Circle>();
            if (circle)
            {
                circle.ReceiveHp(healingPerFrame);
            }
        }
    }



    public void ReceiveHp(float hpReceived)
    {
        Health += hpReceived;
        Health = Mathf.Clamp(Health, 0, BaseHealth);
    }
}
