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
            handler.GetComponent<SpriteRenderer>().sprite = other;
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
            Debug.Log("Attacked");
        }
    }
}
