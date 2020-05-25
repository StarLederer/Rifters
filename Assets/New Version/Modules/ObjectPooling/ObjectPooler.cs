﻿using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : NetworkBehaviour
{
	//
	// Singleton
	private static ObjectPooler instance;
	public static ObjectPooler Instance
	{
		get
		{
			if (instance == null)
				Debug.LogError("Object pooler has not been initialized. You must initialize it manually in the scene");

			return instance;
		}
	}

	//
	// Public variables
	public Dictionary<string, Queue<GameObject>> poolDictionary;

	/// <summary>
	/// Ensures singleton functionality and initializes poolDictionary
	/// </summary>
	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("Only one instance of ObjectPooler is allowed. Destroying the new instance...");
		}
		else
			instance = this;

		poolDictionary = new Dictionary<string, Queue<GameObject>>();
	}

	/// <summary>
	/// Creates a new object pool
	/// </summary>
	/// <param name="tag">Identificator of the pool</param>
	/// <param name="prefab">Prefab the pool is going to create clones of</param>
	/// <param name="size">Amount of clones created on initialiation</param>
	public void CreateNewPool(string tag, GameObject prefab, int size)
	{
		if (poolDictionary.ContainsKey(tag)) return;

		Queue<GameObject> objectPool = new Queue<GameObject>();

		//for (int i = 0; i < size; i++)
		for (int i = 0; i < 1; i++)
		{
			GameObject obj = Instantiate(prefab, transform);
			obj.SetActive(false);
			objectPool.Enqueue(obj);
		}

		poolDictionary.Add(tag, objectPool);
	}

	/// <summary>
	/// Spawns a game object using pool optimization
	/// </summary>
	/// <param name="tag">Identificator of the pool to get the an object from</param>
	/// <returns>The spawned game object</returns>
	//public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
	//{
	//	if (!poolDictionary.ContainsKey(tag))
	//	{
	//		Debug.LogError("Pool with tag " + tag + " doesn't exist.");
	//		return null;
	//	}

	//	GameObject objectToSpawn = poolDictionary[tag].Peek();

		// Checking if an object is available to take
		//if (!objectToSpawn.gameObject.activeSelf)
		//{
			// taking an existing object
		//	poolDictionary[tag].Dequeue();
		//	objectToSpawn.transform.position = position;
		//	objectToSpawn.transform.rotation = rotation;
		//}
		//else
		//{
			// creating a new object because all enqueued objects are in use
	//		objectToSpawn = Instantiate(objectToSpawn, position, rotation, transform);
	//	NetworkServer.Spawn(objectToSpawn);
		//}		

	//	objectToSpawn.SetActive(true);

		// putting the object in the queue
		//poolDictionary[tag].Enqueue(objectToSpawn);

	//	return objectToSpawn;
	//}
}
