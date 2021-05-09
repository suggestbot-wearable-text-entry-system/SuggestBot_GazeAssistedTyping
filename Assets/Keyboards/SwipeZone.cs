using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.ExpTools;

namespace Assets.Keyboards
{
    public class SwipeZone : KeyboardBasic
    {
        int zoneForward = 400;
        int zoneBackward= 900;
        GameObject[] keyboards;
        string[] keysTemp;
        char[,] keys = new char[9, 3];
        float keyboardWidth;
        float keyboardHeight;
        float keyboardGap;
        Canvas[,] deactivater;
        const int left = 0;
        const int center = 1;
        const int right = 2;
        const int whole = 3;
        float border = 0.003f;
        // Use this for initialization
        void Start()
        {
            gtEvent = new GazeTouchEvent();
            whichKeyboard = "SwipeZone";
            keyboards = GameObject.FindGameObjectsWithTag("SwipeZone_keyboard");
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
            deactivater = new Canvas[9, 4];
            keyboardHeight = keyboards[0].transform.localScale.y;
            keyboardWidth = keyboards[0].transform.localScale.x;
            keyboardGap = keyboardHeight / 15;
            Vector3 basicKeyboardPosition = keyboards[0].transform.localPosition;


            int sub = 0;
            foreach (string key in keysTemp)
            {
                temp = key.ToCharArray();
                keys[sub, 0] = temp[0];
                keys[sub, 1] = temp[1];
                keys[sub, 2] = temp[2];
                sub++;
            }

            for (int i = 0; i < keyboards.Length; i++)
            {
                keyboards[i].transform.localPosition = new Vector3(basicKeyboardPosition.x + (i / 3) * (keyboardWidth + keyboardGap), basicKeyboardPosition.y - (i % 3) * (keyboardHeight + keyboardGap), basicKeyboardPosition.z);
                Canvas[] tempC = keyboards[i].GetComponentsInChildren<Canvas>();
                deactivater[i, left] = keyboards[i].GetComponentsInChildren<Canvas>()[left];
                deactivater[i, center] = keyboards[i].GetComponentsInChildren<Canvas>()[center];
                deactivater[i, right] = keyboards[i].GetComponentsInChildren<Canvas>()[right];
                deactivater[i, whole] = keyboards[i].GetComponentsInChildren<Canvas>()[whole];

                Text[] keysOn = keyboards[i].GetComponentsInChildren<Text>();
                for (int k = 0; k < 3; k++)
                {
                    keysOn[k].text = "" + keys[i, k];
                }
                if(i == 0 || i == 2 || i== 6 || i == 8)
                {

                    keyboards[i].GetComponent<MeshRenderer>().material.color = Color.white;
                    MeshFilter[] meshes = keyboards[i].GetComponentsInChildren<MeshFilter>();
                    int k = 0;
                    Vector3 pos;
                    Canvas[] canvases = keyboards[i].GetComponentsInChildren<Canvas>();
                    foreach (Canvas c in canvases)
                    {
                        if (c.tag.CompareTo("highlight") == 0)
                        {
                            pos = c.transform.position;
                            switch (i)
                            {
                                case 0:
                                    pos.x -= border;
                                    pos.y += border;
                                    break;
                                case 2:
                                    pos.x -= border;
                                    pos.y -= border;
                                    break;
                                case 6:
                                    pos.x += border;
                                    pos.y += border;
                                    break;
                                case 8:
                                    pos.x += border;
                                    pos.y -= border;
                                    break;
                            }
                            c.transform.position = pos;

                        }
                    }
                    foreach (MeshFilter mesh in meshes)
                    {
                        if (k == 0)
                        {
                            k++;
                            pos = mesh.transform.position;
                            switch (i)
                            {
                                case 0:
                                    pos.x += keyboardGap;
                                    pos.y -= keyboardGap;
                                    break;
                                case 2:
                                    pos.x += keyboardGap;
                                    pos.y += keyboardGap;
                                    break;
                                case 6:
                                    pos.x -= keyboardGap;
                                    pos.y -= keyboardGap;
                                    break;
                                case 8:
                                    pos.x -= keyboardGap;
                                    pos.y += keyboardGap;
                                    break;
                            }
                            mesh.transform.position = pos;
                        }
                        else { 
                            pos = mesh.transform.position;
                            switch (i)
                            {
                                case 0:
                                    pos.x -= border;
                                    pos.y += border;
                                    break;
                                case 2:
                                    pos.x -= border;
                                    pos.y -= border;
                                    break;
                                case 6:
                                    pos.x += border;
                                    pos.y += border;
                                    break;
                                case 8:
                                    pos.x += border;
                                    pos.y -= border;
                                    break;
                            }
                            mesh.transform.position = pos;
                        }
                        k++;

                    }
                }
                else
                {

                    keyboards[i].GetComponent<MeshRenderer>().material.color = Color.gray;
                }


            }
            
            deactivateAll();

            initExperiment();
            initTouchPad();

            audio.Play();
        }
        /*
        public void recording(object sender, ElapsedEventArgs a)
        {
            int currentB = experimentManager.getBlock();

            experimentManager.recordResults_provideNext();
            this.Dispatcher.Invoke((Action)(() =>
            {
                int block = experimentManager.getBlock();
                int trial = experimentManager.getTrial();
                if (trial == 1)
                {
                    if (block > maxBlock)
                    {
                        exampleBox.Content = "Experiment is done! Thanks!";
                        exampleBox.Foreground = new SolidColorBrush(Colors.SeaGreen);

                        thread.Abort();

                    }
                    else
                    {
                        delayInterBlock.Start();
                        exampleBox.Content = "READY for next Block: block " + experimentManager.getBlock() + " / " + maxBlock;
                        exampleBox.Foreground = new SolidColorBrush(Colors.SeaGreen);

                    }


                }
                else
                {
                    delayInterTask.Start();
                    exampleBox.Content = "(READY) Next: Block " + experimentManager.getBlock() + " - Trial: " + experimentManager.getTrial() + " / " + maxTrial;
                    exampleBox.Foreground = new SolidColorBrush(Colors.Gray);
                }
                exampleBox.InvalidateVisual();
            }));

        }*/
        // Update is called once per frame
        void Update()
        {
            //watchInputCheck();
            checkRawKeyInput();
            updateGazeCursor();
        }

        void deactivateAll()
        {
            foreach (Canvas c in deactivater) c.enabled = false;
            activatedWindow = -1;
        }
        void activateKeyboard(int id)
        {
            for (int i = 0; i < id; i++)
            {
                deactivater[i, whole].enabled = true;
            }
            for (int i = id + 1; i < 9; i++)
            {
                deactivater[i, whole].enabled = true;
            }
        }
        void activateKey(int id)
        {
            if (activatedWindow > -1)
            {
                deactivater[activatedWindow, id].enabled = true;
            }

        }
        
        public void keySelection(int window, int keyID)
        {
            keyEntered(keys[activatedWindow, keyID]);
            deactivater[window, keyID].enabled = true;
            StartCoroutine("keyDeactivating");

        }

        IEnumerator keyDeactivating()
        {
            yield return new WaitForSecondsRealtime(0.08f);
            deactivateAll();
        }
        //raw data handling
        public void tapInputed(int zone)
        {
            if (activatedWindow > -1)
            {
                keySelection(activatedWindow, 1);
                //keyEntered(keys[activatedWindow, 1]);
                //deactivateAll();
            }
            else
            {
                activatedWindow = zone + 1;
                activateKeyboard(activatedWindow);
            }
            gazeMovable = true;
            
        }
        public int getZone()
        {
            if (wtx < zoneForward)
            {
                return 0;
            }
            else if (wtx < zoneBackward)
            {
                return 3;
            }
            else
            {
                return 6;
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
                    tapInputed(getZone());
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
                                keyEntered(Key.Enter);
                                deactivateAll();
                                break;
                            case 'C':
                                tapInputed(getZone());
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
                                                keyEntered(Key.backspace);
                                                deactivateAll();

                                                break;
                                            case SWIPE_BOTTOM_LEFT:
                                            case SWIPE_LEFT:
                                            case SWIPE_TOP_LEFT:
                                                keyEntered(Key.space);
                                                deactivateAll();
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
                                    if (activatedWindow > -1)
                                    {
                                        switch (direction)
                                        {
                                            case SWIPE_BOTTOM_LEFT:
                                            case SWIPE_LEFT:
                                            case SWIPE_TOP_LEFT:
                                                wtx = 300;
                                                wty = 50;
                                                keySelection(activatedWindow, 2);
                                                //keyEntered(keys[activatedWindow, 2]);
                                                //deactivateAll();
                                                break;
                                            case SWIPE_BOTTOM_RIGHT: //backward
                                            case SWIPE_RIGHT:
                                            case SWIPE_TOP_RIGHT:
                                                wtx = 50;
                                                wty = 50;
                                                keySelection(activatedWindow, 0);
                                                //keyEntered(keys[activatedWindow, 0]);
                                                //deactivateAll();
                                                break;
                                            default:
                                                deactivateAll();
                                                break;
                                        }

                                    }
                                    else
                                    {

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
                                                zone = zone +2;
                                                break;
                                            default:
                                                break;

                                        }
                                        if (zone > -1)
                                        {
                                            activatedWindow = zone;
                                            activateKeyboard(activatedWindow);

                                        }

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
    }
}
