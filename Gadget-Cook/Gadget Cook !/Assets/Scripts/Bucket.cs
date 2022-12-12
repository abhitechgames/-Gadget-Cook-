using UnityEngine;

public class Bucket : MonoBehaviour
{
    #region Variables
    private bool once = true;
    private bool midPointReached = false;
    private bool shouldMove = false;

    [SerializeField] private Vector3 midPoint;

    [SerializeField] private Vector3 finalPoint;
    
    private iphone _iphone;
    #endregion
    
    private void Start()
    {
        _iphone = iphone.instance;
    }

    //Called when Gadget Triggered with Customer Bucket
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("iphone") && once && _iphone.Frying)
        {
            iphone.instance.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            other.transform.SetParent(transform);
            GameManager.instance.AddSliderValue(1f);
            StartCoroutine(_iphone.WinCamRot());
            EnableShouldMove();

            once = false;
        }
    }

    // Lerping Bucket's Position
    private void Update()
    {
        if (shouldMove)
        {
            if (midPointReached)
            {
                transform.position = Vector3.Lerp(transform.position, finalPoint, Time.deltaTime * 3f);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, midPoint, Time.deltaTime * 3f);

                if (Vector2.Distance(transform.position, midPoint) < 0.15f)
                {
                    midPointReached = true;
                }
            }
        }
    }

    //Enable Should Move Variable
    public void EnableShouldMove()
    {
        shouldMove = true;
    }

}
