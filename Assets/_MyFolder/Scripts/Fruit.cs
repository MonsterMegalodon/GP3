using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceParticalEffect;

    [SerializeField] float heightLimit = 50f;
    [SerializeField] float heightSpeedReduceRange = 20f;

    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceParticalEffect = GetComponentInChildren<ParticleSystem>();
    }
    private void Sliced(Vector3 direction, Vector3 position, float force)
    {
        whole.SetActive(false);
        sliced.SetActive(true);

        fruitCollider.enabled = false;
        juiceParticalEffect.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;        // rotate the fruit to match the cut
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);        //match fruit to the angle that its being cut at

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
            slice.velocity = new Vector3(slice.velocity.x, slice.velocity.y / 10, slice.velocity.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Sliced(blade.direction, blade.transform.position, blade.cutForce);
        }
    }

    private void Update()
    {
        float distanceToTop = heightLimit - transform.position.y;
        if(distanceToTop < heightSpeedReduceRange)
        {
            float reduceFactor = distanceToTop / heightSpeedReduceRange;
            fruitRigidbody.velocity = new Vector3(fruitRigidbody.velocity.x, fruitRigidbody.velocity.y * reduceFactor, fruitRigidbody.velocity.z);
        }
    }

}
