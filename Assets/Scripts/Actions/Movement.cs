using UnityEngine;
using UnityEngine.Events;

namespace Actions
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Animator anim;

        public float threshold = .1f;
        public string animationTriggerName = "run";
        public bool dontAnime = false;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            dontAnime = true;
            if (anim != null)
            {
                foreach (var param in anim.parameters)
                {
                    if (param.name == animationTriggerName)
                    {
                        dontAnime = false;
                        break;
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (!dontAnime)
                anim.SetBool(animationTriggerName, rb.velocity.magnitude >= threshold);

            if (rb.velocity.x > 0)
                transform.localScale = Vector3.one;
            else if (rb.velocity.x < 0)
                transform.localScale = Vector3.one + 2 * Vector3.left;
        }
    }
}
