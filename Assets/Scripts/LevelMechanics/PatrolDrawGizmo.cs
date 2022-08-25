using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolDrawGizmo : Singleton<PatrolDrawGizmo>
{
	public Transform pathHolder;
	void Awake()
	{
		pathHolder = transform;
		Vector3[] waypoints = new Vector3[pathHolder.childCount];
		for (int i = 0; i < waypoints.Length; i++)
		{
			waypoints[i] = pathHolder.GetChild(i).position;
			waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
		}

	}
	void OnDrawGizmos()
	{
		foreach (Transform waypoint in pathHolder)
		{
			Gizmos.DrawSphere(waypoint.position, .3f);
		}
	}
}
