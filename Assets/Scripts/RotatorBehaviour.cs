using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed = 50;
    // Update is called once per frame
    void Update() => transform.Rotate(0, 1 * _speed * Time.deltaTime, 0);
}
