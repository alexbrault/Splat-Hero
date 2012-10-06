using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCamera : MonoBehaviour 
{
    private Entity currentBall;
    private GameObject leftBorder;
    private GameObject rightBorder;

    private List<Entity> player = new List<Entity>();

    public GameObject player1_marker;
    public GameObject player2_marker;

    private GameObject p1Marker;
    private GameObject p2Marker;

    private float resolutionCorrection;
    private float heroPointerScreenPosX, heroNotVisibleScreenPosX;


	// Use this for initialization
	void Start () 
    {
        leftBorder = GameObject.Find("wallLeft");
        rightBorder = GameObject.Find("wallRight");
 
        resolutionCorrection = transform.camera.aspect * transform.camera.orthographicSize;
        heroPointerScreenPosX = resolutionCorrection - 5;
        heroNotVisibleScreenPosX = resolutionCorrection + 5;
	}

    public void SetNewTarget(Entity target)
    {
        currentBall = target;
    }

    public void RegisterPlayer(Entity aPlayer)
    {
        player.Add(aPlayer);
     
        if(player.Count == 1)
            p1Marker = GameObject.Instantiate(player1_marker) as GameObject;
        else
            p2Marker = GameObject.Instantiate(player2_marker)as GameObject;
    }

	// Update is called once per frame
	void Update () 
    {
        float player1PosX = player[0].transform.position.x;
        float player1PosZ = player[0].transform.position.z;

        if (player1PosX >= transform.position.x + heroNotVisibleScreenPosX)
        {
            p1Marker.renderer.enabled = true;
            p1Marker.transform.position = new Vector3(transform.position.x + heroPointerScreenPosX, 0.0f, player1PosZ);
        }

        else if (player1PosX <= transform.position.x - heroNotVisibleScreenPosX)
        {
            p1Marker.renderer.enabled = true;
            p1Marker.transform.position = new Vector3(transform.position.x - heroPointerScreenPosX, 0.0f, player1PosZ);
        }

        else
        {
            p1Marker.renderer.enabled = false;
        }



        float player2PosX = player[1].transform.position.x;
        float player2PosZ = player[1].transform.position.z;

        if (player2PosX >= transform.position.x + heroNotVisibleScreenPosX)
        {
            p2Marker.renderer.enabled = true;
            p2Marker.transform.position = new Vector3(transform.position.x + heroPointerScreenPosX, 0.0f, player2PosZ);
        }

        else if (player2PosX <= transform.position.x - heroNotVisibleScreenPosX)
        {
            p2Marker.renderer.enabled = true;
            p2Marker.transform.position = new Vector3(transform.position.x - heroPointerScreenPosX, 0.0f, player2PosZ);
        }

        else
        {
            p2Marker.renderer.enabled = false;
        }



        if (currentBall != null)
        {
            float currentBallPosX = currentBall.transform.position.x*0.6f;

            transform.position = new Vector3(currentBallPosX, transform.position.y, transform.position.z);
        }	
	}
}
