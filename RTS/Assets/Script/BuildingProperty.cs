using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProperty : MonoBehaviour
{
    [SerializeField]
    private int Health;

    [SerializeField]
    private int gold;

    [SerializeField]
    private int material;

    [SerializeField]
    private int towerRange;

    public Transform target;
    public Transform partToRotate;
    public GameObject workerPrefab;
    public GameObject SpawnLocation;

    public GameObject worker;
    public enum BuildingType
    {
        WorkerBuilding,
        Tower
    }

    public BuildingType buildingType;

    // Start is called before the first frame update
    void Start()
    {
        if (buildingType == BuildingType.WorkerBuilding)
        {
            worker = Object.Instantiate(workerPrefab, SpawnLocation.transform.position, Quaternion.identity);
            worker.GetComponent<worker>().Home = this.gameObject;
        }
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.Health <= 0)
            DestroysBuilding();

        if (buildingType == BuildingType.Tower)
        {
            Tower();
        }

    }

    public int getHealth()
    {
        return Health;
    }

    public void setHealth(int newHealth)
    {
        Health = newHealth;
    }

    private void DestroysBuilding()
    {
        if (worker != null)
        {
            Destroy(worker);
        }

        Destroy(gameObject);
    }

    public void Demolish()
    {
        DestroysBuilding();
    }

    void Tower()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, towerRange);

    }

    void UpdateTarget()
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

        if (nearestEnemy != null && shortestDistance <= towerRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }
}
