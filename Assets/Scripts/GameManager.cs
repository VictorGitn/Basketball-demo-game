using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Vector3> throwPositions;
    public Vector3 currentThrowPosition;
    public BallScript ball;
    public Text counterText;
    public Camera mainCamera;
    public GameObject StartScreen;
    public Button button;
    public bool isGameStart;
   
    private int indexPosition;
    private int Count = 0;
    // Start is called before the first frame update
    void Start()
    {
        ball = ball.GetComponent<BallScript>();
        indexPosition = 0;
        currentThrowPosition = throwPositions[indexPosition];
        Count = 0;
        ball.ÑhangeRotationY(RotationYAxis.ferstPosRotate);
        isGameStart = false;
        button.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        isGameStart = true;
        StartScreen.gameObject.SetActive(false);
    }

    public void NextThrowPosition()
    {
        if (indexPosition < throwPositions.Count)
        {
            indexPosition++;
            if (indexPosition > 3)
                indexPosition = 0;
            currentThrowPosition = throwPositions[indexPosition];
            NextCameraPos();
        }
    }

    void NextCameraPos()
    {
        switch (indexPosition)
        {
            case 0:
                mainCamera.transform.position = currentThrowPosition + new Vector3(2, 0.5f, 0);
                mainCamera.transform.rotation = Quaternion.Euler(new Vector3(-7, ball.ballRotateY, 0));
                break;
            case 1:
                mainCamera.transform.position = currentThrowPosition + new Vector3(2,0.5f,2);
                mainCamera.transform.rotation = Quaternion.Euler(new Vector3(-7, ball.ballRotateY, 0));
                break;
            case 2:
                mainCamera.transform.position = currentThrowPosition + new Vector3(2, 0.5f, -2);
                mainCamera.transform.rotation = Quaternion.Euler(new Vector3(-7, ball.ballRotateY, 0));
                break;
            case 3:
                mainCamera.transform.position = currentThrowPosition + new Vector3(2, 0.5f, 0);
                mainCamera.transform.rotation = Quaternion.Euler(new Vector3(-7, ball.ballRotateY, 0));
                break;
        }
    }
    
    public void AddPoint()
    {
        Count += 1;
        counterText.text = "Count : " + Count;
        switch (indexPosition)
        {
            case 0:
                ball.ÑhangeRotationY(RotationYAxis.secondPosRotate);
                break;
            case 1:
                ball.ÑhangeRotationY(RotationYAxis.thirdPosRotate);
                break;
            case 2:
                ball.ÑhangeRotationY(RotationYAxis.fourthPosRotate);
                break;
            case 3:
                ball.ÑhangeRotationY(RotationYAxis.ferstPosRotate);
                break;
        }
        NextThrowPosition();
    }
}
