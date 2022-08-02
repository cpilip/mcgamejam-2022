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

    private SoundManager smgr;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        // sound manager for jump
        smgr = GameObject.FindWithTag("SFX").GetComponent<SoundManager>();
    }

    void OnEnable ()
    {
        SceneManager.sceneLoaded += ResetToStartPosition;
    }

    // Update is called once per frame
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
            // play jump sound
            smgr.Play("jump");
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