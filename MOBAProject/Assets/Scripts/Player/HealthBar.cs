using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public Image healthBarEffect;
    public Text hpinfo;
    public Text playerName;
    public float health;
    private GameObject closest;
    public float maxHealth;
    private float healthSpeed = 0.003f;
    private void Awake()
    {
        maxHealth = FindClosesWithTag("Player").GetComponent<ManageCharacter>().maxHealth;

        health = maxHealth;
        hpinfo.text = maxHealth.ToString();
    }
    private void Start()
    {
        maxHealth = health;
    }
    private void Update()
    {
        healthBar.fillAmount = health / maxHealth;
        if (healthBarEffect.fillAmount > healthBar.fillAmount)
        {
            healthBarEffect.fillAmount -= healthSpeed;
        }
        else
        {
            healthBarEffect.fillAmount = healthBar.fillAmount;
        }

    }

    GameObject FindClosesWithTag(string tag)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(tag);
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach(GameObject go in players)
        {
            Vector3 diff = go.transform.position - position;
            float currentDistance = diff.sqrMagnitude;
            if(currentDistance < distance)
            {
                closest = go;
                distance = currentDistance;
            }
        }
        return closest;
    }
}
