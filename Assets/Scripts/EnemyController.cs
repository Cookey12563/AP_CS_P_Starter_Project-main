using System;
using UnityEngine;
using Unity.Mathematics;

public class EnemeyController : MonoBehaviour
{
   [SerializeField]GameObject fireball_prefab;
   [SerializeField]Transform spawnpoint;
    bool inRange = false;
    float timer;
    float time_between_shots = 1.5f;
    public float speed = 0.01f;

    Transform target;



    private void Start()
    {
       timer = time_between_shots;
    }

    private void Update()
    {
        if(inRange)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject.transform;
            inRange = true;
        }
    }



    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            target = null;
        }
    }

    void Shoot()
    {
        print("shoot");
        GameObject fireball_copy = Instantiate(fireball_prefab, spawnpoint.position, quaternion.identity);
        Projectile projectile_script = fireball_copy.GetComponent<Projectile>();
        projectile_script.target = target;
    }
}