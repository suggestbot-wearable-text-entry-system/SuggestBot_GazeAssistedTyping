using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.ExpTools;

namespace Assets.Keyboards
{
    public class Dynamic_cascade : KeyboardBasic
    {
        public GameObject[] keys;
        public string[] keyLayout;
        public float keyWidth;
        float spacebarScale = 3;
        public float keyHeight;
        public float keyGap;
        Text dwellInfo;
        public const int maxDwell = 2066;
        public const int minDwell = 150;
        public static float baseDwell = 0.6f;
        public static int baseDwell_int = 600;
        float dwellAdjuster_y = 0.2014f;
        
        // Use this for initialization
        void Start()
        {
            gtEvent = new GazeTouchEvent();
            whichKeyboard = "DC";
            isDynamicCascading = true;
            keyLayout = new string[5];
            keyLayout[0] = "qwertyuiop";
            keyLayout[1] = "asdfghjkl";
            keyLayout[2] = "zxcvbnm";
            keyLayout[3] = "<>~";
            keyLayout[4] = "+-$";
            keys = GameObject.FindGameObjectsWithTag("eye_key");
            int keyID = 0;
            int keyRow = 0;
            int keyCol = 0;
            Vector3 basicPosition = keys[0].transform.localPosition;
            Text text;
            

            Text[] temp = gameObject.GetComponentsInChildren<Text>();
            foreach (Text t in temp)
            {
                if (t.tag.CompareTo("dwell") == 0)
                {
                    dwellInfo = t;
                    baseDwell_int = getDwellTime();
                    updateDwell();
                    break;
                }
            }
            foreach (GameObject key in keys)
            {
                if (key != null)
                {
                    keyWidth = key.transform.localScale.x;
                    keyHeight = key.transform.localScale.y;
                    keyGap = keyWidth / 15;
                    //key.transform.localScale = new Vector3(keySize, keySize, keySize / 8);
                    Vector3 keyPosition = new Vector3(basicPosition.x, basicPosition.y, basicPosition.z);
                    keyPosition.x += (keyWidth + keyGap) * keyCol + keyWidth * keyRow / 2;
                    keyPosition.y -= (keyHeight + keyGap) * keyRow;
                    key.transform.localPosition = keyPosition;
                    text = key.GetComponentInChildren<Text>();
                    
                    text.text = keyLayout[keyRow].Substring(keyCol, 1);
                    key.GetComponent<Eyekeyboard_eye_only>().key = text.text.ToCharArray()[0];
                    setBaseDwell(key);
                    if (text.text.CompareTo("~") == 0)
                    {
                        text.text = "Enter";
                        
                        keyPosition.x += (keyWidth *spacebarScale*1.5f+ keyGap);
                        key.transform.localPosition = keyPosition;

                    }else if (text.text.CompareTo("<") == 0)
                    {
                        Vector3 keyScale = new Vector3(key.transform.localScale.x * 2, key.transform.localScale.y, key.transform.localScale.z);
                        key.transform.localScale= keyScale;

                        key.transform.localPosition = keyPosition;
                        Vector3 scaleT = text.transform.localScale;
                        scaleT.x /= 2;
                        text.transform.localScale = scaleT;
                        text.text = "del";
                    }
                    else if (text.text.CompareTo(">") == 0)
                    {
                        Vector3 keyScale = new Vector3(key.transform.localScale.x * spacebarScale, key.transform.localScale.y, key.transform.localScale.z);
                        key.transform.localScale = keyScale;
                        keyPosition.x = basicPosition.x + (keyWidth * spacebarScale + keyGap) * keyCol * 1.5f;
                        key.transform.localPosition = keyPosition;
                        text.text = "Space";

                        Vector3 scaleT = text.transform.localScale;
                        scaleT.x /= (spacebarScale);
                        text.transform.localScale = scaleT;
                    }
                    else if (text.text.CompareTo("-") == 0)
                    {
                        Vector3 keyScale = new Vector3(key.transform.localScale.x * spacebarScale, key.transform.localScale.y, key.transform.localScale.z);
                        key.transform.localScale = keyScale;
                        keyPosition.x = basicPosition.x + keyWidth * 1.7f;
                        keyPosition.y = dwellAdjuster_y;
                        key.transform.localPosition = keyPosition;
                        //text.text = "-50";
                        
                    }

                    else if (text.text.CompareTo("+") == 0)
                    {
                        Vector3 keyScale = new Vector3(key.transform.localScale.x * spacebarScale, key.transform.localScale.y, key.transform.localScale.z);
                        key.transform.localScale = keyScale;
                        keyPosition.x = basicPosition.x + keyWidth * 4.8f;
                        keyPosition.y = dwellAdjuster_y;
                        key.transform.localPosition = keyPosition;
                        //text.text = "";
                    }
                    else if (text.text.CompareTo("$") == 0)
                    {
                        Vector3 keyScale = new Vector3(key.transform.localScale.x * spacebarScale, key.transform.localScale.y, key.transform.localScale.z);
                        key.transform.localScale = keyScale;
                        keyPosition.x = basicPosition.x + keyWidth * 9.4f;
                        keyPosition.y = dwellAdjuster_y;
                        key.transform.localPosition = keyPosition;
                        text.text = "Start";
                        text.transform.localScale = new Vector3(text.transform.localScale.x / spacebarScale, text.transform.localScale.y, text.transform.localScale.z);
                    }

                    text.transform.localScale = new Vector3(text.transform.localScale.x, text.transform.localScale.y * keyWidth / keyHeight, text.transform.localScale.z);
                    keyCol++;
                    if (keyCol == keyLayout[keyRow].Length)
                    {
                        keyCol = 0;
                        keyRow++;
                    }
                    keyID++;

                }
            }
            initExperiment();
            initCalib();
            foreach (GameObject g in markers)
            {
                g.SetActive(false);
            }
            foreach (GameObject g in keys)
            {
                g.SetActive(true);
            }
            keys[31].SetActive(false);
            keys[28].SetActive(true);
            audio.Play();
            if(startTrial > 0)
            {
                keyEntered(Key.Enter);
            }
            else
            {

                adjustDwellPhaseStart();
            }
        }

        public void adjustDwellPhaseStart()
        {
            experimentManager.setAdditionalPhase();
            enableTyping = true;
            keys[28].SetActive(false);
            keys[29].SetActive(true);
            keys[30].SetActive(true);
            keys[31].SetActive(true);
            exampleBox.text = "PLEASE ADJUSTING DWELL TIME";
            exampleBox.color = Color.green;
            inputBox.text = "_";
        }
        public void adjustDwellPhaseDone()
        {
            enableTyping = false;

            gazeMovable = false;
            experimentManager.recorRawData();
            experimentManager.recordInputed();
            experimentManager.recoverPhase();
            keys[28].SetActive(true);
            keys[29].SetActive(false);
            keys[30].SetActive(false);
            keys[31].SetActive(false);
            inputBox.text = "_";
            inputed = "";
            experimentManager.taskDone(inputed);
        }
        public void setBaseDwell(GameObject g)
        {
            g.GetComponent<Eyekeyboard_eye_only>().setDwell(baseDwell);
        }

        public void updateDwell()
        {
            baseDwell = (float)baseDwell_int/1000;
            foreach (GameObject key in keys)
            {
                setBaseDwell(key);
            }
            dwellInfo.text = baseDwell_int + "ms";
        }
        // Update is called once per frame
        void Update()
        {
            updateGazeCursor();
            checkEyeKeyboard();
            calibReadyKeyInput();
        }

        void checkEyeKeyboard()
        {
            if (gazeMovable)
            {
                integratedEyeCursorPosition.x = gtEvent.xGazePos;
                integratedEyeCursorPosition.y = gtEvent.yGazePos;
                integratedEyeCursor.transform.localPosition = integratedEyeCursorPosition;

            }
        }

        override public void calibReady() {
            isCalibUse = false;
            foreach (GameObject g in markers)
            {
                g.SetActive(true);
            }
            foreach (GameObject g in keys)
            {
                g.SetActive(false);
            }

        }
        override public void calibDone() {
            foreach (GameObject g in markers)
            {
                g.SetActive(false);
            }
            foreach (GameObject g in keys)
            {
                g.SetActive(true);
            }
            isCalibUse = true;
        }
    }
}