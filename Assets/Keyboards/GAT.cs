using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.ExpTools;
using Assets;

namespace Assets.Keyboards
{
    public class GAT : KeyboardBasic
    {
        GameObject[] keyboards;
        string[] keysTemp;
        char[,] keys = new char[9, 3];
        int selectedRow = 0;
        float keyboardWidth;
        float keyboardHeight;
        float keyboardGap;
        const int left = 0;
        const int center = 1;
        const int right = 2;
        const int whole = 3;
        const float xScale = 1.5f;
        // Use this for initialization
        float[] row_threshold;
        float[] col_threshold; // x axis

        GAT_keyboard[] GATkeyboards;
        //Text[] wordOnKeyboard;
        int wordOnKeyboardMaxLen = 5;
        string wordOnKeyboardShow="";
        string wordHighlightColor = "<color=#ff0000>";
        string wordHighlightColorEnd = "</color>";

        Color transparent = Color.clear;
        Color wordColor;
        int inputedLength = 0;

        bool isUpdateGazeCursor = false;
        int updateGazeCursor = 0;
        const int updateInterval = 4;



        void Start()
        {
            gtEvent = new GazeTouchEvent();
            whichKeyboard = "GAT";
            activatedWindow = 0;
            keyboards = GameObject.FindGameObjectsWithTag("GAT_keyboard");
            keysTemp = new string[9];
            keysTemp[0] = "qwe";
            keysTemp[1] = "asd";
            keysTemp[2] = "zxc";
            keysTemp[3] = "rty";
            keysTemp[4] = "fgh";
            keysTemp[5] = "vbn";
            keysTemp[6] = "uio";
            keysTemp[7] = "jkp";
            keysTemp[8] = "ml.";
            char[] temp;
            Vector3 basicKeyboardPosition = keyboards[0].transform.localPosition;
            Vector3 basicKeyboardScale = keyboards[0].transform.localScale;
            basicKeyboardScale.x *= xScale;
            Vector3 basicTextScale = keyboards[0].GetComponentInChildren<Text>().transform.localScale;
            basicTextScale.x /= xScale;

            keyboardHeight = basicKeyboardScale.y;
            keyboardWidth = basicKeyboardScale.x;
            keyboardGap = keyboardHeight / 15;

            GATkeyboards = new GAT_keyboard[9];
            col_threshold = new float[3];
            row_threshold = new float[3];
            int sub = 0;
            foreach (string key in keysTemp)
            {
                temp = key.ToCharArray();
                keys[sub, 0] = temp[0];
                keys[sub, 1] = temp[1];
                keys[sub, 2] = temp[2];
                sub++;
            }

            //wordOnKeyboard = new Text[9];
            for (int i = 0; i < keyboards.Length; i++)
            {
                keyboards[i].transform.localPosition = new Vector3(basicKeyboardPosition.x + (i / 3) * (keyboardWidth + keyboardGap), basicKeyboardPosition.y - (i % 3) * (keyboardHeight + keyboardGap), basicKeyboardPosition.z);

                keyboards[i].transform.localScale = basicKeyboardScale;
                GATkeyboards[i] = keyboards[i].GetComponent<GAT_keyboard>();
                //wordOnKeyboard[i] = keyboards[i].GetComponentsInChildren<Text>()[1];
                GATkeyboards[i].key = keyboards[i].GetComponentsInChildren<Text>()[0];

                
                GATkeyboards[i].setBasicKeys(keys[i, 0], keys[i, 1], keys[i, 2]);
                GATkeyboards[i].key.transform.localScale = basicTextScale;
                GATkeyboards[i].id = i;


            }
            //wordColor = wordOnKeyboard[0].color;
            integratedEyeCursor_yPoses = new float[3];
            integratedEyeCursor_yPoses[0] = keyboards[0].transform.localPosition.y - keyboards[0].transform.localScale.y / 2;
            integratedEyeCursor_yPoses[1] = keyboards[1].transform.localPosition.y - keyboards[1].transform.localScale.y / 2;
            integratedEyeCursor_yPoses[2] = keyboards[2].transform.localPosition.y - keyboards[2].transform.localScale.y / 2;

            col_threshold = new float[2];
            row_threshold = new float[2];

            row_threshold[0] = keyboards[1].transform.localPosition.y + (keyboards[1].transform.localScale.y + keyboardGap) / 2;
            row_threshold[1] = keyboards[2].transform.localPosition.y + (keyboards[2].transform.localScale.y + keyboardGap) / 2;
            col_threshold[0] = keyboards[3].transform.localPosition.x - (keyboards[3].transform.localScale.x - keyboardGap) / 2;
            col_threshold[1] = keyboards[6].transform.localPosition.x - (keyboards[6].transform.localScale.x - keyboardGap) / 2;
            
            initExperiment();
            initTouchPad();

            initCalib();


            foreach (GameObject g in markers)
            {
                g.SetActive(false);
            }
            foreach (GameObject g in keyboards)
            {
                g.SetActive(true);
            }
            audio.Play();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            updateGazeCursor++;
            if (updateGazeCursor > updateInterval)
            {
                updateGazeCursor();
                updateGazeCursor = 1;
            }
            if (gazeMovable)
            {
                checkEyeKeyboard();
                //checkWordOnKeyboardUpdate();

            }
            calibReadyKeyInput();
            //watchInputCheck();

        }

        override public void calibReady()
        {
            isCalibUse = false;
            foreach (GameObject g in markers)
            {
                g.SetActive(true);
            }
            foreach (GameObject g in keyboards)
            {
                g.SetActive(false);
            }

        }
        override public void calibDone()
        {
            foreach (GameObject g in markers)
            {
                g.SetActive(false);
            }
            foreach (GameObject g in keyboards)
            {
                g.SetActive(true);
            }
            isCalibUse = true;
        }
        void checkEyeKeyboard()
        {
            activateKeyboard();
            integratedEyeCursorPosition.x = gtEvent.xGazePos;
            integratedEyeCursorPosition.y = integratedEyeCursor_yPoses[selectedRow];
            integratedEyeCursor.transform.localPosition = integratedEyeCursorPosition;
            
        }
        public void activateKeyboard()
        {
            int row, col;
            if (gtEvent.xGazePos < col_threshold[0])
            {
                col = 0;
            }
            else if (gtEvent.xGazePos < col_threshold[1])
            {
                col = 1;
            }
            else
            {
                col = 2;
            }
            if (gtEvent.yGazePos > row_threshold[0])
            {
                row = 0;
            }
            else if (gtEvent.yGazePos > row_threshold[1])
            {
                row = 1;
            }
            else
            {
                row = 2;
            }

            int tempActivatedWindow = col * 3 + row;
            if(activatedWindow != tempActivatedWindow)
            {
                gtEvent.eventTime = TimeUtils.currentTimeMillis();
                keyboards[activatedWindow].GetComponent<GAT_keyboard>().deactivate();
                activatedWindow = tempActivatedWindow;
                keyboards[activatedWindow].GetComponent<GAT_keyboard>().activate();
                gtEvent.eventLabel = 'w';
                gtEvent.selected = (char)('0' + activatedWindow);
                experimentManager.appendGazeTouchEvent(gtEvent);
                gtEvent.selected = (char)0;
                selectedRow = row;
                //window change log
                /*
                for (int i = 0; i < activatedWindow; i++)
                {
                    //wordOnKeyboard[i].color = transparent;
                    wordOnKeyboard[i].enabled = false;
                }
                for (int i = activatedWindow+1; i < 9; i++)
                {
                    //wordOnKeyboard[i].color = transparent;
                    wordOnKeyboard[i].enabled = false;
                }

                //wordOnKeyboard[activatedWindow].color = wordColor;
                wordOnKeyboard[activatedWindow].enabled = true;
                */

            }
        }


        
        public void keySelection(int id)
        {
            int acti = activatedWindow;
            GATkeyboards[activatedWindow].highlighting(id);
            keyEntered(keys[acti, id]);
        }
        public void keySelection(char k)
        {
             keyEntered(k);
        }

        //raw data handling
        public void tapInputed()
        {
            gazeMovable = true;
            if (activatedWindow > -1)
            {
                keySelection(1);
            }
        }

        public override void interpretPacket(string[] splitedPacket)
        {
            switch (splitedPacket[0][0])
            {
                case 'd':
                    if (!gazeMovable)
                    {
                    }
                    break;
                case 'z': //touch move
                    gazeMovable = false;
                    wtx = int.Parse(splitedPacket[1]);
                    wty = int.Parse(splitedPacket[2]);
                    gtEvent.state = 2;
                    gtEvent.xPos = wtx;
                    gtEvent.yPos = wty;
                    //print("wtx: " + wtx);
                    break;
                case 'v': //touch up
                    gazeMovable = true;
                    gtEvent.eventTime = TimeUtils.currentTimeMillis();
                    wtx = int.Parse(splitedPacket[1]);
                    wty = int.Parse(splitedPacket[2]);
                    tapInputed();
                    gtEvent.state = 0;
                    gtEvent.eventLabel = 't';
                    experimentManager.appendGazeTouchEvent(gtEvent);
                    gtEvent.xPos = -1;
                    gtEvent.yPos = -1;
                    break;
                case 'x': //touch down
                    gtEvent.eventTime = TimeUtils.currentTimeMillis();
                    gazeMovable = false;
                    wtx = int.Parse(splitedPacket[1]);
                    wty = int.Parse(splitedPacket[2]);

                    gtEvent.state = 1;
                    gtEvent.xPos = wtx;
                    gtEvent.yPos = wty;
                    gtEvent.eventLabel = 't';
                    experimentManager.appendGazeTouchEvent(gtEvent);
                    break;
                case 's':

                    gazeMovable = true;
                    gtEvent.eventTime = TimeUtils.currentTimeMillis();
                    try
                    {
                        switch (splitedPacket[1][0])
                        {
                            case 'A': // '<'
                            case 'B': // '>'
                                break;
                            case 'D':

                                keySelection(Key.Enter);
                                break;
                            case 'C':
                                tapInputed();
                                break;
                            default:

                                int direction = int.Parse(splitedPacket[1]);
                                int multi, fingerCount;
                                multi = int.Parse(splitedPacket[2]);
                                fingerCount = multi % 10;
                                if (fingerCount > 1)
                                {
                                    if (fingerCount == 2)
                                    {
                                        switch (direction)
                                        {
                                            case SWIPE_BOTTOM_RIGHT: //backward
                                            case SWIPE_RIGHT:
                                            case SWIPE_TOP_RIGHT:
                                                keySelection(Key.backspace);
                                                gazeMovable = true;

                                                break;
                                            case SWIPE_BOTTOM_LEFT:
                                            case SWIPE_LEFT:
                                            case SWIPE_TOP_LEFT:
                                                keySelection(Key.space);
                                                gazeMovable = true;
                                                break;

                                        }
                                    }
                                    else // three finger
                                    {
                                        
                                        switch (direction)
                                        {
                                            case SWIPE_BOTTOM_RIGHT:
                                            case SWIPE_RIGHT:
                                            case SWIPE_TOP_RIGHT:
                                            case SWIPE_BOTTOM_LEFT:
                                            case SWIPE_LEFT:
                                            case SWIPE_TOP_LEFT:
                                                //keySelection(Key.Enter);
                                                gazeMovable = true;
                                                break;

                                        }

                                        
                                    }
                                }
                                else // single finger
                                {
                                    multi = int.Parse(splitedPacket[2]);
                                    if (activatedWindow > -1)
                                    {
                                        switch (direction)
                                        {
                                            case SWIPE_TOP_LEFT://backward
                                            case SWIPE_LEFT://backward
                                            case SWIPE_BOTTOM_LEFT://backward
                                                wtx = 300;
                                                wty = 50;
                                                keySelection(2);
                                                gazeMovable = true;
                                                break;
                                            case SWIPE_TOP_RIGHT://backward
                                            case SWIPE_RIGHT: //forward
                                            case SWIPE_BOTTOM_RIGHT://backward
                                                wtx = 50;
                                                wty = 50;
                                                keySelection(0);
                                                gazeMovable = true;
                                                break;
                                            default:
                                                break;
                                        }

                                    }

                                }
                                break;
                        }


                        gtEvent.state = 0;
                        gtEvent.gesture = splitedPacket[1][0];
                        gtEvent.eventLabel = 't';
                        experimentManager.appendGazeTouchEvent(gtEvent);
                        gtEvent.xPos = -1;
                        gtEvent.yPos = -1;

                    }
                    catch (System.Exception e)
                    {

                    }
                    break;

            }
        }
        /*
        public void checkWordOnKeyboardUpdate()
        {
            if(inputedLength != inputed.Length)
            {
                inputedLength = inputed.Length;
                makeWordOnKeyboad();
                setWordOntheKeyboard();
            }

        }

        public void makeWordOnKeyboad()
        {
            if(inputed.Length > 0)
            {
                string[] strtok = inputed.Split(' ');
                string lastword = strtok[strtok.Length - 1];

                if (lastword.Length > wordOnKeyboardMaxLen)
                {
                    lastword = ".." + lastword.Substring(lastword.Length - wordOnKeyboardMaxLen, wordOnKeyboardMaxLen);
                }
                if (lastword.Length > 0)
                {
                    int len = lastword.Length;
                    char[] chars = lastword.ToCharArray();
                    wordOnKeyboardShow = lastword.Substring(0,len-1) + wordHighlightColor + lastword.Substring(len - 1, 1) + wordHighlightColorEnd + "_";
                }
                else
                {
                    wordOnKeyboardShow = "_";
                }

            }
            else
            {
                wordOnKeyboardShow = "_";
            }
        }
        
        public void setWordOntheKeyboard()
        {
            foreach(Text text in wordOnKeyboard)
            {
                text.text = wordOnKeyboardShow;
            }

        }*/
    }
}