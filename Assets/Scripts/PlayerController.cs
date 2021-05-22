using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public float jumpForce;
    public float gravityModifier;
    private bool isOnGround = true;
    public bool gameOver = false;
    private float jumpBuffer = 0.3f;

    //start animation variables
    private float startTime;
    private float duration = 2.0f;
    private float minimum = -8.0f;
    private float maximum = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //define rigidbody
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        jumpBuffer += Time.deltaTime;
        //player jump
        if ((Input.GetKeyDown(KeyCode.Space) || jumpBuffer < 0.2f) && isOnGround && !gameOver) {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            jumpBuffer = 0.0f;
        }

        //entrance animation
        if ((Time.time - startTime) / duration <= 1) {
            float t = (Time.time - startTime) / duration;
            transform.position = new Vector3(Mathf.SmoothStep(minimum, maximum, t), transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            isOnGround = true;
            dirtParticle.Play();
        } else if (other.gameObject.CompareTag("Obstacle")) {
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            Debug.Log("GAEM OBER");
        }
    }
}
