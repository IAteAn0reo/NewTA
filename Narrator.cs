using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{
    [SerializeField] GameObject projectile, spawner;

    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] Transform player;
    int punches;
    bool returnToMiddle;
    bool rotating;

    int attack;

    void Start()
    {
        ChooseAttack();
    }

    void ChooseAttack()
	{
        int previousAttack = attack;
        attack = Random.Range(1, 3);

        while (previousAttack == attack)
		{
            attack = Random.Range(1, 3);
        }

        switch (attack)
		{
            case 1:
                spriteRenderer.sprite = sprites[0];

                StartCoroutine(Shoot(0));
                break;
            case 2:
                spriteRenderer.sprite = sprites[1];

                StartCoroutine(PunchStageOne());
                break;
		}
	}

    IEnumerator Shoot(int i)
    {
        yield return new WaitForSeconds(0.1f);
        transform.eulerAngles += new Vector3(0f, 0f, 36);

        GameObject proj = Instantiate(projectile, spawner.transform.position, Quaternion.identity);

        proj.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 90);

        if (i < 9) StartCoroutine(Shoot(i + 1));
        else StartCoroutine(Cooldown(0.4f));
    }

	private void Update()
	{
        if (rotating)
		{
            Vector3 vectorToTarget = player.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 100000f);
        }
        
    }

	IEnumerator PunchStageOne()
	{
        rotating = true;
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(PunchStageTwo());
	}

    IEnumerator PunchStageTwo()
	{
        rotating = false;
        punches++;

        Vector3 target = player.position;
        yield return new WaitForSeconds(0.5f);
        transform.position = target;

        punches = 0;
        transform.eulerAngles = Vector3.zero;
        ChooseAttack();
	}

    IEnumerator Cooldown(float time)
	{
        yield return new WaitForSeconds(time);
        ChooseAttack();
	}
}
