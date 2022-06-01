using UnityEngine;
// simple follow waypoint script where the game object will follow the given waypoints and turn smoothly towards the next waypoint
public class Enemy : MonoBehaviour
{
    #region Public Fields
    public bool isDead;
    #endregion

    #region Private Fields
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float turningDistance = 3f;
    [SerializeField] private GameObject[] waypoints;
    private int currentWP = 0;

    #endregion

    #region MonoBehaviour Callbacks
    void Update()
    {
        OnEnemyDead();
        FollowWayPoints();
        
    }

    #endregion

    #region Private Methods
    private void OnEnemyDead()
    {
        //if the enemy is dead turn off the collider
        if (isDead)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            return;
        }
    }

    private void FollowWayPoints()
    {
        if (!isDead)
        {
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
            // the direction of the next goal
            Quaternion lookatWP = Quaternion.LookRotation(waypoints[currentWP].transform.position - transform.position);
            // turn the game object to the direction of the next goal position but smoothly with a given rotation speed;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookatWP, rotationSpeed * Time.deltaTime);
            // push the game object forward/z axis with a given speed
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        
    }

    #endregion
}
