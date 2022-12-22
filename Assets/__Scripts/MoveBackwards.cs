using UnityEngine;

public class MoveBackwards : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);     
    }

    private void Update()
    {
        if (!gameObject.CompareTag("EnemyBird") && PlayerController.Instance.IsGameActive == false) return;

        transform.Translate(-1 * _speed * Time.deltaTime, 0, 0);
    }
}
