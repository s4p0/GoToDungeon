using System.Collections;
using System.Collections.Generic;
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

    public class AvoidAction : FindAction
    {
        public float vigilantDistance = 2f;
        public float speed = 100f;

        public float stopDistance = 1.2f;
        public bool keepOriginalPosition;
        private Vector2 originalPosition;


        public override float DistanceRadius => 4f;
        public override string TagToFind => "Player";


        // Start is called before the first frame update
        protected override void AfterStart()
        {
            Rigidbody2D.gravityScale = 0;
            Rigidbody2D.mass = Random.Range(40f, 60f);
            Rigidbody2D.drag = Random.Range(5f, 15f);
            Rigidbody2D.freezeRotation = true;

            originalPosition = Rigidbody2D.position;
        }

        public virtual void GoToPosition(Vector3 toPosition)
        {
            var toVector = (toPosition - transform.position).normalized;
            Rigidbody2D.AddRelativeForce(-1 * toVector * speed);
        }

        public override void InRangeAction(GameObject subject, float distance)
        {
            if (distance > stopDistance)
                GoToPosition(subject.transform.position);
        }

        public override void NotInRangeAction(GameObject subject, float distance)
        {
            if (keepOriginalPosition && Vector2.Distance(transform.position, originalPosition) > stopDistance)
                GoToPosition(originalPosition);

        }
    }

    public class ChaseAction : FindAction
    {
        public float vigilantDistance = 2f;
        public float speed = 100f;

        public float stopDistance = 1.2f;
        public bool keepOriginalPosition;
        private Vector2 originalPosition;


        public override float DistanceRadius => 4f;
        public override string TagToFind => "Player";


        // Start is called before the first frame update
        protected override void AfterStart()
        {
            Rigidbody2D.gravityScale = 0;
            Rigidbody2D.mass = Random.Range(40f, 60f);
            Rigidbody2D.drag = Random.Range(5f, 15f);
            Rigidbody2D.freezeRotation = true;

            originalPosition = Rigidbody2D.position;
        }

        public virtual void GoToPosition(Vector3 toPosition)
        {
            var toVector = (toPosition - transform.position).normalized;

            Rigidbody2D.AddRelativeForce(toVector * speed);
        }

        public override void InRangeAction(GameObject subject, float distance)
        {
            if (distance > stopDistance)
                GoToPosition(subject.transform.position);
        }

        public override void NotInRangeAction(GameObject subject, float distance)
        {
            if (keepOriginalPosition && Vector2.Distance(transform.position, originalPosition) > stopDistance)
                GoToPosition(originalPosition);

        }
    }
}
