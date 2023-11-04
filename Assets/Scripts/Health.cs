using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBarImage;
    public bool isVisible = false;
    public TextMeshPro floatingText;
    public int damage = 10;

    void Start()
    {
        currentHealth = maxHealth;
        healthBarImage.fillAmount = (float)currentHealth/maxHealth;
        healthBarImage.gameObject.SetActive(isVisible);
        floatingText.gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBarImage.fillAmount = (float)currentHealth/maxHealth;
        floatingText.text = "-" + damage + " pts!";
        floatingText.gameObject.SetActive(true);
        StartCoroutine(floatingTextRoutine());
    }

    IEnumerator floatingTextRoutine()
    {
        yield return new WaitForSeconds(2f);
        floatingText.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(damage);
        }
    }
}
