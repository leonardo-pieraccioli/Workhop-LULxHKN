using System.Collections;
using UnityEngine;

public class SpawnerFrenetico : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn; // Prefab to spawn
    [SerializeField] [Range(0.1f, 2f)] float spawnInterval = 1f; // Time interval between spawns

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("SpawnerFrenetico started. Prefab to spawn: " + prefabToSpawn.name);
        if (prefabToSpawn != null)
        {
            // Set the prefab to spawn to be kinematic
            if (prefabToSpawn.GetComponent<Rigidbody>() != null)
            {
                prefabToSpawn.GetComponent<Rigidbody>().useGravity = true;
            }
            Debug.Log("Prefab to spawn gravity enabled: " + prefabToSpawn.GetComponent<Rigidbody>().useGravity);
        }
        else
        {
            Debug.LogError("Prefab to spawn is not assigned in the inspector.");
        }
    }

    private float timePassed = 10f; // Time passed since the last spawn
    void Update()
    {
        timePassed += Time.deltaTime; // Increment time passed
        if (timePassed >= spawnInterval) // Check if the time passed is greater than or equal to the spawn interval
        {
            timePassed = 0f; // Reset time passed
            StartCoroutine(SpawnPrefab()); // Start the coroutine to spawn the prefab
        }
    }


    IEnumerator SpawnPrefab()
    {
        Debug.Log("Spawning prefab: " + prefabToSpawn.name);
        // Wait for the specified spawn interval

        Debug.Log("Instantiating prefab: " + prefabToSpawn.name);
        // Instantiate the prefab at the current position and rotation of this GameObject
        GameObject tempInstance = Instantiate(prefabToSpawn, transform.position, transform.rotation);
        if (tempInstance.GetComponent<Rigidbody>() != null)
        {
            // Set the prefab to spawn to be kinematic
            tempInstance.GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
            tempInstance.AddComponent<Rigidbody>();
            tempInstance.GetComponent<Rigidbody>().useGravity = true;
        }
        tempInstance.GetComponent<Rigidbody>().AddForce(new Vector3(5*Random.Range(-1f, 1f), 5*Random.Range(0.5f, 1f), 5*Random.Range(-1f, 1f)), ForceMode.Impulse);
        // Destroy the spawned prefab after 5 seconds
        Destroy(tempInstance, 5f);
        yield return new WaitForSeconds(spawnInterval);
    }
}