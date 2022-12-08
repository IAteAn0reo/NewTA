using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
	[SerializeField] Transform player;
    public List<Vector3> positions;
	[SerializeField] Material floorColor;

	public bool move;
	float speed = 2f;

	private void Start()
	{
		StartCoroutine(Wait());
	}

	void Update()
    {
		if (Vector3.Distance(transform.position, player.position) < 2f)
		{
			floorColor.color = Color.black;
			player.GetComponent<PlayerController>().Finish();
		}

		if (move)
		{
			transform.position = Vector3.Lerp(transform.position, positions[0], speed * Time.deltaTime);

			if (positions.Count == 1)
			{
				player.GetComponent<PlayerController>().Finish();
			}
			else if (Vector3.Distance(transform.position, positions[0]) < 0.1f)
			{
				positions.Remove(positions[0]);
				speed += 0.5f;
				floorColor.color += new Color(.02f, 0f, 0f);
			}
		}
    }

    IEnumerator Wait()
	{
        yield return new WaitForSeconds(15f);
		move = true;
	}
}
