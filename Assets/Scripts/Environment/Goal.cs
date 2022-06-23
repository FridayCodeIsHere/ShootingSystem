using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour, IDemageable
{
    [SerializeField] private int _health;
    [SerializeField] private TextMeshProUGUI _healthText;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_health < 0)
        {
            _health = 0;
        }
    }
    #endregion

    private void Start()
    {
        RenderHealth();
    }

    public void ReduceHealth(int value)
    {
        int damage = value > _health ? _health : value;
        _health -= damage;
        RenderHealth();
        if (_health <= 0f)
        {
            Invoke(nameof(Dead), 0.2f);
        }

    }

    private void RenderHealth()
    {
        if (_healthText != null)
        {
            _healthText.text = _health.ToString();
        }
        else
        {
            Debug.LogWarning("You did not add the Text Component!");
        }
    }

    public void Dead()
    {
        Destroy(this.gameObject);
    }
}
