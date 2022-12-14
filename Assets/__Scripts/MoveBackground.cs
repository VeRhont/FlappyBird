using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _length;

    [SerializeField] private Vector2 _startPoint;

    private void Start()
    {
        _startPoint = transform.position;
    }

    private void Update()
    {
        if (PlayerController.Instance.IsGameActive == false) return;

        transform.Translate(-_speed * Time.deltaTime, 0, 0);

        if (_startPoint.x - _length >= transform.position.x)
        {
            transform.position = _startPoint;
        }
    }
}