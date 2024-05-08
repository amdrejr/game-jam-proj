using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMoviment : MonoBehaviour
{
    
    [SerializeField] private CharacterController characterPlayer;
    [SerializeField] private Animator animationPlayer;
    [SerializeField] private float characterSpeedy;
    private Vector3 _movementPlayer;

    void Update()
    {
        PlayerMoviment();
    }

    private void PlayerMoviment(){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _movementPlayer = (transform.right * vertical) + (transform.forward * horizontal);
        characterPlayer.Move(_movementPlayer.normalized * characterSpeedy * Time.deltaTime);
    }
}
