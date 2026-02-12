using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private string _paramSpeed = "speed";
    [SerializeField] private string _paramVerticalSpeed = "vSpeed";
    [SerializeField] private string _paramIsGrounded = "isGrounded";
    [SerializeField] private string _paramJump = "jump";
    [SerializeField] private string _paramIsHit = "isHit";
    [SerializeField] private string _paramIsDead = "isDead";

    private Animator _animator;
    private Rigidbody _rb;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    public void SetSpeed(float Speed)
    {
        _animator.SetFloat(_paramSpeed, Speed);
    }

    public void SetVerticalSpeed()
    {
        _animator.SetFloat(_paramVerticalSpeed, _rb.velocity.y);
    }
    
    public void OnIsGroundedChanged(bool isGrounded)
    {
        _animator.SetBool(_paramIsGrounded, isGrounded);
    }

    public void OnJump()
    {
        _animator.SetTrigger(_paramJump);
    }

    public void OnHit()
    {
        _animator.SetTrigger(_paramIsHit);
    }

    public void OnDeath(bool isDead)
    {
        _animator.SetBool(_paramIsDead, isDead);
    }
}