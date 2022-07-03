using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private Goal _globalPrefab;
    [SerializeField] private Vector2 _spawnerZone;
    private Vector2 _firstPos;
    private Vector2 _lastPos;
    private Goal _currentGoal;

    private void Start()
    {
        _firstPos.x = transform.position.x - _spawnerZone.x / 2 + _globalPrefab.ShapeSize.x;
        _firstPos.y = transform.position.y + _spawnerZone.y / 2 - _globalPrefab.ShapeSize.y;

        _lastPos.x = _firstPos.x + _spawnerZone.x / 2 - _globalPrefab.ShapeSize.x;
        _lastPos.y = _firstPos.y - _spawnerZone.y / 2 + _globalPrefab.ShapeSize.y;

        InitGoal();
    }

    private void InitGoal()
    {
        _currentGoal = Instantiate(_globalPrefab, GetRandomPosition(), Quaternion.identity, transform);
    }

    public void SetGoalPosition()
    {
        _currentGoal.transform.position = GetRandomPosition();
    }

    private Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(_firstPos.x, _lastPos.x);
        float randomY = Random.Range(_firstPos.y, _lastPos.y);
        return new Vector2(randomX, randomY);
    }

    private void OnDrawGizmos()
    {
        Color redColor = new Color(0.6f, 0, 0, 0.3f);
        Gizmos.color = redColor;
        Gizmos.DrawCube(transform.position, _spawnerZone);
    }
}
