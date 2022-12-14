using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public bool IsGameActive = true;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _diveForce;

    [SerializeField] private ParticleSystem _deathParticles;
    [SerializeField] private ParticleSystem _eggParticles;

    [SerializeField] private TextMeshProUGUI _scoreText;

    private AudioSource _audioSource;
    private Rigidbody2D _playerRb;
    private Animator _animator;

    [Header("AudioClips")]
    [SerializeField] private AudioClip _diveSound;
    [SerializeField] private AudioClip _UpSound;
    [SerializeField] private AudioClip _pickUpSound;
    [SerializeField] private AudioClip _deathSound;

    private int _score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _playerRb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (IsGameActive == false) return;

        if (Input.GetButtonDown("Jump"))
        {
            _audioSource.PlayOneShot(_UpSound);
            _playerRb.velocity = Vector2.up * _jumpForce;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dive();
        }
    }

    private void Dive()
    {
        _audioSource.PlayOneShot(_diveSound);
        _playerRb.velocity = -Vector2.up * _diveForce;

        _animator.SetTrigger("Dive");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        IsGameActive = false;
        _audioSource.PlayOneShot(_deathSound);

        Instantiate(_deathParticles, transform.position, _deathParticles.transform.rotation);

        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        _animator.SetTrigger("Death");

        yield return new WaitForSeconds(1);
        _animator.enabled = false;

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
        SceneManager.LoadScene("Game");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pass"))
        {
            _audioSource.PlayOneShot(_pickUpSound);

            _score += 1;
            UpdateScore();
        }
        else if (collision.CompareTag("Collectable"))
        {
            _eggParticles.transform.position = collision.transform.position;
            _eggParticles.Play();
            _audioSource.PlayOneShot(_pickUpSound);

            Destroy(collision.gameObject);

            _score += 5;
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        _scoreText.SetText($"Score: {_score}");
    }
}