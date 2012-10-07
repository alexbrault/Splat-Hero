using UnityEngine;
using System.Collections;

public class SelectionMenu : MonoBehaviour {
	
	public GameObject buttonPrefab = null;
	public GameObject cursor = null;
	
	public GameObject rironman = null;
	public GameObject leTruc = null;
	public GameObject turquoiseMage = null;
	
	GameObject menuCursor;
	int xIndex = 0;
	int yIndex = 0;
	GameObject[,] buttons = new GameObject[2,2];
	
	int activeTeam = 1;
	int choosenTeam1 = 0;
	int choosenTeam2 = 0;
	Player.Character[] team1 = new Player.Character[2];
	Player.Character[] team2 = new Player.Character[2];
	
	// Use this for initialization
	void Start () {
	
		GameObject b1 = (GameObject)GameObject.Instantiate(buttonPrefab);
		b1.renderer.material.mainTexture = (Texture2D)Resources.Load("Icon/Rironman.icon");
		b1.transform.position = new Vector3(-20,20,0);
		CharacterButton button1 = b1.AddComponent<CharacterButton>();
		button1.character = Player.Character.RIRONMAN;
		
		GameObject b2 = (GameObject)GameObject.Instantiate(buttonPrefab);
		b2.AddComponent<CharacterButton>();
		b2.renderer.material.mainTexture = (Texture2D)Resources.Load("Icon/Truc.icon");
		b2.transform.position = new Vector3(20,20,0);
		CharacterButton button2 = b2.AddComponent<CharacterButton>();
		button2.character = Player.Character.LE_TRUC;
		
		GameObject b3 = (GameObject)GameObject.Instantiate(buttonPrefab);
		b3.AddComponent<CharacterButton>();
		b3.renderer.material.mainTexture = (Texture2D)Resources.Load("Icon/Rironman.icon");
		b3.transform.position = new Vector3(-20,-20,0);
		CharacterButton button3 = b3.AddComponent<CharacterButton>();
		button3.character = Player.Character.TURQUOISE_MAGE;
		
		GameObject b4 = (GameObject)GameObject.Instantiate(buttonPrefab);
		b4.AddComponent<CharacterButton>();
		b4.renderer.material.mainTexture = (Texture2D)Resources.Load("Icon/Rironman.icon");
		b4.transform.position = new Vector3(20,-20,0);
		CharacterButton button4 = b4.AddComponent<CharacterButton>();
		button4.character = Player.Character.RIRONMAN;
		
		buttons[0,0] = b1;
		buttons[1,0] = b2;
		buttons[0,1] = b3;
		buttons[1,1] = b4;
		
		menuCursor = (GameObject)GameObject.Instantiate(cursor);
		menuCursor.transform.position = new Vector3(-20,20,0);
		
		DetermineFirstTeam();
	}
	
	void DetermineFirstTeam()
	{
		activeTeam = Random.Range(1, 3);
		
		if(activeTeam == 1)
			menuCursor.renderer.material.mainTexture = (Texture2D)Resources.Load("Sprites/rightGoal");
		
		else
			menuCursor.renderer.material.mainTexture = (Texture2D)Resources.Load("Sprites/goalLeft");
	}
	
	// Update is called once per frame
	void Update () {
	
		MoveCursor();
		SelectCharacter();
	}
	
	void MoveCursor()
	{
		float xMovement = Input.GetAxis("Player1_MoveX");
		float zMovement = -Input.GetAxis("Player1_MoveZ");
		
		if(xMovement < 0 && xIndex > 0)
			xIndex--;
		
		if(xMovement > 0 && xIndex < 1)
			xIndex++;
		
		if(zMovement < 0 && yIndex > 0)
			yIndex--;
		
		if(zMovement > 0 && yIndex < 1)
			yIndex++;
		
		menuCursor.transform.position = buttons[xIndex, yIndex].transform.position;
	}
	
	void SelectCharacter()
	{
		if(Input.GetAxisRaw("Player1_Fire") > 0)
		{
			CharacterButton button = buttons[xIndex, yIndex].GetComponent<CharacterButton>();
			
			if(button.activated == true)
			{
				button.activated = false;
				
				if(xIndex == 0)
				{
					if(yIndex == 0)
					{
						button.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("Icon/RironmanBW.icon");
						SpawnCharacter(Player.Character.RIRONMAN);
					}
					
					else if(yIndex == 1)
					{
						button.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("Icon/RironmanBW.icon");
						SpawnCharacter(Player.Character.TURQUOISE_MAGE);
					}
				}
				
				else if(xIndex == 1)
				{
					if(yIndex == 0)
					{
						button.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("Icon/TrucBW.icon");
						SpawnCharacter(Player.Character.LE_TRUC);
					}
					
					else if(yIndex == 1)
					{
						button.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("Icon/RironmanBW.icon");
						SpawnCharacter(Player.Character.RIRONMAN);
					}
				}
			}
		}
	}
	
	void SpawnCharacter(Player.Character character)
	{
		GameObject obj = null;
		
		if(character == Player.Character.RIRONMAN)
		{
			obj = (GameObject)GameObject.Instantiate(rironman);
			
			obj.transform.Rotate(90, 180, 0);
		}
		
		else if(character == Player.Character.LE_TRUC)
		{
			obj = (GameObject)GameObject.Instantiate(leTruc);
			obj.transform.position = new Vector3(-80,20,0);
			obj.transform.Rotate(90, 180, 0);
		}
		
		else if(character == Player.Character.TURQUOISE_MAGE)
		{
			obj = (GameObject)GameObject.Instantiate(turquoiseMage);
			obj.transform.position = new Vector3(-80,20,0);
			obj.transform.Rotate(90, 180, 0);
		}
		
		if(activeTeam == 1)
		{
			obj.GetComponent<CharacterImage>().SetAnimation("RunRight");
			
			if(choosenTeam1 == 0)
				obj.transform.position = new Vector3(-80,20,0);
			
			else
				obj.transform.position = new Vector3(-55,5,0);
			
			choosenTeam1++;
			menuCursor.renderer.material.mainTexture = (Texture2D)Resources.Load("Sprites/goalLeft");
			activeTeam = 2;
		}
		
		else
		{
			obj.GetComponent<CharacterImage>().SetAnimation("RunLeft");
			
			if(choosenTeam2 == 0)
				obj.transform.position = new Vector3(80,20,0);
			
			else
				obj.transform.position = new Vector3(55,5,0);
			
			choosenTeam2++;
			menuCursor.renderer.material.mainTexture = (Texture2D)Resources.Load("Sprites/rightGoal");
			activeTeam = 1;
		}
	}
}
