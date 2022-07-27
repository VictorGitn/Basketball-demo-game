using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 public enum RotationYAxis
{
    ferstPosRotate = -90,
    secondPosRotate = -160,
    thirdPosRotate = -20,
    fourthPosRotate = -90
}
public class BallScript : MonoBehaviour
{
    public Transform ring;
    public GameManager gameManager;
    [SerializeField] float throwForce = 10;
    [SerializeField] float throwAngle = 10;
    [SerializeField] int countInGround;

    public float ballRotateY;
    private Rigidbody ballRb;
    private bool ballInRing;
    private bool ballInHands = true;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameManager.GetComponent<GameManager>(); 
        ballRb = GetComponent<Rigidbody>();
        ballRb.isKinematic = true;
        countInGround = 0;
        ballInRing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameStart && Input.GetKeyDown(KeyCode.Space) && ballInHands)
        {
            ThrowBall();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            RespawnBall();
        }
    }

    void ThrowBall()
    {
        ballRb.isKinematic = false;
        ballRb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        ballInHands = false;
    }

    public void AdjustForce(float force)
    {
        throwForce = force;
    }

    public void AdjustRotationZ(float angle)
    {
        throwAngle = -angle;
        transform.rotation = Quaternion.Euler(new Vector3(throwAngle, ballRotateY, 0));
    }
    public void RespawnBall()
    {
        ballRb.isKinematic = true;
        transform.position = gameManager.currentThrowPosition;
        ballInHands = true;
        transform.rotation = Quaternion.Euler(new Vector3(throwAngle, ballRotateY, 0));
    }
    
    public void ÑhangeRotationY(RotationYAxis rotationPos)
    {
        ballRotateY = (float)rotationPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            countInGround++;
            if (countInGround > 1)
            {
                countInGround = 0;
                RespawnBall();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(transform.position.y > other.gameObject.transform.position.y)
        {
            ballInRing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ballInRing && transform.position.y < other.gameObject.transform.position.y)
        {
            gameManager.AddPoint();
        }
    }
}
