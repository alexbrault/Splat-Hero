using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour 
{
    private Entity currentBall;
    private GameObject leftBorder;
    private GameObject rightBorder;

	// Use this for initialization
	void Start () 
    {
        leftBorder = GameObject.Find("wallLeft");
        rightBorder = GameObject.Find("wallRight");
	}

    public void SetNewTarget(Entity target)
    {
        currentBall = target;
    }

	// Update is called once per frame
	void Update () 
    {
        if (currentBall != null)
        {
            float currentBallPosX = currentBall.transform.position.x;

            if( currentBallPosX > (leftBorder.transform.position.x + 120) && currentBallPosX < (rightBorder.transform.position.x - 120) )
                transform.position = new Vector3(currentBallPosX, transform.transform.position.y, transform.position.z);
        }	
	}
}
