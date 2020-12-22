using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyProperty : MonoBehaviour
{
    [SerializeField]
    private int health;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int SearchBuildingRange;

    [SerializeField]
    private int SearchEnemyRange;

    [SerializeField]
    private int AttackDelay;

    public NavMeshAgent agent;

    Transform target;
    int timer;

    GameObject nearestEnemy = null;
    GameObject nearestBuilding = null;
    public enum State
    {
        attackWorker,
        attackBuilding
    }

    public State state;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.attackWorker)
        {
            if (target == null)
                return;
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
            if (Vector3.Distance(this.transform.position, target.transform.position) < 1.5f)
            {
                ApplyDamageToWorker();
                agent.isStopped = true;
            }
        }

        else if (state == State.attackBuilding)
        {
            if (target == null)
                return;
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
            if (Vector3.Distance(this.transform.position, target.transform.position) < 1.5f)
            {
                ApplyDamageToBuilding();
                agent.isStopped = true;
            }
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Worker");
        float shortestDistanceEnemy = Mathf.Infinity;
        nearestEnemy = null;
        foreach (GameObject Enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, Enemy.transform.position);
            if (distanceToEnemy < shortestDistanceEnemy)
            {
                shortestDistanceEnemy = distanceToEnemy;
                nearestEnemy = Enemy;
            }
        }

        if (nearestEnemy != null && shortestDistanceEnemy <= SearchEnemyRange)
        {
            target = nearestEnemy.transform;
            state = State.attackWorker;
            return;
        }
        else
        {
            target = null;
        }

        GameObject[] building = GameObject.FindGameObjectsWithTag("Building");
         float shortestDistance = Mathf.Infinity;
        nearestBuilding = null;
         foreach (GameObject Building in building)
         {
            float distanceToBuilding = Vector3.Distance(transform.position, Building.transform.position);
            if (distanceToBuilding < shortestDistance)
            {
               shortestDistance = distanceToBuilding;
               nearestBuilding = Building;
            }
         }

         if (nearestBuilding != null && shortestDistance <= SearchBuildingRange)
         {
            target = nearestBuilding.transform;
            state = State.attackBuilding;
        }
         else
         {
            target = null;
         }       
    }

    public void setHealth(int newHealth)
    {
        health = newHealth;
    }

    public int getHealth()
    {
        return health;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,SearchBuildingRange);


        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, SearchEnemyRange);

    }

    void ApplyDamageToWorker()
    {
        if (nearestEnemy != null)
        {
            if (timer < AttackDelay)
            {
                timer += 1;
            }
            else
            {
                timer = 0;
                nearestEnemy.GetComponent<worker>().setHealth(nearestEnemy.GetComponent<worker>().getHealth() - damage);
            }        
        
            
        }
            
    }

    void ApplyDamageToBuilding()
    {
        if (nearestBuilding != null)
        {
            if (timer < AttackDelay)
            {
                timer += 1;
            }
            else
            {
                nearestBuilding.GetComponent<BuildingProperty>().setHealth(nearestBuilding.GetComponent<BuildingProperty>().getHealth() - damage);
            }
        }  
    }
}
