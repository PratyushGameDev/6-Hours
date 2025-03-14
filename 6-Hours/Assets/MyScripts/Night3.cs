using UnityEngine.AI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Night3 : MonoBehaviour
{
    [SerializeField] public NavMeshAgent monsterAI;
    [SerializeField] GameObject timeline;
    [SerializeField] public GameObject prefabedMonster;
    [SerializeField] public GameObject carPortal;
    [SerializeField] public GameObject carPortalTimeline;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject clickSpaceToInteract;
    [SerializeField] GameObject doomSFXObject;
    [SerializeField] GameObject doomSFXOpeningObject;
    [SerializeField] GameObject doomSFXWinObject;
    [SerializeField] GameObject fadeOutObject;
    [SerializeField] GameObject fadeOutWinObject;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject instructions;
    [SerializeField] GameObject guidanceAudioEmpty;
    [SerializeField] AudioClip guidanceAudioClip;
    [SerializeField] public GameObject capsule1Button;
    [SerializeField] public GameObject capsule2Button;
    [SerializeField] public GameObject capsule3Button;
    [SerializeField] public GameObject cam;
    [SerializeField] public Transform player;
    public bool isLookingAtCapsule1 = false;
    public bool isLookingAtCapsule2 = false;
    public bool isLookingAtCapsule3 = false;
    public bool canZoomIn = true;
    bool inRangeCapsule1 = false;
    bool inRangeCapsule2 = false;
    bool inRangeCapsule3 = false;
    public bool shouldSkipIntro = false;
    [SerializeField] AudioClip doomSFX;
    [SerializeField] AudioClip loseSFX;
    [SerializeField] AudioClip winSFX;
    Vector3 posOfPlayerWhenCompleting3Capsules;
    Quaternion rotOfPlayerWhenCompleting3Capsules;
    public GameObject monsterAIMesRenderer;
    public GameObject monsterObjectMeshRenderer;

    Animator aN;
    Buttons bT;


    // Start is called before the first frame update
    void Start()
    {
        aN = GetComponent<Animator>();
        bT = FindObjectOfType<Buttons>();
        Debug.Log(Time.timeSinceLevelLoad);
        capsule1Button.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(4);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (winScreen.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Invoke(nameof(LoadNextScene), 5f);
        }
        if (instructions == null)
        {
            monsterAI.SetDestination(player.position);
            monsterAI.GetComponent<Animator>().Play("Walk", 0);
            if (guidanceAudioEmpty != null)
            {
                if (!guidanceAudioEmpty.GetComponent<AudioSource>().isPlaying)
                {
                    guidanceAudioEmpty.GetComponent<AudioSource>().PlayOneShot(guidanceAudioClip);
                }
            }     
            else
            {
                Debug.Log("guidance audio empty is null");

            }
            if (Time.timeSinceLevelLoad >= 24+11)
            {
                Debug.Log("Time to destroy guidance emtpy");
                Destroy(guidanceAudioEmpty);
            }
        }
        if (instructions != null)
        {
            if (instructions.activeInHierarchy)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }        
        if (bT.monsterSpeed == 1)
        {
            monsterAI.speed = 4;
        }
        if (bT.monsterSpeed == 2)
        {
            monsterAI.speed = 4.5f;
        }
        if (bT.monsterSpeed == 3)
        {
            monsterAI.speed = 5.5f;
        }
        if (Time.timeSinceLevelLoad >= 11 && shouldSkipIntro == false && instructions != null)
        {
            timeline.SetActive(false);
            instructions.SetActive(true);
        }
        if (timeline.active == false && Time.timeSinceLevelLoad <= 12 && shouldSkipIntro == false)
        {
            aN.enabled = false;
        }

        if (instructions != null)
        {
            if (instructions.activeInHierarchy == true)
            {
                if (!doomSFXOpeningObject.GetComponent<AudioSource>().isPlaying)
                {
                    doomSFXOpeningObject.GetComponent<AudioSource>().PlayOneShot(doomSFX);
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Destroy(instructions);
                    Time.timeScale = 1f;
                }
            }
        }
    }

    void TurnOnButton1()
    {
        Debug.Log("time to click button");
        if (capsule1Button.activeInHierarchy == false)
        {
            capsule1Button.SetActive(true);
        }
        //Cursor.lockState = CursorLockMode.None;
    }
    void TurnOnButton2()
    {
        Debug.Log("time to click button");
        if (capsule2Button.activeInHierarchy == false)
        {
            capsule2Button.SetActive(true);
        }
        //Cursor.lockState = CursorLockMode.None;
    }

    void TurnOnButton3()
    {
        Debug.Log("time to click button");
        if (capsule3Button.activeInHierarchy == false)
        {
            capsule3Button.SetActive(true);
        }
        //Cursor.lockState = CursorLockMode.None;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Capsule1Field")
        {
            //inRangeCapsule1 = true;
            //clickSpaceToInteract.SetActive(true);
            //Debug.Log("time to zoom into capsule1");
            aN.enabled = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            GetComponent<Rigidbody>().isKinematic = true;
            if (canZoomIn == true)
            {
                aN.Play("ZoomIntoCapsule1", 0);
            }
            canvas.GetComponent<Animator>().Play("EnableCapsule1Button", 0);
            isLookingAtCapsule1 = true;
            Invoke(nameof(TurnOnButton1), 1f);
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("time to click button through trigger method");
        }
        if (other.gameObject.tag == "Capsule2Field")
        {
            //inRangeCapsule2 = true;
            //clickSpaceToInteract.SetActive(true);
            aN.enabled = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            GetComponent<Rigidbody>().isKinematic = true;
            if (canZoomIn == true)
            {
                aN.Play("ZoomIntoCapsule2", 0);
            }
            canvas.GetComponent<Animator>().Play("EnableCapsule2Button", 0);
            isLookingAtCapsule2 = true;
            Invoke(nameof(TurnOnButton2), 1f);
            Cursor.lockState = CursorLockMode.None;
        }
        if (other.gameObject.tag == "Capsule3Field")
        {
            //inRangeCapsule3 = true;
            //clickSpaceToInteract.SetActive(true);
            aN.enabled = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            GetComponent<Rigidbody>().isKinematic = true;
            if (canZoomIn == true)
            {
                aN.Play("ZoomIntoCapsule3", 0);
            }
            canvas.GetComponent<Animator>().Play("EnableCapsule3Button", 0);
            isLookingAtCapsule3 = true;
            Invoke(nameof(TurnOnButton3), 1f);
            Cursor.lockState = CursorLockMode.None;
        }
        if (other.gameObject.tag == "Car")
        {
            Debug.Log("won");
            fadeOutWinObject.SetActive(true);
            GetComponent<Rigidbody>().isKinematic = true;
            doomSFXWinObject.GetComponent<AudioSource>().PlayOneShot(doomSFX);
            GetComponent<CapsuleCollider>().enabled = false;            
            Invoke(nameof(TurnOnWinScreen), 2f);
        }
    }
    void TurnOnWinScreen()
    {
        winScreen.SetActive(true);
        Destroy(fadeOutWinObject);
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(winSFX);
        }
        Cursor.lockState = CursorLockMode.None;
        aN.enabled = true;
        aN.Play("PlayerWinN3", 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Monster")
        {
            prefabedMonster.SetActive(true);
            doomSFXObject.GetComponent<AudioSource>().PlayOneShot(doomSFX);
            fadeOutObject.SetActive(true);
            monsterAI.gameObject.SetActive(false);
            Destroy(monsterAI);
            Debug.Log("your dead");
            Invoke(nameof(TurnOnLoseScreen), 1f);
        }
    }
    void TurnOnLoseScreen()
    {
        loseScreen.SetActive(true);
        Destroy(fadeOutObject);
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(loseSFX);
        }
        Cursor.lockState = CursorLockMode.None;
    }
}
