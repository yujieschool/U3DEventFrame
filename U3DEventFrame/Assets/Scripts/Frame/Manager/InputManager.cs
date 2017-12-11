using UnityEngine;
using System.Collections;




namespace U3DEventFrame
{
    public delegate void TouchBegin(Vector2 input);

    public delegate void TouchMove(Vector2 input);


    public delegate void TouchEnd(Vector2 input);



    public delegate void MouseDown(Vector3 input);
    public delegate void MouseUp(Vector3 input);
    public delegate void MouseMove(Vector3 input);
    


    public class InputManager : MonoBehaviour
    {


        TouchBegin touchBegin;

        TouchMove touchMove;


        TouchEnd touchEnd;




        MouseDown mouseDown;

        MouseUp mouseUp;

        MouseMove mouseMove;

        public void RegistTouchBegin(TouchBegin begin)
        {



            touchBegin += begin;

        }
        public void UnRegistTouchBegin(TouchBegin begin)
        {


          
            touchBegin -= begin;

        }

        public void RegistTouchMove(TouchMove move)
        {

            touchMove += move;

        }
        public void UnRegistTouchMove(TouchMove move)
        {

            touchMove -= move;

        }

        public void RegistTouchEnd(TouchEnd end)
        {

            touchEnd += end;

        }

        public void UnRegistTouchEnd(TouchEnd end)
        {

            touchEnd -= end;

        }

        public void RegistMouseDown(MouseDown down)
        {
            mouseDown += down;
        }

        public void UnRegistMouseDown(MouseDown down)
        {
            mouseDown -= down;
        }



        public void RegistMouseUp(MouseUp up)
        {

            mouseUp += up;

        }

        public void UnRegistMouseUp(MouseUp up)
        {

            mouseUp -= up;

        }


        public void RegistMouseMove(MouseMove move)
        {

            mouseMove += move;

        }

        public void UnRegistMouseMove(MouseMove move)
        {

            mouseMove -= move;

        }

        public void TouchBegin(Vector2 begin)
        {

        }

        public void TouchMove(Vector2 move)
        {

        }

        public void TouchEnd(Vector2 end)
        {


        }

        public void MouseDown(Vector3 down)
        {



        }

        public void MouseUp(Vector3 up)
        {


        }

        public void MouseMove(Vector3 up)
        {


        }






        public static InputManager Instance;

        void Awake()
        {

            Instance = this;


			InputManager.Instance.RegistTouchBegin(new TouchBegin(TouchBegin));
			InputManager.Instance.RegistTouchMove(new TouchMove(TouchMove));
			
			InputManager.Instance.RegistTouchEnd(new TouchEnd(TouchEnd));
			
			
			InputManager.Instance.RegistMouseUp(new MouseUp(MouseUp));

            InputManager.Instance.RegistMouseMove(new MouseMove(MouseMove));
            InputManager.Instance.RegistMouseDown(new MouseDown(MouseDown));


        }
        // Use this for initialization
        void Start()
        {





        }

        // Update is called once per frame
        void Update()
        {



            if (Input.touchCount == 1)
            {

                if (Input.touches[0].phase == TouchPhase.Began)
                {

                    touchBegin(Input.touches[0].position);
                }
                else if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    touchMove(Input.touches[0].position);
                }
                else if (Input.touches[0].phase == TouchPhase.Ended)
                {

                    touchEnd(Input.touches[0].position);

                }

            }


            if (Input.GetMouseButtonDown(0))
            {

                mouseDown(Input.mousePosition);

            }

            if (Input.GetMouseButton(0))
            {

                mouseMove(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {

                mouseUp(Input.mousePosition);
            }



        }
    }

}



