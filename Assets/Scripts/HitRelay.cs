using UnityEngine;

public class HitRelay : MonoBehaviour
{
    public void Hit()
    {
        transform.parent.gameObject.SendMessage("Hit");
    }
}
