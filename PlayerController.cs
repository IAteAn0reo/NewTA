using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed, sensitivity;
	[SerializeField] Transform view;
	[SerializeField] Monster monster;
	[SerializeField] GameObject menu, finishScreen, information;

	bool finished = true;
	bool paused;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		StartCoroutine(RecordPos());
	}

	void Update()
    {
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);

		if (!paused) transform.eulerAngles += new Vector3(0f, Time.deltaTime * Input.GetAxis("Mouse X") * sensitivity, 0f);

		if (Input.GetKeyDown(KeyCode.Escape) && !finished)
		{
			if (paused)
			{
				paused = false;
				sensitivity = menu.transform.GetChild(1).GetComponent<Scrollbar>().value * 1500f;
				StartCoroutine(RecordPos());
				monster.move = true;
				menu.SetActive(false);
				Cursor.lockState = CursorLockMode.Locked;
			}
			else
			{
				Cursor.lockState = CursorLockMode.None;
				paused = true;
				monster.move = false;
				menu.SetActive(true);
			}
		}

		if (finished)
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				SceneManager.LoadScene(0);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				Application.Quit();
			}
		}
    }

	public void Finish()
	{
		finished = true;
		Cursor.lockState = CursorLockMode.None;
		finishScreen.SetActive(true);
	}

	public void Information()
	{
		if (information.activeInHierarchy)
		{
			information.SetActive(false);
		}
		else
		{
			information.SetActive(true);
		}
	}

	IEnumerator RecordPos()
	{
		yield return new WaitForSeconds(0.5f);
		monster.positions.Add(transform.position);
		if (!paused) StartCoroutine(RecordPos());
	}
}
