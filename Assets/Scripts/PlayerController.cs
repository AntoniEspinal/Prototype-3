﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForcue;
    public bool midAir = true;
    int delayTime = 1;
    private float extraDelay = 1;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    private Animator playerAnim;
    public ParticleSystem esplosionPartical;
    public ParticleSystem dirtPartical;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
         playerRb = GetComponent<Rigidbody>();
         Physics.gravity *= gravityModifier;
         playerAnim = GetComponent<Animator>();
         playerAudio = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForcue,ForceMode.Impulse);
            isOnGround = false;
            midAir = true;
            playerAnim.SetTrigger("Jump_trig");
            dirtPartical.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f); 
        }
        if (Input.GetKey(KeyCode.Z) && midAir)
        {
            playerRb.AddForce(Vector3.up * jumpForcue,ForceMode.Impulse);
            midAir = false;
            playerAnim.SetTrigger("Jump_trig");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtPartical.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            esplosionPartical.Play();
            dirtPartical.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f); 
        }
        

        
    }
}
