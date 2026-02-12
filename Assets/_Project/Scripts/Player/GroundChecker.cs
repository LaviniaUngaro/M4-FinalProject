using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float _groundDistance = 0.2f;
    [SerializeField] private LayerMask _levelLayer;

    private bool _isGrounded = true;


    public bool GetIsGrounded() => _isGrounded;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _groundDistance);
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(transform.position, _groundDistance, _levelLayer, QueryTriggerInteraction.Ignore);
    }
}