using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Attack : MonoBehaviour
    {
        public GameObject handler;
        public Sprite other;
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
            var rendr = handler.GetComponent<SpriteRenderer>();
            rendr.sprite = other;
            var box = handler.GetComponent<BoxCollider2D>();

            var size = rendr.sprite.bounds.size;
            box.size = size;
            box.offset = new Vector2(0, size.y / 2);
            
        }
        private void Update()
        {
            if(Input.GetKeyDown("space"))
            {
                anim.SetTrigger("attacking");
            }
        }

        public void HasAttacked(object target)
        {
            var other = target as GameObject;
            if(other != null)
            {
                var health = other.GetComponent<Health>();
                if (health != null)
                    health.Damage(10);
            }
        }
    }
}
