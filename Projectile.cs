using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool shrapnel;
	[SerializeField] GameObject child;

	List<Vector3> positions = new List<Vector3>();

	private void Start()
	{
		StartCoroutine(Move());
	}

	IEnumerator Move()
	{
		yield return new WaitForSeconds(0.1f);
		transform.Translate(Vector3.left * (shrapnel ? 0.3f : 0.6f));

		positions.Add(transform.position);

		StartCoroutine(Move());
	}

	private void Update()
	{
		if (positions.Count > 4 && shrapnel)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Border"))
		{
			if (shrapnel)
			{
				Destroy(gameObject);
			}
			else
			{
				for (int i = 0; i < 20; i++)
				{
					Transform tempChild = Instantiate(child, positions[positions.Count - 2], Quaternion.identity).transform;

					tempChild.eulerAngles = new Vector3(0f, 0f, (i * 18) - 90);
					tempChild.GetComponent<Projectile>().shrapnel = true;
				}

				Destroy(gameObject);
			}
		} 
	}
}
