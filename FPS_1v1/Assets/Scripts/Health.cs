using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //Public/Serialized data
    [SerializeField] int maxHealth;
    [SerializeField] float respawnTime;
    public Slider healthSlider;

    //Helper data
    int currentHealth;
    bool bIsAlive = true;
    Vector3 respawnPoint;

    public bool BIsAlive { get { return bIsAlive; } }

    // Start is called before the first frame update
    void Start()
    {
        //Setup health and UI
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        respawnPoint = transform.position;
    }

    public void TakeDamage(int damageValue)
    {
        //Take damage
        currentHealth -= damageValue;

        //Check death
        if (currentHealth <= 0)
        {
            Die();
        }

        //Update slider
        healthSlider.value = currentHealth;
    }

    void Die()
    {
        //TO-DO: Implement
        StartCoroutine(Respawn());

        //Update slider
        healthSlider.value = currentHealth;
    }

    IEnumerator Respawn()
    {
        //Turn off rotation lock and controls
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        bIsAlive = false;

        yield return new WaitForSeconds(respawnTime);

        //Spawn at position
        transform.position = respawnPoint;

        //Restore rotation and controls
        transform.rotation = Quaternion.identity; ;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        bIsAlive = true;
    }
}
