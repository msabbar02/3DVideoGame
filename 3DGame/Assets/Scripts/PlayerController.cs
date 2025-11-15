using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterStats stats;
    public CharacterController controller;
    public float speed = 12f;
    new Transform camera;
    float gravity = 9.81f;
    float verticalVelocity = 10;
    public float jumpValue = 10;
    Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        camera = Camera.main.transform;
        stats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float sprint = isSprinting ? 1.7f : 1;
        // PARA EL ATTACKE
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            LevelManager.instance.PlaySound(LevelManager.instance.levelSounds[4], LevelManager.instance.player.position);
        }
        
        Vector3 move = new Vector3(h, 0, v);
        animator.SetFloat("Speed",Math.Clamp(move.magnitude, 0, 0.5f) + (isSprinting ? 0.5f : 0));

        if (controller.isGrounded)
        {
            if (Input.GetAxis("Jump") > 0)
            {
                verticalVelocity = jumpValue;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        if (move.magnitude >= 0.1f)
        {
            float angle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        move = transform.TransformDirection(move);
        move = new Vector3(move.x * speed * sprint, verticalVelocity, move.z * speed * sprint);
        controller.Move(move *Time.deltaTime);
    }

    // Este es tu script PlayerController.cs

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Healt"))
        {
            //Debug.Log("Health Pickup");
            stats.ChangeHealth(20);
            LevelManager.instance.PlaySound(LevelManager.instance.levelSounds[1], LevelManager.instance.player.position);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("LevelItem"))
        { 
            LevelManager.instance.PlaySound(LevelManager.instance.levelSounds[2], LevelManager.instance.player.position);
            LevelManager.instance.AddLevelItem();
            Destroy(other.gameObject);
        }
    }

    public void DoAttack()
    {
        transform.Find("Collider").GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(HideCollider());
    }

    IEnumerator HideCollider()
    {
        yield return new WaitForSeconds(0.3f);
        transform.Find("Collider").GetComponent<BoxCollider>().enabled = false;
    }
}