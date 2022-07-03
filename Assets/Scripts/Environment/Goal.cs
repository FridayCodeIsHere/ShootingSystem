using UnityEngine;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
public class Goal : MonoBehaviour, IDemageable
{
    [SerializeField] private int _health;
    [SerializeField] private TextMeshProUGUI _healthText;
    private int _currentHealth;
    public Vector2 ShapeSize { get; private set; }

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_health < 0)
        {
            _health = 0;
        }
    }
    #endregion

    private void Awake()
    {
        ShapeSize = GetComponent<SpriteRenderer>().size;
    }

    private void Start()
    {
        _currentHealth = _health;
        RenderHealth();
        
    }

    public void ReduceHealth(int value)
    {
        int damage = value > _currentHealth ? _currentHealth : value;
        _currentHealth -= damage;
        RenderHealth();
        if (_currentHealth <= 0f)
        {
            Invoke(nameof(Dead), 0.2f);
        }

    }

    private void RenderHealth()
    {
        if (_healthText != null)
        {
            _healthText.text = _currentHealth.ToString();
        }
        else
        {
            Debug.LogWarning("You did not add the Text Component!");
        }
    }

    public void Dead()
    {
        if (transform.root.TryGetComponent(out GoalManager goalManager))
        {
            goalManager.SetGoalPosition();
            _currentHealth = _health;
            RenderHealth();
        }
    }
}
