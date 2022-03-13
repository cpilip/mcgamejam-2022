using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private float m_runSpeed = 30.0f;
    [SerializeField] private float m_horizontalMove = 0f;

    private CharacterController controller;
    private bool jumping = false;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnEnable ()
    {
        SceneManager.sceneLoaded += ResetToStartPosition;
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontalMove = Input.GetAxisRaw("Horizontal") * m_runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jumping = true;
        }
    }

    void ResetToStartPosition(Scene scene, LoadSceneMode mode)
    {
        transform.position = Vector3.zero;
    }

    void FixedUpdate()
    {
        controller.Move(m_horizontalMove * Time.deltaTime, jumping);
        jumping = false;
    }

}