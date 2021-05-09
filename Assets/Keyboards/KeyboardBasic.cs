using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.ExpTools;
using UnityEngine.UI;
using Assets.Keyboards;
using System.Timers;

using System.IO;
//Edit -> Project setting -> Player -> other setting -> ".Net 2.0" 으로 설정 후 reimport 
using System.IO.Ports;
using System.Net.Sockets;
using System.Media;
using System.Threading;


namespace Assets.Keyboards
{
    public class KeyboardBasic : MonoBehaviour
    {
        Vector2 tempGpos;
        public float gxScale = 2, gyScale = 2, gxOffset = 0, gyOffset = 0;
        public bool isCalibUse = false;
        const int calibPoint = 9;
        Matrix4x4 T1, T2, T3, T4;
        Vector2[] gazeRef;
        Vector2[] markerPos;

        //Marker
        public GameObject[] markers;
        public string[] markerLayout;
        public float markerWidth;
        public float markerHeight;
        public float markerGap;
        //Marker


        public AudioSource audio;
        public AudioClip click;

        public int startTrial = 0, startBlock = 0;
        public int participant = 0;

        public AverageFilter filterX, filterY;
        public FoveInterfaceBase foveInterface;

        private Collider my_collider;
        public const int SWIPE_BOTTOM_LEFT = 1;
        public const int SWIPE_LEFT = 2;
        public const int SWIPE_TOP_LEFT = 3;
        public const int SWIPE_TOP = 4;
        public const int SWIPE_BOTTOM = 8;
        public const int SWIPE_BOTTOM_RIGHT = 7;
        public const int SWIPE_RIGHT = 6;
        public const int SWIPE_TOP_RIGHT = 5;
        public const int GESTURE_CIRCLE = 9;

        public const int TOUCH_UP = 10;
        public const int TOUCH_UP_GESTURE = 13;
        public const int TOUCH_MOVE = 11;
        public const int TOUCH_DOWN = 12;

        float interRecordingInterval = 0.05f;
        float memorizeInterval = 15;
        float interTaskInterval = 5;//30
        float interBlockInterval = 10;//60
        bool isEyeClosed = false;

        public GazeTouchEvent gtEvent;
        public bool enableTyping;
        public bool isWindowActivated = false;
        public int activatedWindow = -1;
        public bool isRecording = false;

        public bool isKeyDown = false;
        public bool gazeMovable = true;

        protected Text exampleBox, inputBox, resultBox;
        protected string inputed = "";
        public ExperimentManager experimentManager;
        public string whichKeyboard = "";
        GameObject DC;
        public bool malInputCheck = false;

        GameObject[] eyeCursors;
        MeshFilter[] eyeCursorMeshes;
        public MeshFilter integratedEyeCursor;
        public Vector3[] eyeCursorsLocalPositionOnKeyboard;
        public Vector3 integratedEyeCursorPosition;
        public float[] integratedEyeCursor_yPoses;

        public int wtx, wty; //watch touch x,y
        private SerialPort watchConnector;
        string watchPacket;
        public bool isDynamicCascading = false;

        public float gRatioX, gRatioY;
        public int adjusterX = 11;

        // Use this for initialization
        void Start()
        {
        }

        protected void initExperiment()
        {
            tempGpos = new Vector2();
            gRatioX = 1;
            gRatioY = 1;
            if (isDynamicCascading)
            {
                DC = GameObject.Find("Eye_only");

            }
            filterX = new AverageFilter();
            filterY = new AverageFilter();
            audio = gameObject.GetComponentInChildren<AudioSource>();
            eyeCursorsLocalPositionOnKeyboard = new Vector3[2];
            eyeCursors = new GameObject[2];
            eyeCursors= GameObject.FindGameObjectsWithTag("eyecursor");
            eyeCursorMeshes = new MeshFilter[2];
            eyeCursorMeshes[0] = eyeCursors[0].GetComponent<MeshFilter>();
            eyeCursorMeshes[1] = eyeCursors[1].GetComponent<MeshFilter>();
            integratedEyeCursor = gameObject.GetComponentsInChildren<MeshFilter>()[1];
            integratedEyeCursorPosition = integratedEyeCursor.transform.localPosition;

            my_collider = GetComponent<Collider>();
            // 파일 열려있으면 죽음
            experimentManager = new ExperimentManager(participant, startTrial, startBlock, ExperimentManager.maxBlockDefault, ExperimentManager.maxTrialDefault, whichKeyboard);
            Text[] temp = gameObject.GetComponentsInChildren<Text>();
            foreach (Text t in temp)
            {
                if (t.tag.CompareTo("example") == 0)
                {
                    exampleBox = t;
                }
                else if (t.tag.CompareTo("inputed") == 0)
                {
                    inputBox = t;
                }
                else if (t.tag.CompareTo("resultView") == 0)
                {
                    resultBox = t;
                }
            }
            exampleBox.text = experimentManager.getPhrase();
            exampleBox.color = Color.green;
            inputBox.text = "_";
            resultBox.text = "-";
            enableTyping = true;
            gazeMovable = true;

            hideEyeCursors();

            StartCoroutine("recordGazeCursor");

        }

        public void hideEyeCursors()
        {

            eyeCursors[0].GetComponentInChildren<MeshFilter>().GetComponent<MeshRenderer>().material.color = Color.clear;
            eyeCursors[1].GetComponentInChildren<MeshFilter>().GetComponent<MeshRenderer>().material.color = Color.clear;
        }

        public void keyActivatedRecording(char k)
        {
            gtEvent.eventTime = TimeUtils.currentTimeMillis();
            gtEvent.eventLabel = 'w';
            gtEvent.selected = k;
            experimentManager.appendGazeTouchEvent(gtEvent);
            gtEvent.selected = (char)0;
        }
        public void keyDeactivatedRecording(char k)
        {
            gtEvent.eventTime = TimeUtils.currentTimeMillis();
            gtEvent.eventLabel = 'w';
            gtEvent.selected = (char)(k-32); //upper 
            experimentManager.appendGazeTouchEvent(gtEvent);
            gtEvent.selected = (char)0;
        }
        public void keyEntered(char k)
        {
            if (enableTyping && !malInputCheck)
            {
                malInputCheck = true;
                StartCoroutine("malInputChecker");
                gtEvent.eventTime = TimeUtils.currentTimeMillis();
                gtEvent.eventLabel = '?';

                if (k >= Key.A && k <= Key.Z)
                {
                    gtEvent.eventLabel = 'k';
                    audio.Play();

                    experimentManager.updateInput(k, inputed);
                    gtEvent.selected = k;

                    inputed += k;
                    inputBox.text = inputed + "_";
                    experimentManager.appendGazeTouchEvent(gtEvent);
                    gtEvent.selected = (char)0;
                    gazeMovable = true;
                }
                else
                {
                    switch (k)
                    {
                        case Key.backspace:
                            gtEvent.eventLabel = 'k';
                            audio.Play();
                            experimentManager.updateInput(k, inputed);
                            gtEvent.selected = k;
                            int inputNum = inputed.Length;
                            if (inputNum > 0)
                            {
                                inputed = inputed.Substring(0, inputNum - 1);
                            }
                            inputBox.text = inputed + "_";
                            experimentManager.appendGazeTouchEvent(gtEvent);
                            gtEvent.selected = (char)0;
                            gazeMovable = true;
                            break;
                        case Key.space:
                            gtEvent.eventLabel = 'k';
                            audio.Play();
                            experimentManager.updateInput(k, inputed);
                            gtEvent.selected = k;
                            inputed += " ";
                            inputBox.text = inputed + "_";
                            experimentManager.appendGazeTouchEvent(gtEvent);
                            gtEvent.selected = (char)0;
                            gazeMovable = true;
                            break;
                        case Key.Enter:

                            enableTyping = false;
                            gazeMovable = false;
                            gtEvent.eventLabel = 'x';
                            gtEvent.selected = '~';
                            doneTask();
                            experimentManager.appendGazeTouchEvent(gtEvent);
                            gtEvent.selected = (char)0;
                            exampleBox.text = "(RECORDING RESULTS...)";
                            audio.Play();
                            exampleBox.color = Color.red;
                            gazeMovable = true;


                            StartCoroutine("delayForRecording");
                            break;
                        case Key.Plus:

                            gtEvent.eventLabel = 'd';
                            gtEvent.selected = k;
                            gtEvent.state = getDwellTime();
                            experimentManager.appendGazeTouchEvent(gtEvent);
                            gtEvent.selected = (char)0;
                            audio.Play();
                            gazeMovable = true;


                            Dynamic_cascade.baseDwell_int = adjustDwell(true);
                            if (Dynamic_cascade.baseDwell_int >= Dynamic_cascade.maxDwell)
                            {
                                Dynamic_cascade.baseDwell_int = Dynamic_cascade.maxDwell;
                            }
                            DC.GetComponent<Dynamic_cascade>().updateDwell();
                            break;
                        case Key.Minus:

                            gtEvent.eventLabel = 'd';
                            gtEvent.selected = k;
                            gtEvent.state = getDwellTime();
                            experimentManager.appendGazeTouchEvent(gtEvent);
                            gtEvent.selected = (char)0;
                            audio.Play();
                            gazeMovable = true;

                            Dynamic_cascade.baseDwell_int = adjustDwell(false);
                            if (Dynamic_cascade.baseDwell_int <= Dynamic_cascade.minDwell)
                            {
                                Dynamic_cascade.baseDwell_int = Dynamic_cascade.minDwell;
                            }
                            DC.GetComponent<Dynamic_cascade>().updateDwell();
                            break;
                        case Key.Start:
                            audio.Play();
                            gtEvent.eventLabel = 's';
                            gtEvent.selected = k;
                            experimentManager.appendGazeTouchEvent(gtEvent);
                            gtEvent.selected = (char)0;
                            DC.GetComponent<Dynamic_cascade>().adjustDwellPhaseDone();
                            gazeMovable = true;

                            StartCoroutine("delayInterTask");
                            break;
                        default:
                            gtEvent.eventLabel = 'w';
                            switch (k)
                            {
                                case Key.D1:
                                case Key.D2:
                                case Key.D3:
                                case Key.D4:
                                case Key.D5:
                                case Key.D6:
                                case Key.D7:
                                case Key.D8:
                                case Key.D9:
                                    gtEvent.selected = k;
                                    activatedWindow = gtEvent.selected - '1';
                                    isWindowActivated = true;
                                    break;
                                case Key.F1:
                                case Key.F2:
                                case Key.F3:
                                case Key.F4:
                                case Key.F5:
                                case Key.F6:
                                case Key.F7:
                                case Key.F8:
                                case Key.F9:
                                    gtEvent.selected = k;
                                    isWindowActivated = false;
                                    activatedWindow = -1;
                                    break;
                                default:
                                    gtEvent.eventLabel = '?';
                                    break;
                            }
                            experimentManager.appendGazeTouchEvent(gtEvent);
                            gtEvent.selected = (char)0;
                            gazeMovable = true;
                            break;
                    }

                }
            }
            //setWordOntheKeyboard(); //for GAT
        }

        public IEnumerator malInputChecker()
        {
            yield return new WaitForSecondsRealtime(0.05f);
            malInputCheck = false;

        }
        public int adjustDwell(bool isPlus)
        {
            if (isPlus)
            {
                adjusterX++;
                if (adjusterX > 24) adjusterX = 24;
            }
            else
            {
                adjusterX--;
                if (adjusterX <0) adjusterX = 0;
            }
            return getDwellTime();
        }

        public int getDwellTime()
        {
            return (int)((Mathf.Exp((float)adjusterX / 12) * 300 - 150));
        }
        // Update is called once per frame
        void Update()
        {
        }
        protected void updateGazeCursor()
        {
            if (foveInterface.Gazecast(my_collider))
            {
                //foveInterface.
                eyeCursorsLocalPositionOnKeyboard[0] = transform.InverseTransformPoint(eyeCursorMeshes[0].transform.position);
                eyeCursorsLocalPositionOnKeyboard[1] = transform.InverseTransformPoint(eyeCursorMeshes[1].transform.position);

                tempGpos.x = filterX.updateData((eyeCursorsLocalPositionOnKeyboard[0].x + eyeCursorsLocalPositionOnKeyboard[1].x) / gxScale + gxOffset);
                tempGpos.y = filterY.updateData((eyeCursorsLocalPositionOnKeyboard[0].y + eyeCursorsLocalPositionOnKeyboard[1].y) / gyScale + gyOffset); 
                if (isCalibUse)
                {
                    tempGpos = getCalibratedPos(tempGpos);
                }

                gtEvent.xGazePos = tempGpos.x;
                gtEvent.yGazePos = tempGpos.y;

            }

            //gtEvent.eyeOpen = (int)FoveInterface.CheckEyesClosed();
        }
        public void doneTask()
        {
            /*
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].inputTouch(TOUCH_UP_GESTURE, 0, 0);
            }*/

            experimentManager.taskDone(inputed);
            resultBox.text = experimentManager.getResultInString();
            //resultBox.text = result;
            inputed = "";
            inputBox.text = inputed + "_";
        }

        void UI_update()
        {

            gazeMovable = false;
            int block = experimentManager.getBlock();
            int trial = experimentManager.getTrial();
            if (trial == 1)
            {
                if (block > experimentManager.maxBlock)
                {
                    exampleBox.text = "Experiment is done! Thanks!";
                    exampleBox.color = Color.green;

                }
                else
                {
                    exampleBox.text = "READY for next Block: block " + experimentManager.getBlock() + " / " + experimentManager.maxBlock;
                    exampleBox.color = Color.green;

                }


            }
            else
            {
                exampleBox.text = "(READY) Next: Block " + experimentManager.getBlock() + " - Trial: " + experimentManager.getTrial() + " / " + experimentManager.maxTrial;
                exampleBox.color = Color.yellow;
            }
        }

        private IEnumerator delayInterTask()
        {
            experimentManager.provideNext();
            UI_update();
            bool inInterval = true;
            while (inInterval)
            {
                yield return new WaitForSecondsRealtime(interTaskInterval);

                inInterval = false;
                experimentManager.setEnableLogging();
                audio.Play();
                exampleBox.text = experimentManager.getPhrase();
                exampleBox.color = Color.green;
                inputBox.text = "Please MEMORIZE the phrase...";
                StartCoroutine("delayForMemorize");
                gazeMovable = true;
            }
        }
        private IEnumerator delayForRecording()
        {
            bool inInterval = true;
            experimentManager.recordResults();
            while (inInterval)
            {
                yield return new WaitForSecondsRealtime(interRecordingInterval);

                inInterval = false;
                if (!isDynamicCascading)
                {
                    StartCoroutine("delayInterTask");
                }
                else
                {
                    if (experimentManager.getBlock() > 0)
                    {
                        DC.GetComponent<Dynamic_cascade>().adjustDwellPhaseStart();
                    }
                    else
                    {
                        gtEvent.eventLabel = 's';
                        gtEvent.selected = Key.Start;
                        experimentManager.appendGazeTouchEvent(gtEvent);
                        gtEvent.selected = (char)0;
                        DC.GetComponent<Dynamic_cascade>().adjustDwellPhaseDone();
                        
                        StartCoroutine("delayInterTask");
                    }

                }
            }

        }
        private IEnumerator delayForMemorize()
        {
            bool inInterval = true;
            while (inInterval)
            {
                yield return new WaitForSecondsRealtime(memorizeInterval);

                inInterval = false;
                enableTyping = true;
                audio.Play();
                exampleBox.color = Color.black;
                inputBox.text = "_";
            }

        }
        virtual public char keyInputed(char raw) { return (char)0; }
        virtual public char keyDown(char raw) { return (char)0; }

        public void checkRawKeyInput()
        {

            char rawKey = (char)0;
            if (isKeyDown)
            {
                rawKey = (char)0;
                if (Input.GetKeyUp(KeyCode.Keypad7))
                {
                    rawKey = 'Q';
                }
                else if (Input.GetKeyUp(KeyCode.Keypad8))
                {
                    rawKey = 'W';

                }
                else if (Input.GetKeyUp(KeyCode.Keypad9))
                {
                    rawKey = 'E';
                }
                else if (Input.GetKeyUp(KeyCode.Keypad4))
                {
                    rawKey = 'A';
                }
                else if (Input.GetKeyUp(KeyCode.Keypad5))
                {
                    rawKey = 'S';
                }
                else if (Input.GetKeyUp(KeyCode.Keypad6))
                {
                    rawKey = 'D';
                }
                else if (Input.GetKeyUp(KeyCode.Keypad1))
                {
                    rawKey = 'Z';
                }
                else if (Input.GetKeyUp(KeyCode.Keypad2))
                {
                    rawKey = 'X';
                }
                else if (Input.GetKeyUp(KeyCode.Keypad3))
                {
                    rawKey = 'C';
                }

                if (rawKey > (char)0)
                {

                    keyEntered(keyInputed(rawKey));
                }
                else if (Input.GetKeyUp(KeyCode.Return))
                {
                    rawKey = Key.Enter;
                    keyEntered(rawKey);
                }

            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    rawKey = 'Q';
                }
                else if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    rawKey = 'W';

                }
                else if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    rawKey = 'E';
                }
                else if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    rawKey = 'A';
                }
                else if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    rawKey = 'S';
                }
                else if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    rawKey = 'D';
                }
                else if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    rawKey = 'Z';
                }
                else if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    rawKey = 'X';
                }
                else if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    rawKey = 'C';
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    rawKey = Key.Enter;
                }
                if (rawKey > (char)0)
                {

                    keyDown(rawKey);
                }
            }
        }

        private void OnApplicationQuit()
        {
            serialPortClose();
        }
        void serialPortClose()
        {
            if(watchConnector != null)
            {
                if (watchConnector.IsOpen) watchConnector.Close();
            }
        }

        public void initTouchPad()
        {
            watchConnector = new SerialPort("\\\\.\\COM13", 115200);//google glass, in unity, port# over 9 should follow this format
            watchConnector.ReadTimeout = 1;
            watchConnector.Open();
            print("Touch connecting: " + watchConnector.IsOpen);
            StartCoroutine("watchProcessing");
        }
        IEnumerator watchProcessing()
        {
            while (watchConnector.IsOpen)
            {
                yield return new WaitForSecondsRealtime(0.02f); //10ms interval
                watchInputCheck();
            }
        }

        public void watchInputCheck()
        {

            try
            {
                watchPacket = watchConnector.ReadLine();
            }
            catch (System.TimeoutException e)
            {
                //print("timeout @ read BT");
            }
            if (enableTyping)
            {
                try
                {
                    if (watchPacket != null)
                    {
                        string[] temp = watchPacket.Split(' ');
                        if (temp[0] == "d")
                        {
                            gazeMovable = true;
                        }
                        else
                        {
                            interpretPacket(temp);
                        }
                    }
                }
                catch (System.NullReferenceException e) { }

            }
        }

        virtual public void interpretPacket(string[] splitedPacket) { }

        IEnumerator recordGazeCursor()
        {
            while (true)
            {

                gtEvent.eventTime = TimeUtils.currentTimeMillis();
                gtEvent.eventLabel = 'g';
                experimentManager.appendGazeTouchEvent(gtEvent);
                yield return new WaitForSecondsRealtime(0.03f);
            }
        }

        virtual public void setWordOntheKeyboard(int where, bool mustUpdate) { }


        public void initCalib()
        {

            gazeRef = new Vector2[calibPoint];
            markerPos = new Vector2[calibPoint];
            markerLayout = new string[calibPoint];
            markerLayout[0] = "●";
            markers = GameObject.FindGameObjectsWithTag("marker");

            Vector3 basicPosition = markers[0].transform.localPosition;
            markerWidth = markers[0].transform.localScale.x;
            markerHeight = markers[0].transform.localScale.y;
            Text text;


            foreach (GameObject g in markers)
            {

                g.GetComponentInChildren<Text>().text = "●";
            }
            markers[0].GetComponent<Calib_marker>().key = 'q';
            Vector3 keyPosition = new Vector3(basicPosition.x, basicPosition.y, basicPosition.z);
            markers[0].transform.localPosition = keyPosition;
            markers[1].GetComponent<Calib_marker>().key = 'w';
            keyPosition = new Vector3(basicPosition.x + 0.055f * 4.5f, basicPosition.y, basicPosition.z);
            markers[1].transform.localPosition = keyPosition;
            markers[2].GetComponent<Calib_marker>().key = 'e';
            keyPosition = new Vector3(basicPosition.x + 0.055f * 9, basicPosition.y, basicPosition.z);
            markers[2].transform.localPosition = keyPosition;
            markers[3].GetComponent<Calib_marker>().key = 'a';
            keyPosition = new Vector3(basicPosition.x, basicPosition.y - 0.07f * 2, basicPosition.z);
            markers[3].transform.localPosition = keyPosition;
            markers[4].GetComponent<Calib_marker>().key = 's';
            keyPosition = new Vector3(basicPosition.x + 0.055f * 4.5f, basicPosition.y - 0.07f * 2, basicPosition.z);
            markers[4].transform.localPosition = keyPosition;
            markers[5].GetComponent<Calib_marker>().key = 'd';
            keyPosition = new Vector3(basicPosition.x + 0.055f * 9, basicPosition.y - 0.07f * 2, basicPosition.z);
            markers[5].transform.localPosition = keyPosition;
            markers[6].GetComponent<Calib_marker>().key = 'z';
            keyPosition = new Vector3(basicPosition.x, basicPosition.y - 0.07f * 4, basicPosition.z);
            markers[6].transform.localPosition = keyPosition;
            markers[7].GetComponent<Calib_marker>().key = 'x';
            keyPosition = new Vector3(basicPosition.x + 0.055f * 4.5f, basicPosition.y - 0.07f * 4, basicPosition.z);
            markers[7].transform.localPosition = keyPosition;
            markers[8].GetComponent<Calib_marker>().key = 'c';
            keyPosition = new Vector3(basicPosition.x + 0.055f * 9, basicPosition.y - 0.07f * 4, basicPosition.z);
            markers[8].transform.localPosition = keyPosition;

            int i = 0;
            foreach (GameObject g in markers)
            {
                //should match input plane transform
                gazeRef[i] = new Vector2();
                markerPos[i] = transform.InverseTransformPoint(g.transform.position);
                
                i++;
            }
        }


        public void getPositionAt(int which)
        {
            audio.Play();
            gazeRef[which].x = gtEvent.xGazePos;
            gazeRef[which].y = gtEvent.yGazePos;
            markers[which].GetComponent<Calib_marker>().set("O");

        }

        public void calculateMat()
        {
            audio.Play();
            T1 = calcurateTransformMatrixAt(0, 1, 3, 4);
            T2 = calcurateTransformMatrixAt(1, 2, 4, 5);
            T3 = calcurateTransformMatrixAt(3, 4, 6, 7);
            T4 = calcurateTransformMatrixAt(4, 5, 7, 8);

            foreach (GameObject g in markers)
            {
                g.GetComponent<Calib_marker>().set("O");
            }
            audio.Play();
        }
        public Matrix4x4 calcurateTransformMatrixAt(int refTL, int refTR, int refBL, int refBR)
        {
            Matrix4x4 mat, screenMat, pupilMat;
            mat = new Matrix4x4();
            screenMat = new Matrix4x4();
            pupilMat = new Matrix4x4();
            screenMat.m00 = markerPos[refTL].x;
            screenMat.m01 = markerPos[refTR].x;
            screenMat.m02 = markerPos[refBL].x;
            screenMat.m03 = markerPos[refBR].x;
            screenMat.m10 = markerPos[refTL].y;
            screenMat.m11 = markerPos[refTR].y;
            screenMat.m12 = markerPos[refBL].y;
            screenMat.m13 = markerPos[refBR].y;
            screenMat.m20 = 0;
            screenMat.m21 = 0;
            screenMat.m22 = 0;
            screenMat.m23 = 0;
            screenMat.m30 = 0;
            screenMat.m31 = 0;
            screenMat.m32 = 0;
            screenMat.m33 = 0;



            pupilMat.m00 = gazeRef[refTL].x;
            pupilMat.m01 = gazeRef[refTR].x;
            pupilMat.m02 = gazeRef[refBL].x;
            pupilMat.m03 = gazeRef[refBR].x;
            pupilMat.m10 = gazeRef[refTL].y;
            pupilMat.m11 = gazeRef[refTR].y;
            pupilMat.m12 = gazeRef[refBL].y;
            pupilMat.m13 = gazeRef[refBR].y;
            pupilMat.m20 = gazeRef[refTL].x * gazeRef[refTL].y;
            pupilMat.m21 = gazeRef[refTR].x * gazeRef[refTR].y;
            pupilMat.m22 = gazeRef[refBL].x * gazeRef[refBL].y;
            pupilMat.m23 = gazeRef[refBR].x * gazeRef[refBR].y;
            pupilMat.m30 = 1;
            pupilMat.m31 = 1;
            pupilMat.m32 = 1;
            pupilMat.m33 = 1;
            /*
            screenMat.m00 = markerPos[refTL].x;
            screenMat.m10 = markerPos[refTR].x;
            screenMat.m20 = markerPos[refBL].x;
            screenMat.m30 = markerPos[refBR].x;
            screenMat.m01 = markerPos[refTL].y;
            screenMat.m11 = markerPos[refTR].y;
            screenMat.m21 = markerPos[refBL].y;
            screenMat.m31 = markerPos[refBR].y;
            screenMat.m02 = 0;
            screenMat.m12 = 0;
            screenMat.m22 = 0;
            screenMat.m32 = 0;
            screenMat.m03 = 0;
            screenMat.m13 = 0;
            screenMat.m23 = 0;
            screenMat.m33 = 0;
            pupilMat.m00 = gazeRef[refTL].x;
            pupilMat.m10 = gazeRef[refTR].x;
            pupilMat.m20 = gazeRef[refBL].x;
            pupilMat.m30 = gazeRef[refBR].x;
            pupilMat.m01 = gazeRef[refTL].y;
            pupilMat.m11 = gazeRef[refTR].y;
            pupilMat.m21 = gazeRef[refBL].y;
            pupilMat.m31 = gazeRef[refBR].y;
            pupilMat.m02 = gazeRef[refTL].x * gazeRef[refTL].y;
            pupilMat.m12 = gazeRef[refTR].x * gazeRef[refTR].y;
            pupilMat.m22 = gazeRef[refBL].x * gazeRef[refBL].y;
            pupilMat.m32 = gazeRef[refBR].x * gazeRef[refBR].y;
            pupilMat.m03 = 1;
            pupilMat.m13 = 1;
            pupilMat.m23 = 1;
            pupilMat.m33 = 1;*/



            return screenMat * pupilMat.inverse;
        }

        virtual public void calibReady() { }
        virtual public void calibDone() { }

        public void calibReadyKeyInput()
        {
            if (Input.GetKeyUp(KeyCode.O))
            {
                calibReady();

            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                getPositionAt(0);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                getPositionAt(1);
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                getPositionAt(2);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                getPositionAt(3);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                getPositionAt(4);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                getPositionAt(5);
            }
            if (Input.GetKeyUp(KeyCode.Z))
            {
                getPositionAt(6);
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                getPositionAt(7);
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                getPositionAt(8);
            }
            if (Input.GetKeyUp(KeyCode.P))
            {
                calculateMat();
                calibDone();
            }
            if (Input.GetKeyUp(KeyCode.M))
            {
                isCalibUse = !isCalibUse;
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                audio.Play();
                gtEvent.eventLabel = 's';
                gtEvent.selected = 's';
                experimentManager.appendGazeTouchEvent(gtEvent);
                gtEvent.selected = (char)0;
                DC.GetComponent<Dynamic_cascade>().adjustDwellPhaseDone();
                gazeMovable = true;

                StartCoroutine("delayInterTask");
            }
        }


        public Vector2 getCalibratedPos(Vector2 raw)
        {
            int idx = getNearestCorner(raw);
            return getPositionAtRegion(idx, raw);

        }

        private Vector2 getPositionAtRegion(int region, Vector2 raw)
        {
            Matrix4x4 pupilMat = new Matrix4x4();
            pupilMat.m00 = raw.x;
            pupilMat.m10 = raw.y;
            pupilMat.m20 = raw.x * raw.y;
            pupilMat.m30 = 1;
            Matrix4x4 mat;
            //1st order polynomial
            switch (region)
            {
                case 1:
                    mat = T1 * pupilMat;
                    break;
                case 2:
                    mat = T2 * pupilMat;
                    break;
                case 3:
                    mat = T3 * pupilMat;
                    break;
                case 4:
                    mat = T4 * pupilMat;
                    break;
                default:
                    mat = new Matrix4x4();
                    break;
            }
            Vector2 rawPoint = new Vector2();
            rawPoint.x = mat.m00;
            rawPoint.y = mat.m10;
            /*
            Matrix rawPointMat = new Matrix(4,1);

            rawPointMat.setValueAt(0,0,rawPoint.x);
            rawPointMat.setValueAt(1,0,rawPoint.y);
            rawPointMat.setValueAt(2,0,rawPoint.x * rawPoint.y);
            rawPointMat.setValueAt(3,0,1);
            //mat = MatrixMathematics.multiply(postT, rawPointMat);
            rawPoint = new Vector2((float)mat.getValueAt(0,0), (float)mat.getValueAt(1,0));
    */
            return rawPoint;
        }


        public int getNearestCorner(Vector2 raw)
        {
            float minDist = float.MaxValue;
            float tempDist;
            float tempX, tempY;
            int id = -1;

            tempX = raw.x - gazeRef[0].x;
            tempY = raw.y - gazeRef[0].y;
            tempDist = tempX * tempX + tempY * tempY;
            if (minDist > tempDist)
            {
                minDist = tempDist;
                id = 1;
            }
            tempX = raw.x - gazeRef[2].x;
            tempY = raw.y - gazeRef[2].y;
            tempDist = tempX * tempX + tempY * tempY;
            if (minDist > tempDist)
            {
                minDist = tempDist;
                id = 2;
            }
            tempX = raw.x - gazeRef[6].x;
            tempY = raw.y - gazeRef[6].y;
            tempDist = tempX * tempX + tempY * tempY;
            if (minDist > tempDist)
            {
                minDist = tempDist;
                id = 3;
            }
            tempX = raw.x - gazeRef[8].x;
            tempY = raw.y - gazeRef[8].y;
            tempDist = tempX * tempX + tempY * tempY;
            if (minDist > tempDist)
            {
                minDist = tempDist;
                id = 4;
            }
            return id;
        }

    }

}