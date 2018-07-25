using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    PlayerController player;
    [SerializeField] float sightRange = 5;
    [SerializeField] float minAttackRange = 2;
    [SerializeField] float maxAttackRange = 5;
    [SerializeField] float turnSpeed = 2;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float attackSpeed = 5;
    [SerializeField] GameObject projectile;
    float sqrSight;
    float attackTimer;

    Rigidbody2D rigidbody;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rigidbody = GetComponent<Rigidbody2D>();
        sqrSight = sightRange * sightRange;
	}
	
	// Update is called once per frame
	void Update () {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        //Vector3 angles = transform.eulerAngles;
        //angles.z = Vector3.Angle(Vector3.zero, displacement);
        //transform.eulerAngles = angles;
        float sqrMag = (transform.position - player.transform.position).sqrMagnitude;
        if (sqrMag < sqrSight)
        {
            Vector3 displacement = transform.position - player.transform.position;
            float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle + 90, Vector3.forward), turnSpeed*Time.deltaTime);

            if(attackTimer <= 0 && sqrMag < maxAttackRange * maxAttackRange)
            {
                Instantiate(projectile, transform.position + transform.up, transform.rotation);
                attackTimer = attackSpeed;
            }
            if(sqrMag > (minAttackRange * minAttackRange))
            {
                rigidbody.velocity = transform.up;
            }
        }
	}
}
