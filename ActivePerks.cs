using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePerks : MonoBehaviour
{
    public Transform swordPos;
    public LayerMask whatIsEnemies;
    public float swordRange;
    public int swordDmg;

    private int trapRemainNum;
    public int trapDmg;
    public static bool TrapMode = false;

    public int laserDmg;
    private float laserDuration;
    public float laserStartDuration;
    private bool laserOn;

    public float swordCoolDown;
    private float swordCurCoolDown;
    public float trapCoolDown;
    private float trapCurCoolDown;
    public float laserCoolDown;
    private float laserCurCoolDown;

    public GameObject swordObject;
    public GameObject trapObject;
    public GameObject laserObject;

    void Start()
    {
        trapRemainNum = 3;
        laserOn = false;
    }

    void Update()
    {
        swordCurCoolDown -= Time.deltaTime;
        trapCurCoolDown -= Time.deltaTime;
        laserCurCoolDown -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && TrapMode)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
            Vector3 adjustZ = new Vector3(worldPoint.x, worldPoint.y, trapObject.transform.position.z);

            Instantiate(trapObject).transform.position = adjustZ;
            trapRemainNum--;            
        }

        if (trapRemainNum <= 0)
        {
            TrapMode = false;
            trapRemainNum = 3;
        }

        if (laserOn)
        {
            laserDuration -= Time.deltaTime;
            
            if (laserDuration <= 0)
            {
                laserOn = false;                
            }

            laserObject.SetActive(true);
        }
        else
        {
            laserObject.SetActive(false);
            laserDuration = laserStartDuration;
        }
    }

    public void SwordSwing()
    {
        if (swordCurCoolDown <= 0)
        {
            GameObject s = Instantiate(swordObject, swordPos.position, transform.rotation);
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(swordPos.position, swordRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyControl>().TakeDamage(swordDmg);
            }
            swordCurCoolDown = swordCoolDown;
        }        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(swordPos.position, swordRange);
    }

    public void Trap()
    {
        if (trapCurCoolDown <= 0)
        {
            TrapMode = true;

            trapCurCoolDown = trapCoolDown;
        }
    }

    public void Laser()
    {
        if (laserCurCoolDown <= 0)
        {
            laserOn = true;

            laserCurCoolDown = laserCoolDown;
        }
    }
}
