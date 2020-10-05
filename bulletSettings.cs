using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bulletSettings : MonoBehaviour
{
    // Start is called before the first frame update
    private CapsuleCollider _col;
    private AudioSource audio;
    public AudioClip destroyClip;

    public Rigidbody explosion;

    private int contador;



    void Start()
    {
        _col = GetComponent<CapsuleCollider>();
        audio = GetComponent<AudioSource>();
        contador = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "asteroide")
        {
            audio.clip = destroyClip;
            audio.Play();
            Rigidbody explosionInstance;
            explosionInstance = Instantiate(explosion, this.gameObject.transform.position, this.gameObject.transform.rotation) as Rigidbody;

            GameObject[] killEmAll;
            Destroy(other.gameObject);
            killEmAll = GameObject.FindGameObjectsWithTag("explosion");
            for (int i = 0; i < killEmAll.Length; i++)
            {
                Destroy(killEmAll[i].gameObject, 3f);
            }
        }
    }
}
