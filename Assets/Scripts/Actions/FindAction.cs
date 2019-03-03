using UnityEngine;

namespace Actions
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class FindAction : MonoBehaviour
    {
        private Rigidbody2D rb;

        public abstract float DistanceRadius { get; }
        public abstract string TagToFind { get; }
        public Rigidbody2D Rigidbody2D => rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            AfterStart();
        }

        private void Update()
        {
            FindByTag();
        }

        protected virtual void AfterStart()
        {

        }

        public abstract void InRangeAction(GameObject subject, float distance);
        public abstract void NotInRangeAction(GameObject subject, float distance);

        protected virtual void FindByTag()
        {
            var subject = GameObject.FindGameObjectWithTag(TagToFind);
            if (subject == null)
                return;

            var distance = Vector2.Distance(subject.transform.position, transform.position);
            if (distance < DistanceRadius)
                InRangeAction(subject, distance);
            else
                NotInRangeAction(subject, distance);
        }
    }
}
