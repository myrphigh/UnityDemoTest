using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

	//public static event System.Action OnGuardHasSpottedPlayer;
	public Slider alertStatusMeter;
	public float pSpeed = 5;
	public float mSpeed = 5;
	public float waitTime = .3f;
	public float turnSpeed = 90;
	public float timeToSpotPlayer = .5f;
	public float alertTime = .5f;

	public Light spotlight;
	public float viewDistance;
	public LayerMask viewMask;
	public GameObject PlayerIns;
	public float point = 3f;

	public Skill skP;

	float viewAngle;
	float playerVisibleTimer;
	private int enemyState = 0;
	// 0 = patrol, 1 = combat, 

	public Transform pathHolder;
	Transform player;
	Color originalSpotlightColour;

	IEnumerator patrol;
	IEnumerator turnWaypoints;
	IEnumerator turnPlayer;
	IEnumerator goToPlayer;
	private bool patrolStart;
	private bool turnWaypointsStart;
	private bool turnPlayerStart;
	private bool goToPlayerStart;
	private bool isBroken = false;

	private bool collidedPlayer = false;

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		viewAngle = spotlight.spotAngle;
		originalSpotlightColour = spotlight.color;

		Vector3[] waypoints = new Vector3[pathHolder.childCount];
		for (int i = 0; i < waypoints.Length; i++) {
			waypoints [i] = pathHolder.GetChild (i).position;
			waypoints [i] = new Vector3 (waypoints [i].x, transform.position.y, waypoints [i].z);
		}
		patrol = FollowPath(waypoints);
		StartCoroutine (patrol);
		patrolStart = true;
		
		Vector3 temp = new Vector3(player.position.x, transform.position.y, player.position.z);
		turnPlayer = TurnToFace(player.position);
		goToPlayer = GoToPosition(player);

	}

	void Update() {
		if(isBroken){
			enemyState = 3;
			spotlight.color = Color.green;
		}
		if(enemyState != 3){
			if(skP.showPlayerState() == 2)
			{
				enemyState = 2;
			} else {
				if(playerVisibleTimer >= alertTime)
				{
					enemyState = 1;
				} else {
					enemyState = 0;
				}			
			}
			Vector3 temp = new Vector3(player.position.x, transform.position.y, player.position.z);
			turnPlayer = TurnToFace(player.position);

			if (CanSeePlayer () && enemyState!= 2) {
				playerVisibleTimer += Time.deltaTime;
			} else {
				playerVisibleTimer -= Time.deltaTime;
			}
			playerVisibleTimer = Mathf.Clamp (playerVisibleTimer, 0, timeToSpotPlayer);
			spotlight.color = Color.Lerp (originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);
			alertStatusMeter.value = playerVisibleTimer;

		}
		if(transform.rotation.x != 0 && transform.rotation.z != 0 && !CanSeePlayer())
		{
			GetComponent<Rigidbody>().useGravity = true;
			StopAllCoroutines();
			isBroken = true;
		}

		// if (playerVisibleTimer >= timeToSpotPlayer) {
		// 	if (OnGuardHasSpottedPlayer != null) {
		// 		OnGuardHasSpottedPlayer ();
		// 	}
		// }
	}

	void FixedUpdate(){
		if(enemyState!= 3){
			if (enemyState == 1){
				//enter combat state
				if(patrolStart){
					StopCoroutine (patrol);
					patrolStart = false;
				}

				if(turnPlayerStart){	
					StopCoroutine(turnPlayer);
					turnPlayerStart = false;
				} else{
					StartCoroutine(turnPlayer);
					turnPlayerStart = true;
				}

				if(goToPlayerStart){
				} else{
					StartCoroutine(goToPlayer);
					goToPlayerStart = true;
				}

			}	else {
				//enter patrol state
				if(patrolStart){
					StopCoroutine(patrol);
					patrolStart = false;
				} else{
					StartCoroutine(patrol);
					patrolStart = true;
				}
				if(turnPlayerStart){
					StopCoroutine(turnPlayer);
					turnPlayerStart = false;
				}
				if(goToPlayerStart){
					StopCoroutine(goToPlayer);
					goToPlayerStart = false;
				}
			}
		}
	}

	bool CanSeePlayer() {
		if (Vector3.Distance(transform.position,player.position) < viewDistance) {
			Vector3 dirToPlayer = (player.position - transform.position).normalized;
			float angleBetweenGuardAndPlayer = Vector3.Angle (transform.forward, dirToPlayer);
			if (angleBetweenGuardAndPlayer < viewAngle / 2f) {
				if (!Physics.Linecast (transform.position, player.position, viewMask)) {
					return true;
				}
			}
		}
		return false;
	}

	IEnumerator FollowPath(Vector3[] waypoints) {
		transform.position = waypoints [0];

		int targetWaypointIndex = 1;
		Vector3 targetWaypoint = waypoints [targetWaypointIndex];
		transform.LookAt (targetWaypoint);

		while (true) {
			transform.position = Vector3.MoveTowards (transform.position, targetWaypoint, pSpeed * Time.deltaTime);
			if (transform.position == targetWaypoint) {
				targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
				targetWaypoint = waypoints [targetWaypointIndex];
				turnWaypoints = TurnToFace(targetWaypoint);
				yield return new WaitForSeconds (waitTime);
				yield return StartCoroutine (turnWaypoints);
			}
			yield return null;
		}
	}

	IEnumerator GoToPosition(Transform target)
	{
		//transform.LookAt(target.position);
		while(true){
			Vector3 temp1 = new Vector3(target.position.x, transform.position.y, target.position.z);
			transform.position = Vector3.MoveTowards(transform.position, temp1, mSpeed * Time.deltaTime);
			if(Vector3.Distance(transform.position, temp1) < 0.5f)
			{
				yield return new WaitForSeconds (waitTime);
				break;
			}
			yield return null;
		}
	}

	IEnumerator TurnToFace(Vector3 lookTarget) {
		Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
		float targetAngle = 90 - Mathf.Atan2 (dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

		while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f) {
			float angle = Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
			transform.eulerAngles = Vector3.up * angle;
			if(playerVisibleTimer >= 0.5f){
				break;
			}
			yield return null;
		}
	}

	void OnDrawGizmos() {
		Vector3 startPosition = pathHolder.GetChild (0).position;
		Vector3 previousPosition = startPosition;

		foreach (Transform waypoint in pathHolder) {
			Gizmos.DrawSphere (waypoint.position, .3f);
			Gizmos.DrawLine (previousPosition, waypoint.position);
			previousPosition = waypoint.position;
		}
		Gizmos.DrawLine (previousPosition, startPosition);

		Gizmos.color = Color.red;
		Gizmos.DrawRay (transform.position, transform.forward * viewDistance);
	}


	private void OnTriggerEnter(Collider collision)

    {
        if (collision.gameObject.tag == "Player")
        {

            collidedPlayer = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collidedPlayer = false;
        }
    }

    public float returnPoint()
    {
    	return point;
    }

}
