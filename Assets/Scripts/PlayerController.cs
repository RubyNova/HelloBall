using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;
    [SerializeField] private int _amountToWin;
    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private TMP_Text _winText;
    [SerializeField] private Transform _deathLocation;
    [SerializeField] private GameObject _resetButton;
    private float _xInput = 0;
    private float _yInput = 0;
    private bool _shouldJump;
    private bool _isOnGround;
    private int _currentAmount;

    private void Update()
    {
        if (Mathf.Abs(_deathLocation.position.y - transform.position.y) < 0.5f)
        {
            _counterText.gameObject.SetActive(false);
            _winText.gameObject.SetActive(true);
            _winText.text = $"You Lose! Final Score: {_currentAmount}";
            gameObject.SetActive(false);
            _resetButton.SetActive(true);
        }
        
        _xInput = Input.GetAxis("Horizontal");
        _yInput = Input.GetAxis("Vertical");
        _shouldJump = Input.GetKeyDown(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        _rb.AddForce(new Vector3(_xInput, 0, _yInput) * _speed);

        if (!_shouldJump || !_isOnGround) return;
        
        _rb.AddForce(0, 5, 0, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Pickup")) return;

        _currentAmount++;

        if (_currentAmount >= _amountToWin)
        {
            _winText.gameObject.SetActive(true);
            _counterText.gameObject.SetActive(false);
            _resetButton.SetActive(true);
        }
        else
        {
            _counterText.text = $"Score: {_currentAmount}";
        }
        
        other.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) _isOnGround = true;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) _isOnGround = true;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) _isOnGround = false;
    }

    public void ResetScene() => SceneManager.LoadScene(0);
}
