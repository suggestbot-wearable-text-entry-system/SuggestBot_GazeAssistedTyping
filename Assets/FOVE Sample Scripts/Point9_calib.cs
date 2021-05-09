using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.ExpTools;

namespace Assets.Keyboards
{
    public class Point9_calib : KeyboardBasic
    {
        const int calibPoint = 9;
        Matrix4x4 T1, T2, T3, T4;
        Vector2[] gazeRef;
        Vector2[] markerPos;


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


            gazeRef = new Vector2[calibPoint];
            markerPos = new Vector2[calibPoint];
            keyLayout = new string[calibPoint];
            keyLayout[0] = "●";
            keys = GameObject.FindGameObjectsWithTag("marker");

            Vector3 basicPosition = keys[0].transform.localPosition;
            keyWidth = keys[0].transform.localScale.x;
            keyHeight = keys[0].transform.localScale.y;
            Text text;


            foreach (GameObject g in keys)
            {

                g.GetComponentInChildren<Text>().text = "●";
            }
            keys[0].GetComponent<Calib_marker>().key = 'q';
            Vector3 keyPosition = new Vector3(basicPosition.x, basicPosition.y, basicPosition.z);
            keys[0].transform.localPosition = keyPosition;
            keys[1].GetComponent<Calib_marker>().key = 'w';
            keyPosition = new Vector3(basicPosition.x + 0.055f * 4.5f, basicPosition.y, basicPosition.z);
            keys[1].transform.localPosition = keyPosition;
            keys[2].GetComponent<Calib_marker>().key = 'e';
            keyPosition = new Vector3(basicPosition.x + 0.055f * 9, basicPosition.y, basicPosition.z);
            keys[2].transform.localPosition = keyPosition;
            keys[3].GetComponent<Calib_marker>().key = 'a';
            keyPosition = new Vector3(basicPosition.x, basicPosition.y - 0.07f * 2, basicPosition.z);
            keys[3].transform.localPosition = keyPosition;
            keys[4].GetComponent<Calib_marker>().key = 's';
            keyPosition = new Vector3(basicPosition.x + 0.055f * 4.5f, basicPosition.y - 0.07f * 2, basicPosition.z);
            keys[4].transform.localPosition = keyPosition;
            keys[5].GetComponent<Calib_marker>().key = 'd';
            keyPosition = new Vector3(basicPosition.x + 0.055f * 9, basicPosition.y - 0.07f * 2, basicPosition.z);
            keys[5].transform.localPosition = keyPosition;
            keys[6].GetComponent<Calib_marker>().key = 'z';
            keyPosition = new Vector3(basicPosition.x, basicPosition.y - 0.07f * 4, basicPosition.z);
            keys[6].transform.localPosition = keyPosition;
            keys[7].GetComponent<Calib_marker>().key = 'x';
            keyPosition = new Vector3(basicPosition.x + 0.055f * 4.5f, basicPosition.y - 0.07f * 4, basicPosition.z);
            keys[7].transform.localPosition = keyPosition;
            keys[8].GetComponent<Calib_marker>().key = 'c';
            keyPosition = new Vector3(basicPosition.x + 0.055f * 9, basicPosition.y - 0.07f * 4, basicPosition.z);
            keys[8].transform.localPosition = keyPosition;

            int i = 0;
            foreach(GameObject g in keys)
            {
                //should match input plane transform
                gazeRef[i] = new Vector2();
                markerPos[i] = new Vector2();
                markerPos[i].x = g.transform.localPosition.x;
                markerPos[i].y = g.transform.localPosition.y;
                i++;
            }
            initExperiment();
            audio.Play();
        }

        public void getPositionAt(int which)
        {
            audio.Play();
            gazeRef[which].x = gtEvent.xGazePos;
            gazeRef[which].y = gtEvent.yGazePos;
            keys[which].GetComponent<Calib_marker>().set("O");
           
        }

        public void calculateMat()
        {
            audio.Play();
            T1 = calcurateTransformMatrixAt(0, 1, 3, 4);
            T2 = calcurateTransformMatrixAt(1, 2, 4, 5);
            T3 = calcurateTransformMatrixAt(3, 4, 6, 7);
            T4 = calcurateTransformMatrixAt(4, 5, 7, 8);

            foreach (GameObject g in keys)
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
            

            
            return screenMat * pupilMat.inverse;
        }

        // Update is called once per frame
        void Update()
        {
            updateGazeCursor();
            //checkEyeKeyboard();
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
            }
        }

        public Vector2 getCalibratedPos(Vector2 raw)
        {
            int idx = getNearestCorner(raw);
            return getPositionAtRegion(idx, raw);

        }

        private Vector2 getPositionAtRegion(int region, Vector2 raw)
        {
            Vector4 pupilMat = new Vector4();
            pupilMat.x = raw.x;
            pupilMat.y = raw.y;
            pupilMat.z = raw.x * raw.y;
            pupilMat.z = 1;
            Vector4 mat;
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
                    mat = new Vector4();
                    break;
            }
            Vector2 rawPoint = new Vector2();
            rawPoint.x = mat.x;
            rawPoint.y = mat.y;
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

        void checkEyeKeyboard()
        {
            integratedEyeCursorPosition.x = gtEvent.xGazePos;
            integratedEyeCursorPosition.y = gtEvent.yGazePos;
            integratedEyeCursor.transform.localPosition = integratedEyeCursorPosition;
        }
    }
}