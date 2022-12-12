using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    private bool isTouchHold = false;
    private bool isIphone = false;

    public Rigidbody iphoneRb;

    [SerializeField] private LayerMask whatIsCollidable;

    [SerializeField] private Animator iphoneAnimator;

    [SerializeField] private Transform iphoneTransform;

    public Vector3 height = Vector3.up * 2.5f;

    private Camera cam;

    private RaycastHit raycastHit;

    [SerializeField] private GameObject flameParticles;
    [SerializeField] private GameObject oilBoilParticles;
    #endregion
    
    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // Gadget Picking
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out raycastHit, 100f, whatIsCollidable);

            if(raycastHit.collider == null)
                return;

            if (raycastHit.collider.tag == "iphone")
            {
                isIphone = true;
                iphoneRb.useGravity = false;
            }

            else if (raycastHit.collider.tag == "Spatula" && iphoneRb.GetComponent<iphone>().Dip)
            {
                raycastHit.collider.GetComponentInParent<Animator>().enabled = true;
                StartCoroutine(iphone.instance.DipMaterialChange());
            }

            else if (raycastHit.collider.tag == "Dial" && iphoneRb.GetComponent<iphone>().inOil)
            {
                raycastHit.collider.GetComponent<Animator>().enabled = true;
                Invoke("CameraController.instance.SetCurrentCameraPos(CameraController.instance.oilPanPos)", 1f);
                StartCoroutine(iphone.instance.FryingTime());
                flameParticles.SetActive(true);
                oilBoilParticles.SetActive(true);
            }
        }

        // Gadget Controlling
        if (isIphone)
        {
            iphoneRb.position = Vector3.Lerp(iphoneRb.position, new Vector3(raycastHit.point.x, height.y, raycastHit.point.z), Time.deltaTime * 3f);
        }

        // Gadget Dropped
        if (Input.GetMouseButtonUp(0))
        {
            isTouchHold = false;
            iphoneRb.useGravity = true;
            iphoneAnimator.SetTrigger("Dropped");
            isIphone = false;
        }
    }
}
