using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoState<PlayerController>
{
    [Header("Tank")]
    [SerializeField]
    Transform barrel;

    [Header("Movement")]
    [SerializeField]
    float speed = 8.5F;

    [Header("Animation")]
    [SerializeField]
    Animator animator;

    [Header("Combat")]
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform[] firePoints;

    [SerializeField]
    [Range(0.1F, 1.0F)]
    float fireRate = 0.3F;

    [SerializeField]
    Animator[] wheelAnimators;

    //Es para hacer trabajo con fisicas
    Rigidbody2D _rb;

    Camera CAMERA;


    Vector2 _direction;
    Vector2 _mousePosition;

    float _fireTimer;

    private bool _gameIsOver = false;

    protected override void Awake()
    {
        base.Awake();

         CAMERA = Camera.main;

        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        _mousePosition = CAMERA.ScreenToWorldPoint(Input.mousePosition);
        HandleBarrelRotation();

        _fireTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && _fireTimer <= 0.0F)
        {
            Shoot();
            _fireTimer = fireRate;
        }
    }

    void FixedUpdate()
    {
        if (_direction.sqrMagnitude == 0) 
        {
            return;
        }

        if (animator != null) {
            animator.SetFloat("Horizontal", _direction.x);
            animator.SetFloat("Vertical", _direction.y);
            animator.SetFloat("Speed", _direction.sqrMagnitude);
        }

        if (wheelAnimators.Length > 0)
        {
            foreach (Animator wheelAnimator in wheelAnimators)
            {
                wheelAnimator.SetFloat("Speed", _direction.sqrMagnitude);
            }
        }
        _rb.MovePosition(_rb.position + _direction * speed * Time.fixedDeltaTime);
        HandleRotaion();
    }

    void Shoot()
    {
        foreach(Transform firePoint in firePoints)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    void HandleRotaion() {
        float angle =
            _direction.x > 0 && _direction.y == 0
            ? 90.0F
            : _direction.x > 0 && _direction.y > 0
            ? 45.0F
            : _direction.x > 0 && _direction.y < 0
            ? 135.0F
            : _direction.x == 0 && _direction.y < 0
            ? 180.0F
            : _direction.x < 0 && _direction.y < 0
            ? 225.0F
            : _direction.x < 0 && _direction.y == 0
            ? 270.0F
            : _direction.x < 0 && _direction.y > 0
            ? 315.0F
            : 0.0F;
        transform.rotation = Quaternion.Euler(new Vector3(0.0F, 0.0F, -angle));
    }

    void HandleBarrelRotation()
    {
        float angle = Mathf.Atan2(_mousePosition.y - barrel.position.y,
            _mousePosition.x - barrel.position.x) * Mathf.Rad2Deg - 90.0F;
        barrel.rotation = Quaternion.Euler(new Vector3(0.0F, 0.0F, angle));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyTank") && !_gameIsOver)
        {
            Debug.Log("Game Over");

            _gameIsOver = true;

            Time.timeScale = 0F;
        }
    }
}
