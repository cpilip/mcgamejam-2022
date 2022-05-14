using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private float m_runSpeed = 30.0f;
    [SerializeField] private float m_horizontalMove = 0f;

    [SerializeField] private Animator anim;
    private CharacterController controller;
    private bool jumping = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnEnable ()
    {
        SceneManager.sceneLoaded += ResetToStartPosition;
    }

    void Update()
    {
        float isMoving = Input.GetAxisRaw("Horizontal");
        m_horizontalMove = isMoving * m_runSpeed;

        if (isMoving == -1 || isMoving == 1)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumping = true;
            anim.SetTrigger("Jump");
        }
    }

    void ResetToStartPosition(Scene scene, LoadSceneMode mode)
    {
        transform.position = Vector3.zero;
    }

    public void ResetToPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    void FixedUpdate()
    {
        controller.Move(m_horizontalMove * Time.deltaTime, jumping);
        jumping = false;
    }

}