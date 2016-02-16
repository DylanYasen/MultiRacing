using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour
{
    public float CurrentRotation { get { return transform.rotation.eulerAngles.z; } }
    public bool isMoving { get; private set; }
    public bool isBacking { get; private set; }

    [SerializeField]
    float acceleration = 50;
    [SerializeField]
    float deceleration = 300;
    [SerializeField]
    float maxSpeed = 500;
    [SerializeField]
    float rotateSpeed = 200;

    float currentSpeed = 0;

    Vector2 currentDir;

    Rigidbody2D body2d;

    void Awake()
    {
        body2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateMovement();
        UpdateRotation();
    }

    void UpdateMovement()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            currentSpeed = maxSpeed;
            currentDir = -transform.up;

            isMoving = true;
            isBacking = true;
        }

        else if (Input.GetKey(KeyCode.UpArrow))
        {
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            currentSpeed = maxSpeed;
            currentDir = transform.up;

            isMoving = true;
            isBacking = false;
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= deceleration * Time.deltaTime;
                if (currentSpeed < 0) currentSpeed = 0;

                isMoving = false;
                isBacking = false;
            }
        }

        body2d.velocity = currentDir * currentSpeed;
    }

    void UpdateRotation()
    {
        if (!isMoving)
            return;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            float rotation = CurrentRotation;

            if (isBacking)
                rotation -= rotateSpeed * Time.deltaTime;
            else
                rotation += rotateSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            float rotation = CurrentRotation;

            if (isBacking)
                rotation += rotateSpeed * Time.deltaTime;
            else
                rotation -= rotateSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.up);
    }
}

