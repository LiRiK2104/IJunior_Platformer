using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SignalizationZone : MonoBehaviour
{
    private Signalization _signalization;
    public bool IsDetecting { get; private set; }

    public void Initialize()
    {
        _signalization = GetComponentInParent<Signalization>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryUpdateSignalization(other, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TryUpdateSignalization(other, false);
    }

    private void TryUpdateSignalization(Collider2D other, bool isDetecting)
    {
        if (other.GetComponent<PlayerMove>())
        {
            IsDetecting = isDetecting;
            
            if (_signalization != null)
                _signalization.UpdateSignalization();
        }
    }
}
