using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.ExpTools;
using Assets;

namespace Assets.Keyboards
{
    public class GAT3_adv : KeyboardBasic
    {
        int zoneForward = 400;
        int zoneBackward = 900;
        GameObject[] keyboards;
        string[] keysTemp;
        char[,] keys = new char[3, 9];
        int selectedRow = 0;
        float keyboardWidth;
        float keyboardHeight;
        float keyboardGap;
        const int left = 0;
        const int center = 1;
        const int right = 2;
        const int whole = 3;
        const float xScale = 1.3f;
        // Use this for initialization
        float[] row_threshold;
        float[] col_threshold; // x axis

        GAT_keyboard_zone[] GATkeyboards;
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
            whichKeyboard = "GAT3_adv";
            activatedWindow = 0;
            keyboards = GameObject.FindGameObjectsWithTag("GAT_keyboard_zone");
            keysTemp = new string[3];
            keysTemp[0] = "qweasdzxc";
            keysTemp[1] = "rtyfghvbn";
            keysTemp[2] = "uioklpml.";
            char[] temp;
            Vector3 basicKeyboardPosition = keyboards[0].transform.localPosition;
            Vector3 basicKeyboardScale = keyboards[0].transform.localScale;
            basicKeyboardScale.x *= xScale;
            Vector3 basicTextScale = keyboards[0].GetComponentInChildren<Text>().transform.localScale;
            basicTextScale.x /= xScale;

            keyboardHeight = basicKeyboardScale.y;
            keyboardWidth = basicKeyboardScale.x;
            keyboardGap = keyboardHeight / 15;

            GATkeyboards = new GAT_keyboard_zone[3];
            col_threshold = new float[3];
            int sub = 0;
            foreach (string key in keysTemp)
            {
                temp = key.ToCharArray();
                keys[sub, 0] = temp[0];
                keys[sub, 1] = temp[1];
                keys[sub, 2] = temp[2];
                keys[sub, 3] = temp[3];
                keys[sub, 4] = temp[4];
                keys[sub, 5] = temp[5];
                keys[sub, 6] = temp[6];
                keys[sub, 7] = temp[7];
                keys[sub, 8] = temp[8];
                sub++;
            }

            //wordOnKeyboard = new Text[9];
            for (int i = 0; i < keyboards.Length; i++)
            {
                keyboards[i].transform.localPosition = new Vector3(basicKeyboardPosition.x + (i) * (keyboardWidth + keyboardGap), basicKeyboardPosition.y , basicKeyboardPosition.z);

                keyboards[i].transform.localScale = basicKeyboardScale;
                GATkeyboards[i] = keyboards[i].GetComponent<GAT_keyboard_zone>();
                //wordOnKeyboard[i] = keyboards[i].GetComponentsInChildren<Text>()[1];
                GATkeyboards[i].key = keyboards[i].GetComponentsInChildren<Text>()[0];

                
                GATkeyboards[i].setBasicKeys(keysTemp[i].ToCharArray());
                GATkeyboards[i].key.transform.localScale = basicTextScale;
                GATkeyboards[i].id = i;


            }
            //wordColor = wordOnKeyboard[0].color;
            integratedEyeCursor_yPoses = new float[3];
            integratedEyeCursor_yPoses[0] = keyboards[0].transform.localPosition.y - keyboards[0].transform.localScale.y / 2;

            col_threshold = new float[2];
            
            col_threshold[0] = keyboards[1].transform.localPosition.x - (keyboards[1].transform.localScale.x - keyboardGap) / 2;
            col_threshold[1] = keyboards[2].transform.localPosition.x - (keyboards[2].transform.localScale.x - keyboardGap) / 2;
            selectedRow = 0;
            
            initExperiment();
            initTouchPad();

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
            //watchInputCheck();
            
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
            int col;
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

            int tempActivatedWindow = col;
            if(activatedWindow != tempActivatedWindow)
            {
                gtEvent.eventTime = TimeUtils.currentTimeMillis();
                keyboards[activatedWindow].GetComponent<GAT_keyboard_zone>().deactivate();
                activatedWindow = tempActivatedWindow;
                keyboards[activatedWindow].GetComponent<GAT_keyboard_zone>().activate();
                gtEvent.eventLabel = 'w';
                gtEvent.selected = (char)('0' + activatedWindow);
                experimentManager.appendGazeTouchEvent(gtEvent);
                gtEvent.selected = (char)0;
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
            gazeMovable = true;
        }
        public void keySelection(char k)
        {
             keyEntered(k);
            gazeMovable = true;
        }
        
        //raw data handling
        public void tapInputed()
        {
            if (activatedWindow > -1)
            {
                keySelection(getZone() + 2);
            }

        }

        public int getZone()
        {
            if (wtx < zoneForward)
            {
                return 0;
            }
            else if (wtx < zoneBackward)
            {
                return 1;
            }
            else
            {
                return 2;
            }

        }
        public override void interpretPacket(string[] splitedPacket)
        {
            switch (splitedPacket[0][0])
            {
                case 'd':
                    break;
                case 'z': //touch move
                    wtx = int.Parse(splitedPacket[1]);
                    wty = int.Parse(splitedPacket[2]);
                    gtEvent.state = 2;
                    gtEvent.xPos = wtx;
                    gtEvent.yPos = wty;
                    //print("wtx: " + wtx);
                    break;
                case 'v': //touch up
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
                                int multi, zone, fingerCount;
                                multi = int.Parse(splitedPacket[2]);
                                fingerCount = multi % 10;
                                zone = multi / 10;
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

                                                break;
                                            case SWIPE_BOTTOM_LEFT:
                                            case SWIPE_LEFT:
                                            case SWIPE_TOP_LEFT:
                                                keySelection(Key.space);
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
                                                //keyEntered(Key.Enter);
                                                //deactivateAll();
                                                break;

                                        }

                                    }
                                }
                                else // single finger
                                {
                                    multi = int.Parse(splitedPacket[2]);
                                    zone = multi / 10;

                                    zone = getZone();
                                    switch (direction)
                                    {
                                        case SWIPE_TOP_RIGHT:
                                        case SWIPE_TOP_LEFT:
                                        case SWIPE_TOP:
                                            zone = zone;
                                            break;
                                        case SWIPE_BOTTOM:
                                        case SWIPE_BOTTOM_RIGHT:
                                        case SWIPE_BOTTOM_LEFT:
                                            zone = zone + 6;
                                            break;
                                        default:
                                            break;

                                    }
                                    if (zone > -1)
                                    {
                                        keySelection(zone);

                                    }


                                }
                                break;
                        }

                        gazeMovable = true;

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