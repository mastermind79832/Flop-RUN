using System;
using UnityEngine;

public class OceanSpawner : MonoBehaviour
{
    public OceanBlock BlockPrefab;

    public float BlockMoveSpeed;
	public float SpawnInterval;
	public float YPosition;

	private float timer;

	private float NextXTranform;

	public void GameStart()
	{
		NextXTranform = transform.position.x + SpawnInterval;
	}

	private void Update()
	{
		if (!GameManager.instance.IsGameStart)
			return;
		//TimeBased();
		LocationBased();
	}

	private void LocationBased()
	{
		if (transform.position.x >= NextXTranform)
		{
			SpawnBlock(NextXTranform);
			NextXTranform += SpawnInterval;
		};
	}

	private void TimeBased()
	{
		timer += Time.deltaTime;

		if (timer > SpawnInterval)
		{
			//SpawnBlock();
			timer = 0;
		}
	}

	private void SpawnBlock(float nextXTranform)
	{
		var block = Instantiate(BlockPrefab, new Vector3(nextXTranform, YPosition, 0), Quaternion.identity);

		if (UnityEngine.Random.Range(0, 100) > 40)
		{
			block.Setup(this, true, (ObstacleTypeEnum)UnityEngine.Random.Range(1,Enum.GetValues(typeof(ObstacleTypeEnum)).Length));
		}

		block.Setup(this, false);
	}
}
