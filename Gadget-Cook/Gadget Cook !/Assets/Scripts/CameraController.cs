using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    #region  Singleton
    public static CameraController instance;

    CameraController() { instance = this; }
    #endregion
    
    #region Variables
    public Transform intialPos;
    public Transform newPos;
    public Transform iphonePos;
    public Transform spatulPos;
    public Transform oilPanPos;
    public Transform burnerDialPos;
    public Transform oilPanAndBucket;

    public Transform currentCamPos;
    #endregion
    
    private void Start()
    {
        StartCoroutine(ChangeFocusToIphone());
    }

    public void SetCurrentCameraPos(Transform t)
    {
        currentCamPos = t;
    }

    private IEnumerator ChangeFocusToIphone()
    {
        yield return new WaitForSeconds(3f);
        SetCurrentCameraPos(iphonePos);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, currentCamPos.position, Time.deltaTime * 6f);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentCamPos.rotation, Time.deltaTime * 6f);
    }
}
