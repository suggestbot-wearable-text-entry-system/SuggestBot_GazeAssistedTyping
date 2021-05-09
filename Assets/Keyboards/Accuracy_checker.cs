using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.ExpTools;

namespace Assets.Keyboards
{
    public class Accuracy_checker : KeyboardBasic
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
            whichKeyboard = "Accuracy";
            isDynamicCascading = true;
            keyLayout = new string[5];
            keyLayout[0] = "●";
            keys = GameObject.FindGameObjectsWithTag("eye_key");

            Vector3 basicPosition = keys[0].transform.localPosition;
            keyWidth = keys[0].transform.localScale.x;
            keyHeight = keys[0].transform.localScale.y;
            Text text;
            
            
            foreach (GameObject g in keys)
            {

                g.GetComponentInChildren<Text>().text = "●";
            }
            keys[0].GetComponent<accuracy_target>().key = 'a';
            Vector3 keyPosition = new Vector3(basicPosition.x, basicPosition.y, basicPosition.z);
            keys[0].transform.localPosition = keyPosition;
            keys[1].GetComponent<accuracy_target>().key = 'b';
            keyPosition = new Vector3(basicPosition.x + 0.055f * 9, basicPosition.y, basicPosition.z);
            keys[1].transform.localPosition = keyPosition;
            keys[2].GetComponent<accuracy_target>().key = 'c';
            keyPosition = new Vector3(basicPosition.x + 0.055f*4.5f , basicPosition.y - 0.07f * 2, basicPosition.z);
            keys[2].transform.localPosition = keyPosition;
            keys[3].GetComponent<accuracy_target>().key = 'd';
            keyPosition = new Vector3(basicPosition.x, basicPosition.y - 0.07f * 4, basicPosition.z);
            keys[3].transform.localPosition = keyPosition;
            keys[4].GetComponent<accuracy_target>().key = 'e';
            keyPosition = new Vector3(basicPosition.x + 0.055f * 9, basicPosition.y - 0.07f * 4, basicPosition.z);
            keys[4].transform.localPosition = keyPosition;
            setBaseDwell(keys[0]);
            setBaseDwell(keys[1]);
            setBaseDwell(keys[2]);
            setBaseDwell(keys[3]);
            setBaseDwell(keys[4]);
            initExperiment();
            audio.Play();
        }
        
        public void setBaseDwell(GameObject g)
        {
            g.GetComponent<accuracy_target>().setDwell(baseDwell);
        }
        
        // Update is called once per frame
        void Update()
        {
            updateGazeCursor();
            checkEyeKeyboard();
            if (Input.GetKeyUp(KeyCode.Return))
            {
                keyEntered(Key.Enter);
            }
        }

        void checkEyeKeyboard()
        {
            integratedEyeCursorPosition.x = gtEvent.xGazePos;
            integratedEyeCursorPosition.y = gtEvent.yGazePos;
            integratedEyeCursor.transform.localPosition = integratedEyeCursorPosition;
        }
    }
}