using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character
{
    public float vigilantDistance = 5f;
    public float speed = 100f;
    public GameObject DamageFloatingTextPrefab;
    public GameObject blood;
    //public ParticleSystem blood;
    public void FixedUpdate()
    {
        var heroes = FindObjectsOfType<Hero>();

        foreach (var item in heroes)
        {
            var distance = Vector2.Distance(item.transform.position, transform.position);
            if (distance < vigilantDistance)
            {
                Chase(item.transform.position);

                break;
            }
        }
    }
    public override void OnReceiveDamage(int amount, Vector2? contact = null)
    {
        health -= amount;
        CharacterBody.AddForce(new Vector2(transform.localScale.x * -1, 0) * speed * 1.25f, ForceMode2D.Impulse);

        var pos = new Vector3(contact.Value.x, contact.Value.y, -5) + new Vector3(1f, .8f);
        var blooded = Instantiate(blood, pos, Quaternion.identity);

        //blooded.Emit(System.Convert.ToInt32(amount));
        Destroy(blooded.gameObject, 2f);


        var floatingText = Instantiate(DamageFloatingTextPrefab, pos, Quaternion.identity);
        floatingText.GetComponent<TextMesh>().text = Mathf.Clamp(health, 0, health).ToString();
        CameraShake.Instance.RequestShake();

        if (health <= 0)
            Destroy(gameObject);

    }

    public void Chase(Vector3 toPosition)
    {
        var toVector = toPosition - transform.position;
        toVector.Normalize();

        CharacterBody.AddRelativeForce(toVector * speed);
    }
}
