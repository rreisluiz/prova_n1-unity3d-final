using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedMultiplier;
    public float jumpMultiplier;

    public int lifePoints;

    public float pillEffectTime;

    private float speedBackup;
    private bool pill = false;

    float actualTime;

    private bool isJumping = false;

    Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        speedBackup = speedMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time == actualTime + pillEffectTime)
        {
            speedMultiplier = speedBackup;
        }

        // Walk
        float horizontalInput = Input.GetAxis("Horizontal") * speedMultiplier * Time.deltaTime;
        transform.Translate(new Vector3(horizontalInput, 0, 0));

        // jump
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            if (!isJumping)
            {
                rb.AddForce(Vector3.up * jumpMultiplier, ForceMode.Impulse);
                isJumping = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.collider.tag)
        {
            case "Ground":
                isJumping = false;
                break;

            case "SpeedPill":
                speedMultiplier *= 1.5f;
                pill = true;
                Destroy(collision.gameObject);
                break;

            case "SlowPill":
                speedMultiplier *= 0.5f;
                pill = true;
                Destroy(collision.gameObject);
                break;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        print(collision.collider.tag);

        switch (collision.collider.tag)
        {
            case "Door":
                if (Input.GetKeyDown(KeyCode.Space))
                { 
                    collision.gameObject.transform.Translate(new Vector3(0, 0, 5));
                }
                break;
        }
    }    
}
