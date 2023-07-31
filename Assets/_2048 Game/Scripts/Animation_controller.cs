using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_controller : MonoBehaviour
{
    private Animator _animator;
    bool isrunning = false;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        isrunning = Input.GetKey(KeyCode.Space);

        _animator.SetBool("run",isrunning);
    }
}
