using UnityEngine;
using System.Collections;

public class RPG_Camera : MonoBehaviour {

    public static RPG_Camera instance;

    public Transform cameraPivot;
    public float distance = 5f;
    public float distanceMax = 30f;
    public float mouseSpeed = 8f;
    public float mouseScroll = 15f;
    public float mouseSmoothingFactor = 0.08f;
    public float camDistanceSpeed = 0.7f;
    public float camBottomDistance = 1f;
    public float firstPersonThreshold = 0.8f;
    public float characterFadeThreshold = 1.8f;

    private Vector3 desiredPosition;
    private float desiredDistance;
    private float lastDistance;
    private float mouseX = 0f;
    private float mouseXSmooth = 0f;
    private float mouseXVel;
    private float mouseY = 0f;
    private float mouseYSmooth = 0f;
    private float mouseYVel;
    private float mouseYMin = -89.5f;
    private float mouseYMax = 89.5f;
    private float distanceVel;
    private bool camBottom;
    private bool constraint;
    
    private static float halfFieldOfView;
    private static float planeAspect;
    private static float halfPlaneHeight;
    private static float halfPlaneWidth;

    

    void Awake() {
        instance = this;


        touchWidth = Screen.width / 100.0f;
    }


    void Start() {
        distance = Mathf.Clamp(distance, 0.05f, distanceMax);
        desiredDistance = distance;

        halfFieldOfView = (Camera.main.fieldOfView / 2) * Mathf.Deg2Rad;
        planeAspect = Camera.main.aspect;
        halfPlaneHeight = Camera.main.nearClipPlane * Mathf.Tan(halfFieldOfView);
        halfPlaneWidth = halfPlaneHeight * planeAspect;

        mouseX = 0f;
        mouseY = 15f;        
    }


    //public static void CameraSetup() {
    //    GameObject cameraUsed;
    //    GameObject cameraPivot;
    //    RPG_Camera cameraScript;

    //    if (Camera.main != null)
    //        cameraUsed = Camera.main.gameObject;
    //    else {
    //        cameraUsed = new GameObject("Main Camera");
    //        cameraUsed.AddComponent<Camera>();
    //        cameraUsed.tag = "MainCamera";
    //    }

    //    if (!cameraUsed.GetComponent<RPG_Camera>())
    //        cameraUsed.AddComponent<RPG_Camera>();
    //    cameraScript = cameraUsed.GetComponent<RPG_Camera>() as RPG_Camera;


    
    //    cameraPivot = GameObject.Find("cameraPivot") as GameObject;
    //    cameraScript.cameraPivot = cameraPivot.transform;
    //}
    
    
    void LateUpdate() {
        if (cameraPivot == null) {
            Debug.Log("Error: No cameraPivot found! Please read the manual for further instructions.");
            return;
        }

        GetInput();

        GetDesiredPosition();

        PositionUpdate();


	}


    float mouseXInput = 0;

    float mouseYInput = 0;

    float mouseScrollInput = 0;


    bool hasInput = false;


    public float touchWidth ;


    Vector2 touchPos;


    void MouseInput()
    {
        hasInput = false;

        if (Input.touchCount > 0)
        {

            hasInput = true;


            if (Input.touchCount == 1)
            {

                Touch touchFinger = Input.GetTouch(0);
                if (touchFinger.phase == TouchPhase.Began)
                {

                    touchPos = touchFinger.position;

                }
                if (touchFinger.phase == TouchPhase.Moved)
                {
                    Vector2 deltaPos = touchFinger.position - touchPos;


                    mouseXInput = Mathf.Clamp(touchFinger.deltaPosition.x / touchWidth, -1, 1);

                    mouseYInput = Mathf.Clamp(touchFinger.deltaPosition.y / touchWidth, -1, 1);

                }

                if (touchFinger.phase == TouchPhase.Ended)
                {

                }


            }
            else if(Input.touchCount ==2)
            {

                Touch touchFinger = Input.GetTouch(0);
                if (touchFinger.phase == TouchPhase.Began)
                {

                    touchPos = touchFinger.position;

                }
                     if (touchFinger.phase == TouchPhase.Moved)
                     {

                         Vector2 deltaPos = touchFinger.position - touchPos;


                        float    tmpDelta =   Mathf.Clamp(touchFinger.deltaPosition.y / touchWidth, -1, 1);


                        mouseScrollInput = Mathf.Lerp(mouseScrollInput, tmpDelta,Time.deltaTime);
 
                     }

            }


        }
        else
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                Cursor.visible = false;
                hasInput = true;
                mouseXInput = Input.GetAxis("Mouse X");


              //  Debug.Log("mouseXInput====" + mouseXInput);

                mouseYInput = Input.GetAxis("Mouse Y");

            }
            else
            {
                Cursor.visible = true;
            }


            mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");
           


        }

    }

    void GetInput() {

        if (distance > 0.1) { // distance > 0.05 would be too close, so 0.1 is fine
            Debug.DrawLine(transform.position, transform.position - Vector3.up * camBottomDistance, Color.green);
            camBottom = Physics.Linecast(transform.position, transform.position - Vector3.up * camBottomDistance);
        }

        bool constrainMouseY = camBottom && transform.position.y - cameraPivot.transform.position.y <= 0;


        MouseInput();



        Debug.Log("mouseScrollInput===" + mouseScrollInput);

        if (hasInput)
        {

            mouseX += mouseXInput * mouseSpeed;

            if (constrainMouseY)
            {
                if (mouseYInput < 0)
                    mouseY -= mouseYInput * mouseSpeed;
            }
            else
                mouseY -= mouseYInput * mouseSpeed;

        }




        


         
        mouseY = ClampAngle(mouseY, -89.5f, 89.5f);
        mouseXSmooth = Mathf.SmoothDamp(mouseXSmooth, mouseX, ref mouseXVel, mouseSmoothingFactor);
        mouseXSmooth = ClampAngle(mouseXSmooth, -89.5f, 89.5f);
       // Debug.Log("mouseXSmooth ===" + mouseXSmooth);

        mouseYSmooth = Mathf.SmoothDamp(mouseYSmooth, mouseY, ref mouseYVel, mouseSmoothingFactor);
        mouseYSmooth = ClampAngle(mouseYSmooth, -89.5f, 89.5f);
      //  Debug.Log("mouseYSmooth ===" + mouseYSmooth);

        if (constrainMouseY)
            mouseYMin = mouseY;
        else
            mouseYMin = -89.5f;
        
        mouseYSmooth = ClampAngle(mouseYSmooth, mouseYMin, mouseYMax);


        //if (hasInput)
         //   RPG_Controller.instance.transform.rotation = Quaternion.Euler(RPG_Controller.instance.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, RPG_Controller.instance.transform.eulerAngles.z);

        desiredDistance = desiredDistance - mouseScrollInput * mouseScroll;

        if (desiredDistance > distanceMax)
            desiredDistance = distanceMax;

        if (desiredDistance < 0.05)
            desiredDistance = 0.05f;
    }


    void GetDesiredPosition() {
        distance = desiredDistance;

        desiredPosition = GetCameraPosition(mouseYSmooth, mouseXSmooth, distance);

        float closestDistance;
        constraint = false;

        closestDistance = CheckCameraClipPlane(cameraPivot.position, desiredPosition);

        if (closestDistance != -1) {
            distance = closestDistance;
            desiredPosition = GetCameraPosition(mouseYSmooth, mouseXSmooth, distance);

            constraint = true;
        }


        distance -= Camera.main.nearClipPlane;

        if (lastDistance < distance || !constraint)
            distance = Mathf.SmoothDamp(lastDistance, distance, ref distanceVel, camDistanceSpeed);
        
        if (distance < 0.05)
            distance = 0.05f;

        lastDistance = distance;

        desiredPosition = GetCameraPosition(mouseYSmooth, mouseXSmooth, distance); // if the camera view was blocked, then this is the new "forced" position
    }


    void PositionUpdate() {
        transform.position = desiredPosition;

        if (distance > 0.05)
            transform.LookAt(cameraPivot);
    }





    Vector3 GetCameraPosition(float xAxis, float yAxis, float distance) {
        Vector3 offset = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(xAxis, yAxis, 0);
        return cameraPivot.position + rotation * offset;
    }


    float CheckCameraClipPlane(Vector3 from, Vector3 to) {
        var closestDistance = -1f;
                  
        RaycastHit hitInfo;

        ClipPlaneVertexes clipPlane = GetClipPlaneAt(to);

        Debug.DrawLine(clipPlane.UpperLeft, clipPlane.UpperRight);
        Debug.DrawLine(clipPlane.UpperRight, clipPlane.LowerRight);
        Debug.DrawLine(clipPlane.LowerRight, clipPlane.LowerLeft);
        Debug.DrawLine(clipPlane.LowerLeft, clipPlane.UpperLeft);
     
        Debug.DrawLine(from, to, Color.red);
        Debug.DrawLine(from - transform.right * halfPlaneWidth + transform.up * halfPlaneHeight, clipPlane.UpperLeft, Color.cyan);
        Debug.DrawLine(from + transform.right * halfPlaneWidth + transform.up * halfPlaneHeight, clipPlane.UpperRight, Color.cyan);
        Debug.DrawLine(from - transform.right * halfPlaneWidth - transform.up * halfPlaneHeight, clipPlane.LowerLeft, Color.cyan);
        Debug.DrawLine(from + transform.right * halfPlaneWidth - transform.up * halfPlaneHeight, clipPlane.LowerRight, Color.cyan);


        if (Physics.Linecast(from, to, out hitInfo) && hitInfo.collider.tag != "Player")
            closestDistance = hitInfo.distance - Camera.main.nearClipPlane;
        
        if (Physics.Linecast(from - transform.right * halfPlaneWidth + transform.up * halfPlaneHeight, clipPlane.UpperLeft, out hitInfo) && hitInfo.collider.tag != "Player")
            if (hitInfo.distance < closestDistance || closestDistance == -1)
                closestDistance = Vector3.Distance(hitInfo.point + transform.right * halfPlaneWidth - transform.up * halfPlaneHeight, from);

        if (Physics.Linecast(from + transform.right * halfPlaneWidth + transform.up * halfPlaneHeight, clipPlane.UpperRight, out hitInfo) && hitInfo.collider.tag != "Player")
            if (hitInfo.distance < closestDistance || closestDistance == -1)
                closestDistance = Vector3.Distance(hitInfo.point - transform.right * halfPlaneWidth - transform.up * halfPlaneHeight, from);
        
        if (Physics.Linecast(from - transform.right * halfPlaneWidth - transform.up * halfPlaneHeight, clipPlane.LowerLeft, out hitInfo) && hitInfo.collider.tag != "Player")
            if (hitInfo.distance < closestDistance || closestDistance == -1)
                closestDistance = Vector3.Distance(hitInfo.point + transform.right * halfPlaneWidth + transform.up * halfPlaneHeight, from);
        
        if (Physics.Linecast(from + transform.right * halfPlaneWidth - transform.up * halfPlaneHeight, clipPlane.LowerRight, out hitInfo) && hitInfo.collider.tag != "Player")
            if (hitInfo.distance < closestDistance || closestDistance == -1)
                closestDistance = Vector3.Distance(hitInfo.point - transform.right * halfPlaneWidth + transform.up * halfPlaneHeight, from);


        return closestDistance;
    }


    float ClampAngle(float angle, float min, float max) {
        while (angle < -360 || angle > 360) {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;
        }

        

        return Mathf.Clamp(angle, min, max);
    }


    public struct ClipPlaneVertexes {
        public Vector3 UpperLeft;
        public Vector3 UpperRight;
        public Vector3 LowerLeft;
        public Vector3 LowerRight;
    }


    public static ClipPlaneVertexes GetClipPlaneAt(Vector3 pos) {
        var clipPlane = new ClipPlaneVertexes();

        
        if (Camera.main == null)
            return clipPlane;

        Transform transform = Camera.main.transform;
        float offset = Camera.main.nearClipPlane;

        clipPlane.UpperLeft = pos - transform.right * halfPlaneWidth;
        clipPlane.UpperLeft += transform.up * halfPlaneHeight;
        clipPlane.UpperLeft += transform.forward * offset;

        clipPlane.UpperRight = pos + transform.right * halfPlaneWidth;
        clipPlane.UpperRight += transform.up * halfPlaneHeight;
        clipPlane.UpperRight += transform.forward * offset;

        clipPlane.LowerLeft = pos - transform.right * halfPlaneWidth;
        clipPlane.LowerLeft -= transform.up * halfPlaneHeight;
        clipPlane.LowerLeft += transform.forward * offset;

        clipPlane.LowerRight = pos + transform.right * halfPlaneWidth;
        clipPlane.LowerRight -= transform.up * halfPlaneHeight;
        clipPlane.LowerRight += transform.forward * offset;

        
        return clipPlane;
    }


    public void RotateWithCharacter() {

        {
            float rotation = mouseXInput;
            mouseX += rotation;
        }
  
    }
}
