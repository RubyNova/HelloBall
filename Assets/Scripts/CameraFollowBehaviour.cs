﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CameraFollowBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject _player;
    private Vector3 _offset;

    private void Start ()
    {
        _offset = transform.position - _player.transform.position;
    }

    private void LateUpdate ()
    {
        transform.position = _player.transform.position + _offset;
    }
}
