using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour {
    public float Speed; 
    Rigidbody Rig;

    private void FixedUpdate() {
        Vector3 Position = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Rig.velocity = Position * Speed;
    }
    
    // Start is called before the first frame update
    void Start() {
        Rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        
    }
}
