using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class AttackTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            SendMessageUpwards("HasAttacked", collision.gameObject);
        }
    }
}
