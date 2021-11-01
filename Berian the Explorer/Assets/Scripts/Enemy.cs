using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// simple follow waypoint script where the game object will follow the given waypoints and turn smoothly towards the next waypoint
public class Enemy : MonoBehaviour
{
    public bool isDead;
    // a waypoints array
    public GameObject[] waypoints;
    // the current waypoint index
    public int currentWP = 0;
    // the moving speed
    public float speed = 10f;
    // the turning speed
    public float rotationSpeed = 10f;
    // the distance before the tank start turning to an other waypoint
    public float turningDistance = 3f;

    void Update()
    {
        if (isDead)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            return;
        }
            // the distance between the game object and the next waypoint
            // if we reach the waypoint increase the current waypoint
            if (Vector3.Distance(transform.position, waypoints[currentWP].transform.position) < turningDistance)
        {
            currentWP++;
        }
        // if we reach the last waypoint in the array set the next waypoint to the first one so it starts again from the begining
        if (currentWP >= waypoints.Length)
        {
            currentWP = 0;
        }
        // turn the game object to the goal direction in this way the game object will snap straight away to the direction of the goal position
        //transform.LookAt(waypoints[currentWP].transform);

        // the direction of the next goal
        Quaternion lookatWP = Quaternion.LookRotation(waypoints[currentWP].transform.position - transform.position);
        // turn the game object to the direction of the next goal position but smoothly with a given rotation speed;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookatWP, rotationSpeed * Time.deltaTime);
        // push the game object forward/z axis with a given speed
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
