﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBulletController : MonoBehaviour
{
    [SerializeField]
    Image sight;
    float rayLength = 1000;
    RaycastHit hitObject;
    int damageSorce = 25;
    LineRenderer lr;

    [SerializeField]
    GameObject bullet;

    PlayerController pc;
    float cnt = 0f;
    [SerializeField]
    float interval = 2f;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        pc = transform.root.GetComponent<PlayerController>();
    }

    void Update()
    {
        cnt += Time.deltaTime;
        if (Input.GetMouseButton(2) && interval < cnt) 
        {
            cnt = 0;
            Instantiate(bullet, transform.position, transform.rotation, transform);
        }

        if (pc.GetStopFlag())
        {
            return;
        }
        Ray ray = new Ray(transform.position + transform.rotation * new Vector3(0, 0, 0.01f), transform.forward);
        Debug.DrawRay(transform.position + transform.rotation * new Vector3(0, 0, 0.01f), transform.forward * rayLength, Color.yellow);
        if (Physics.Raycast(ray, out hitObject, rayLength))
        {
            if (hitObject.transform.gameObject.tag == "Enemy" || hitObject.transform.gameObject.tag == "BulletAtype" || hitObject.transform.gameObject.tag == "BulletBtype" || hitObject.transform.gameObject.tag == "Boss")
            {
                lr.enabled = true;
                sight.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                    if (hitObject.transform.gameObject.tag == "Enemy" || hitObject.transform.gameObject.tag == "Boss")
                    {
                        hitObject.collider.GetComponent<EnemyController>().ReceveDamage(damageSorce);
                    }
                    else if (hitObject.transform.gameObject.tag == "BulletAtype")
                    {
                        hitObject.collider.GetComponent<EnemyBulletAtype>().ReceveDamage(damageSorce);
                    }
                    else if (hitObject.transform.gameObject.tag == "BulletBtype")
                    {
                        hitObject.collider.GetComponent<EnemyBulletController>().ReceveDamage(damageSorce);
                    }
            }
            else
            {
                sight.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            }
        }
        else
        {
            lr.enabled = false;
            sight.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
    }
}