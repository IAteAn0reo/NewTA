using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	public TMP_Text storyText;
	public TMP_Text choiceText;

	enum States
	{
		IntroQuestions, FavoriteColor, FavoriteGenre, FavoritePlace, RPG1, RPG1GP, RPG2, RPG3, RPG4, RPGWin, RPGLose, HorrorIntro,
		HorrorIntro2, Death1, PreCoolRoute, CoolRoute, SmartRoute, Death2, SmartCloset, CoolCloset, MazeIntermission, Death4, Closet2,
		Death5, Finale, Win, Death1Part2, Death, RPG1Results, RPG2Results, BossIntermission,
	}

	States myState;

	enum Inputs { Key, Space }
	Inputs myInput;

	bool complete;

	int rpg;
	int horror;

	string keyOne;
	string keyTwo;

	//int damage, health, enemyDamage, enemyHealth;

	[SerializeField] List<Sprite> backgrounds = new List<Sprite>();
	[SerializeField] GameObject background;
	[SerializeField] GameObject panel;

	private void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
		introQuestions();
	}

	private void Update()
	{
		if (complete && myInput == Inputs.Space && Input.GetKeyDown(KeyCode.Space))
		{
			complete = false;
			spaceSwitchState("");
		}
		if (complete && myInput == Inputs.Key)
		{
			if (Input.GetKeyDown(keyOne))
			{
				complete = false;
				keySwitchState(keyOne, keyTwo, keyOne, "", "");
			}
			if (Input.GetKeyDown(keyTwo))
			{
				complete = false;
				keySwitchState(keyOne, keyTwo, keyTwo, "", "");
			}
		}
	}

	//Intro
	#region
	void introQuestions()
	{
		myState = States.IntroQuestions;

		spaceSwitchState("Hello, welcome to the game! Let's start with a few simple questions.");
	}

	void favoriteColor(string input)
	{
		myState = States.FavoriteColor;

		if (input == "r")
		{
			horror++;
			favoriteGenre("");
		}
		else if (input == "g")
		{
			rpg++;
			favoriteGenre("");
		}
		else if (input == "") keySwitchState("r", "g", "", "Which of these do you prefer?",
			"Red - Press R <br> Green - Press G");
	}

	void favoriteGenre(string input)
	{
		myState = States.FavoriteGenre;

		if (input == "r")
		{
			horror++;
			favoritePlace("");
		}
		else if (input == "e")
		{
			rpg++;
			favoritePlace("");
		}
		else if (input == "") keySwitchState("r", "e", "", "What do you like to get out of video games?",
			"Fear - Press R <br> Fun - Press E");
	}

	void favoritePlace(string input)
	{
		myState = States.FavoritePlace;

		if (input == "o")
		{
			horrorIntro();
			choiceText.color = Color.white;
			storyText.color = Color.white;
			SetBG(1);
		}
		else if (input == "h")
		{
			horrorIntro();
			choiceText.color = Color.white;
			storyText.color = Color.white;
			SetBG(1);
		}
		else if (input == "") keySwitchState("o", "h", "", "Which of these places sound the most enjoyable?",
			"Outdoors - Press O <br> Haunted Mansion - Press H");
	}
	#endregion

	//RPG (THE WHOLE RPG REGION IS UNUSED)
	#region
	void RPGOne()
	{
		myState = States.RPG1;

		spaceSwitchState("Based on your answers, you seem like you would like RPG's, so here is an enemy!");
	}

	void RPGOneGameplay(string input)
	{
		myState = States.RPG1GP;

		if (input == "a") RPGOneResults();
		else if (input == "b") RPGOneGameplay("");
		else if (input == "") keySwitchState("a", "b", "", "", "");
	}

	void RPGOneResults()
	{
		myState = States.RPG1Results;

		spaceSwitchState("Congratulations! Here is your reward:");
	}

	//void RPGTwo(string input)
	//{
	//	myState = States.RPG2;

	//	if (input == "a") StartCoroutine(Attack(3));
	//	else if (input == "b") StartCoroutine(Defend());
	//	else if (input == "") keySwitchState("a", "b", "", "", "");
	//}

	//IEnumerator Defend()
	//{

	//}

	//IEnumerator Attack(int attacksRemaining)
	//{

	//}

	void RPGTwoResults()
	{
		myState = States.RPG2Results;

		spaceSwitchState("Congratulations! Here are your rewards:");
	}

	void BossIntermission()
	{
		myState = States.BossIntermission;

		spaceSwitchState("Now that you're prepared, get ready for a bossfight! Here are the controls: <br> WASD/Arrow Keys - Move <br> Space - Attack");
	}

	void Lose(string input)
	{
		myState = States.RPGLose;

		if (input == "1")
		{
			//enemyHealth = 4;
			//enemyDamage = 2;
			//damage = 3;
			//health = 3;
			//RPGTwo("");
		}
		else if (input == "2") Application.Quit();
		else if (input == "") keySwitchState("1", "2", "", "You died!", "Press 1 - Respawn <br> Press 2 - Quit Game");
	}

	void RPGWin(string input)
	{
		myState = States.RPGWin;

		if (input == "1") introQuestions();
		else if (input == "2") Application.Quit();
		else if (input == "") keySwitchState("1", "2", "", "Thanks for playing! <br> Another Cool Route: ", "Press 1 - Play Again <br> Press 2 - Quit Game");
	}

	#endregion

	//Horror
	#region
	void horrorIntro()
	{
		myState = States.HorrorIntro;
		panel.SetActive(true);
		SetBG(2);

		spaceSwitchState("After processing your answers, we have determined that you like horror games. " +
				"Here is something you might enjoy!");
	}

	void horrorIntro2(string input)
	{
		myState = States.HorrorIntro2;

		if (input == "b") SmartRoute("");
		else if (input == "h") PreCoolRoute("");
		else if (input == "")
		{
			keySwitchState("b", "h", "", "There is a being trying to break into the building you are in." +
				" It is almost through the door. What will you do?",
				"Barricade Door - Press B <br> Hide Immediately - Press H");
		}
	}

	void SmartRoute(string input)
	{

		SetBG(3);
		myState = States.SmartRoute;

		if (input == "d") SmartCloset("");
		else if (input == "t") StartCoroutine(Death2());
		else if (input == "")
		{
			keySwitchState("d", "t", "", "You now have more time. <br> Where will you hide?",
				"Hide in the Dresser - Press D <br> Hide under the Table - Press T");
		}
	}

	IEnumerator Death2()
	{
		myState = States.Death2;

		spaceSwitchState("You wait for a while, but it inevitably gets through the door. <br> I don't think a table is a very good hiding spot.");

		yield return new WaitForSeconds(2f);
		StartCoroutine(Jumpscare());
	}

	void SmartCloset(string input)
	{
		myState = States.SmartCloset;

		if (input == "w") SmartCloset2("");
		else if (input == "e") Win("");
		else
		{
			keySwitchState("w", "e", "", "It breaks through the door and can't find you, so it goes to check another room. <br> What will you do?",
				"Wait a little longer - Press W <br> Sprint for the exit - Press E");
		}
	}

	void SmartCloset2(string input)
	{
		myState = States.Closet2;

		if (input == "w") StartCoroutine(Death4());
		else if (input == "e") Win("");
		else
		{
			keySwitchState("w", "e", "", "You wait for a while, and still don't hear anything. <br> What will you do?",
				"Wait more - Press W <br> Go for the emergency exit - Press E");
		}
	}

	IEnumerator Death4()
	{
		myState = States.Death4;

		SequentialText("You wait for so long that it comes back to check more thoroughly.", 0, " ");

		yield return new WaitForSeconds(5f);
		StartCoroutine(Jumpscare());
	}


	IEnumerator Jumpscare()
	{
		SetBG(0);
		yield return new WaitForSeconds(1.5f);
		Death("");
	}

	void SetBG(int index)
	{
		background.GetComponent<Image>().sprite = backgrounds[index];
	}

	void Death(string input)
	{
		myState = States.Death;
		SetBG(1);

		if (input == "r")
		{
			introQuestions();
			horror = 0;
			rpg = 0;
		}
		else if (input == "q") Application.Quit();
		else if (input == "")
		{
			keySwitchState("r", "q", "", "You lost!", "Press R - Try Again <br> Press Q - Quit Game");
		}
	}

	void Win(string input)
	{
		myState = States.Win;

		if (input == "r")
		{
			introQuestions();
			horror = 0;
			rpg = 0;
		}
		else if (input == "q") Application.Quit();
		else if (input == "")
		{
			keySwitchState("r", "q", "", "Congratulations! <br> Another Cool Route: G-E-O", "Press R - Start from Questions <br> Press Q - Quit Game");
		}
	}

	void PreCoolRoute(string input)
	{
		SetBG(4);
		myState = States.PreCoolRoute;

		if (input == "t") StartCoroutine(Death1());
		else if (input == "d") CoolRoute("");
		else if (input == "")
		{
			keySwitchState("t", "d", "", "Where will you hide?",
				"Hide in the Dresser - Press D <br> Hide under the Table - Press T");
		}
	}

	IEnumerator Death1()
	{
		myState = States.Death1;

		keySwitchState("p", "o", "", "It breaks through the door right as you dive under. <br> I don't think a table was a very good hiding spot", " ");

		yield return new WaitForSeconds(2f);
		StartCoroutine(Jumpscare());
	}



	void CoolRoute(string input)
	{
		myState = States.CoolRoute;

		if (input == "w") CoolCloset("");
		else if (input == "l") MazeIntermission();
		else if (input == "") keySwitchState("w", "l", "", "It breaks through the door right as you get inside. <br> It saw you. " +
								"<br> You wait for a while until you hear footsteps leave the room. What will you do?",
								"Wait - Press W <br> Leave to find emergency exit - Press L");
	}

	void MazeIntermission()
	{
		myState = States.MazeIntermission;

		spaceSwitchState("Get Ready! <br> WASD/Arrow Keys - Move <br> Mouse - Rotate Camera <br> You can run through walls, " +
			"but the monster gets drastically faster when you do, so try not to at all costs!");
	}

	void CoolCloset(string input)
	{
		myState = States.CoolCloset;

		if (input == "m") StartCoroutine(Death4());
		else if (input == "e") MazeIntermission();
		else if (input == "") keySwitchState("m", "e", "", "You wait for a while, and still don't hear anything. <br> What will you do?",
			"Wait more - Press M <br> Leave to find the emergency exit - Press E");
	}
	#endregion

	//Templates
	#region
	void keySwitchState(string one, string two, string input, string finalText, string choices)
	{
		myInput = Inputs.Key;

		keyOne = one;
		keyTwo = two;

		if (input == "")
		{
			storyText.text = "";
			choiceText.text = "";

			StartCoroutine(SequentialText(finalText, 0, choices));
		}
		else
		{
			switch (myState)
			{
				case States.FavoriteColor:
					favoriteColor(input);
					break;
				case States.FavoriteGenre:
					favoriteGenre(input);
					break;
				case States.FavoritePlace:
					favoritePlace(input);
					break;
				case States.HorrorIntro2:
					horrorIntro2(input);
					break;
				case States.Win:
					Win(input);
					break;
				case States.Closet2:
					SmartCloset2(input);
					break;
				case States.Death:
					Death(input);
					break;
				case States.CoolRoute:
					CoolRoute(input);
					break;
				case States.SmartRoute:
					SmartRoute(input);
					break;
				case States.SmartCloset:
					SmartCloset(input);
					break;
				case States.PreCoolRoute:
					PreCoolRoute(input);
					break;
				case States.CoolCloset:
					CoolCloset(input);
					break;
				case States.RPG1GP:
					RPGOneGameplay(input);
					break;
				case States.RPG2:
					//RPGTwo(input);
					break;
			}
		}
	}

	void spaceSwitchState(string finalText)
	{
		myInput = Inputs.Space;

		storyText.text = "";
		choiceText.text = "";

		if (finalText == "")
		{
			switch (myState)
			{
				case States.IntroQuestions:
					favoriteColor("");
					break;
				case States.HorrorIntro:
					horrorIntro2("");
					break;
				case States.MazeIntermission:
					SceneManager.LoadScene(2);
					break;
				case States.RPG1:
					RPGOneGameplay("");
					break;
				case States.RPG1Results:
					//RPGTwo("");
					break;
				case States.RPG2Results:
					BossIntermission();
					break;
				case States.BossIntermission:
					SceneManager.LoadScene(3);
					break;
			}
		}
		else StartCoroutine(SequentialText(finalText, 0, "[SPACE TO CONTINUE]"));
	}

	IEnumerator SequentialText(string finalText, int iteration, string choicesText)
	{
		yield return new WaitForSeconds(0.02f);
		if (choicesText != "") storyText.text += finalText[iteration];
		else choiceText.text += finalText[iteration];

		if (iteration < finalText.Length - 1)
		{
			if (finalText[iteration + 1].Equals("<")) iteration += 4;
		}

		if (iteration >= finalText.Length - 1)
		{
			if (choicesText != "") StartCoroutine(SequentialText(choicesText, 0, ""));
			else complete = true;
		}
		else
		{
			StartCoroutine(SequentialText(finalText, iteration + 1, choicesText));
		}
	}

	IEnumerator Delay(float delay)
	{
		yield return new WaitForSeconds(delay);
	}
	#endregion
}