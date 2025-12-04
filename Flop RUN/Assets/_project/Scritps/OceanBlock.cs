using System;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleTypeEnum
{
	Ocean,
	Dolphin,
	Jellyfish,
	Shark,
	Whale,
	Turtle,
	Boat,
	Mine
}

public class OceanBlock : MonoBehaviour
{
    public List<GameObject> Obstacles;

	public OceanSpawner Spawner;
	public float DestroyTime;
	public ObstacleTypeEnum ObstacleType;

	public void Start()
	{
		Destroy(gameObject,DestroyTime);
	}

	//private void Update()
	//{
	//	if (!GameManager.instance.IsGameStart)
	//		return;

	//	if (!Spawner)
	//		return;
 //       transform.position += (Vector3)(Spawner.BlockMoveSpeed * Time.deltaTime * Vector2.left);
	//}

	public void Setup(OceanSpawner spawner,bool obstacleActive = false, ObstacleTypeEnum type = ObstacleTypeEnum.Ocean)
	{
		Spawner = spawner;
		ObstacleType = type;

		if (obstacleActive)
		{
			Obstacles[(int)type -1].SetActive(true);
		}
	}
}
