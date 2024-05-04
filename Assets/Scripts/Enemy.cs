using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float speedMultiplier;
    private bool seenPlayer = false;
    Vector3 direction;

    Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GameObject.Find("spotlight").GetComponent<Light>();
        direction = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speedMultiplier * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, light.range))
        {
            print("hit");
            if (hit.collider.tag == "Player")
            {
                if (!seenPlayer)
                {
                    speedMultiplier *= 2f;
                    seenPlayer = true;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "Wall":
                transform.Rotate(Vector3.up, 180);
                break;

            case "Player":
                Destroy(collision.gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
    }
}
