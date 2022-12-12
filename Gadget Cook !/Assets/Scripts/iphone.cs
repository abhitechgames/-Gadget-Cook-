using UnityEngine;
using System.Collections;
using MoreMountains.NiceVibrations;

public class iphone : MonoBehaviour
{
    #region  Variables
    [SerializeField] private ParticleSystem oilParticles;

    [SerializeField] private Material maidaMaterial;

    [SerializeField] private MeshRenderer[] maidaMesh;

    [SerializeField] private Color originalColor;
    [SerializeField] private Color orangeColor;

    private Animator humanAnimator;

    [SerializeField] private bool isBurnerDialNotAvailable = false;

    public bool Dip;

    [SerializeField] protected bool MaidaMix;

    private bool onceOil = true;
    private bool once = true;
    public bool inOil = false;

    private int temp = 0;

    public bool Frying;
    #endregion

    #region Singleton
    
    public static iphone instance;
    iphone() { instance = this; }

    #endregion

    private void Start()
    {
        maidaMaterial.color = originalColor;
        humanAnimator = GameObject.FindGameObjectWithTag("Human").GetComponent<Animator>();
    }

    private void Update()
    {
        if (Frying)
            maidaMaterial.color = Vector4.Lerp(maidaMaterial.color, orangeColor, Time.deltaTime * 1.5f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Maida") && temp == 0)
        {
            CameraController.instance.SetCurrentCameraPos(CameraController.instance.spatulPos);
            temp++;
        }
        else if (other.collider.CompareTag("Oil"))
        {
            if (!MaidaMix)
            {
                print("Lose");
                FindObjectOfType<PlayerController>().enabled = false;
                this.enabled = false;
                GameManager.instance.Fail_UI();
            }
            else
            {
                if (onceOil)
                {
                    inOil = true;
                    GameManager.instance.AddSliderValue(.5f);

                    CameraController.instance.SetCurrentCameraPos(CameraController.instance.burnerDialPos);
                    if (isBurnerDialNotAvailable)
                    {
                        Invoke("CameraController.instance.SetCurrentCameraPos(CameraController.instance.oilPanPos)", 1f);
                        StartCoroutine(FryingTime());
                    }
                    onceOil = false;
                }
            }
        }
        else if (other.collider.CompareTag("Bucket") && Frying)
        {
            if (once)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                transform.SetParent(other.collider.transform);
                other.collider.GetComponentInParent<Bucket>().EnableShouldMove();
                GameManager.instance.AddSliderValue(1f);
                StartCoroutine(WinCamRot());
                once = false;
            }
        }

        if (other.collider.CompareTag("Maida"))
        {
            Dip = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Oil"))
        {
            oilParticles.gameObject.SetActive(false);
            inOil = false;
            AudioManager.instance.FindSource("Frying").volume = 0f;
        }
        else if (other.collider.CompareTag("Maida"))
        {
            Dip = false;
        }
    }

    public IEnumerator DipMaterialChange()
    {
        yield return new WaitForSeconds(.5f);
        GameManager.instance.AddSliderValue(.25f);
        
        foreach (var m in maidaMesh)
            m.material = maidaMaterial;

        yield return new WaitForSeconds(3f);
        CameraController.instance.SetCurrentCameraPos(CameraController.instance.oilPanPos);

        MaidaMix = true;
    }

    //Winning Camera Rot
    public IEnumerator WinCamRot()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.instance.Play("CashWon");
        AudioManager.instance.Play("Yay");
        humanAnimator.SetTrigger("Victory");
        CameraController.instance.SetCurrentCameraPos(CameraController.instance.newPos);
        foreach (var m in maidaMesh)
            m.enabled = false;
        yield return new WaitForSeconds(1.75f);
        GameManager.instance.Win_UI();
        this.enabled = false;

        MMVibrationManager.Haptic(HapticTypes.Success);

        once = false;
    }

    // Pan Frying Time
    public IEnumerator FryingTime()
    {
        AudioManager.instance.Play("Frying");
        AudioManager.instance.FindSource("Frying").volume = 0.65f;
        oilParticles.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        Frying = true;
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.AddSliderValue(.75f);
        CameraController.instance.SetCurrentCameraPos(CameraController.instance.oilPanAndBucket);
    }
}
