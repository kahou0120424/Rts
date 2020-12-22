using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class worker : MonoBehaviour
{
    public enum Task
    {
        Collect,
        Return,
        Attack
    }

    public Task task = Task.Collect;
    public NavMeshAgent agent;

    [SerializeField]
    private int MaterialRange;

    [SerializeField]
    private int AttackRange;

    [SerializeField]
    int CollectWoodTime = 100;

    [SerializeField]
    int ReloadWoodTime = 50;

    [SerializeField]
    int AttackCoolDown = 30;

    public Transform target;
    public GameObject Home;
    int timer = 0;

    [SerializeField]
    private int health;

    [SerializeField]
    private int attackDamage;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
       if(task == Task.Collect)
        {
            if (target == null)
                return;     
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
            if (Vector3.Distance(this.transform.position, target.transform.position) < 1.5f)
            {
                agent.isStopped = true;
                CollectingWood();
            }           
        }

       else if(task == Task.Return)
        {
            agent.isStopped = false;
            agent.SetDestination(Home.transform.position);

            if (Vector3.Distance(this.transform.position, Home.transform.position) < 2.0f)
            {
                agent.isStopped = true;
                ReloadingWood();
            }
        }

       else if(task == Task.Attack)
       { 
            if(target!= null)
            {
                applyDamageToEnemy();
            }
       }

       if(health < 0)
        {
            Destroy(gameObject);
        }
        


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MaterialRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRange);

    }

    void UpdateTarget()
    {
        if(task == Task.Attack)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= AttackRange)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }
        else
        {
            GameObject[] woods = GameObject.FindGameObjectsWithTag("Wood");
            float shortestDistance = Mathf.Infinity;
            GameObject nearestWood = null;
            foreach (GameObject wood in woods)
            {
                float distanceToWood = Vector3.Distance(transform.position, wood.transform.position);
                if (distanceToWood < shortestDistance)
                {
                    shortestDistance = distanceToWood;
                    nearestWood = wood;
                }
            }

            if (nearestWood != null && shortestDistance <= MaterialRange)
            {
                target = nearestWood.transform;
            }
            else
            {
                target = null;
            }
        }
        

    }

    void CollectingWood()
    {
        
        if(timer <= CollectWoodTime)
        {
            timer += 1;
        }
        else
        {
            task = Task.Return;
            timer = 0;
        }
    }

    void ReloadingWood()
    {

        if (timer <= ReloadWoodTime)
        {
            timer += 1;
        }
        else
        {
            task = Task.Collect;
            timer = 0;
        }
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int newHealth)
    {
        health = newHealth;
    }

    public void takeDamage()
    {
        Debug.Log("got hit");
        task = Task.Attack;
    }

    void applyDamageToEnemy()
    {
        if(timer <= AttackCoolDown)
        {
            timer += 1;
        }
        else
        {
            Debug.Log("hit enemy");
            timer = 0;
            target.GetComponent<EnemyProperty>().setHealth(target.GetComponent<EnemyProperty>().getHealth() - attackDamage);
        }
        
    }
}
