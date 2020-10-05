using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class playerMove : MonoBehaviour
{
    public float moveSpeed = 10f, rotateSpeed = 60f;
    public float realSpeed;
    private int index;
    private string nombre;

    public Transform pistol;
    public Rigidbody bullet;
    public GameObject bulletspawn;
    public AudioClip bulletSound;
    private AudioSource audio;

    private float hInput, vInput;
    private Animator _animator;
    private Rigidbody _rb;

    public Rigidbody asteroide;
    public GameObject asteroideLista;

    public GameObject panelTask;
    
    private bool menuTask = false;
    private bool stop;
    public GameObject panelInformacion;
    public Text sateliteeNombretxt;
    public UnityEngine.UI.Image myImageComponent;

    public GameObject[] arraySatelites;
    public Text txtSatelites;
    public GameObject panelSeeSatelite;
    public Text txtPanelSeeSatelite;
    public Text txtListdosat;
    public Text btnTextNext;

    private bool intervalPhoto;


    public GameObject finalGame;
    public bool showInformation;




    //Satelites encontrados
    public List<string> satelitesContador = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        finalGame.gameObject.SetActive(false);
        intervalPhoto = false;  //True - show information  False - show photo
        _rb = GetComponent<Rigidbody>();
        showInformation = false;
        _animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        panelSeeSatelite.SetActive(false);
        stop = false;
        panelInformacion.gameObject.SetActive(false);
        arraySatelites = GameObject.FindGameObjectsWithTag("fotoSatelite");
        foreach(GameObject foto in arraySatelites)
        {
            foto.SetActive(false);
        }

        for (int i = 0; i < 1000; i++)
        {
            Vector3 asteroidePosition = new Vector3(Random.Range(-1000.0f, 1000.0f), 0, Random.Range(-1000.0f, 1000.0f));
            Rigidbody asteroideInstance;
            asteroideInstance = Instantiate(asteroide, asteroidePosition, this.gameObject.transform.rotation) as Rigidbody;
            asteroideInstance.transform.SetParent(asteroideLista.transform);
        }

        panelTask.gameObject.SetActive(false);




    }

    // Update is called once per frame
    void Update()
    {
        if(satelitesContador.Count < 6)
        {
            vInput = 0;
            if(!stop)
            {
                hInput = Input.GetAxis("Horizontal") * rotateSpeed;
                if (Input.GetKey(KeyCode.Space))
                {
                    vInput = 0;
                }
                else
                {
                    vInput = moveSpeed;
                }
                if (Input.GetButtonDown("Fire1"))
                {
                    Rigidbody bulletInstance;
                    bulletInstance = Instantiate(bullet, pistol.position, pistol.rotation) as Rigidbody;
                    bulletInstance.AddForce(pistol.forward * 3000f);
                    audio.clip = bulletSound;
                    audio.Play();
                }
            }
        

            GameObject[] killEmAll;
            killEmAll = GameObject.FindGameObjectsWithTag("bullet");
            for (int i = 0; i < killEmAll.Length; i++)
            {
                Destroy(killEmAll[i].gameObject,3f);
            }

            txtSatelites.text = "Satellites: "+satelitesContador.Count+"/10";
        }
        else if(satelitesContador.Count >= 6 && !showInformation)
        {
            txtSatelites.text = "Satellites: " + satelitesContador.Count + "/10";

            vInput = 0;
            rotateSpeed = 0;
            finalGame.gameObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {

        _rb.MovePosition(this.transform.position +
        this.transform.forward *
        vInput * Time.fixedDeltaTime);

            

        Vector3 rotation = Vector3.up * hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);
    }

     
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "satelite")
        {
            panelSeeSatelite.SetActive(true);
            txtPanelSeeSatelite.text = "Press E to see " + other.gameObject.name;
        }
        if (other.gameObject.tag == "satelite" && Input.GetKeyUp(KeyCode.E))
        {
            showInformation = true;
            panelSeeSatelite.SetActive(false);

            string name = other.gameObject.name;
            Sprite image;
            //foreach(GameObject foto in arraySatelites)
            for (int i = 0; i < arraySatelites.Length; i++)
            {
                if(name == arraySatelites[i].name)
                {
                    myImageComponent.sprite = arraySatelites[i].GetComponent<SpriteRenderer>().sprite;
                    index = i;
                    nombre = name;
                }
                if(!satelitesContador.Contains(name))
                {
                    satelitesContador.Add(name);
                    txtListdosat.text = txtListdosat.text + "\n" + name;
                }
            }
            sateliteeNombretxt.text = name;
            btnTextNext.text = "Photo";
            panelInformacion.gameObject.SetActive(true);
            stop = true;
           
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "satelite")
        {
            panelSeeSatelite.SetActive(false);
        }

    }

    public void nextInformation()
    {
        for (int i = 0; i < arraySatelites.Length; i++)
        {
            if (nombre == arraySatelites[i].name)
            {
                if(intervalPhoto)
                {
                    myImageComponent.sprite = arraySatelites[index].GetComponent<SpriteRenderer>().sprite;
                    btnTextNext.text = "Photo";
                    intervalPhoto = false;
                }
                else
                {
                    foreach (GameObject foto in arraySatelites)
                    {
                        if(nombre+"1" == foto.name)
                        {
                            myImageComponent.sprite = foto.GetComponent<SpriteRenderer>().sprite;
                        }
                    }
                    //    myImageComponent.sprite = arraySatelites[index + 1].GetComponent<SpriteRenderer>().sprite;
                    btnTextNext.text = "Information";
                    intervalPhoto = true;
                }
            }
        }
    }




    public void closeInformation()
    {
        panelInformacion.gameObject.SetActive(false);
        showInformation = false;
        stop = false;

    }

    public void showTasks()
    {
        if(menuTask)
        {
            menuTask = false;
            panelTask.gameObject.SetActive(false);
        }
        else
        {
            menuTask = true;
            panelTask.gameObject.SetActive(true);
        }
    }

    public void showCredits()
    {
        SceneManager.LoadScene(2);
    }
}
