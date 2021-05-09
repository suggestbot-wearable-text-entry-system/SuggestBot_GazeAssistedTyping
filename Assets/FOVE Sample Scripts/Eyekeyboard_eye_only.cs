using System.Collections;
using UnityEngine;
using Assets.ExpTools;
using UnityEngine.UI;
using Assets.Keyboards;
namespace Assets
{
    public class Eyekeyboard_eye_only : MonoBehaviour
    {

        private Collider my_collider;
        private Material material;
        private MeshFilter border;
        private MeshRenderer borderRenderer;
        private MeshRenderer boxRenderer;
        
        private float dwell_pre;
        private float dwell = 0;
        private long startTime = TimeUtils.currentTimeMillis();
        bool isDwell = false;
        float wait = 0.02f; // in ms
        public char key;
        GameObject DC;
        bool isMarked = false;

        
        void Start()
        {
            my_collider = GetComponent<Collider>();

            border = GetComponentsInChildren<MeshFilter>()[1];
            borderRenderer = border.GetComponent<MeshRenderer>();
            borderRenderer.material.color = Color.black;

            boxRenderer = GetComponentsInChildren<MeshFilter>()[0].GetComponent<MeshRenderer>();

            boxRenderer.material.color = Color.black;
            dwell_pre = Dynamic_cascade.baseDwell / 3;
            material = gameObject.GetComponent<Renderer>().material;
            DC = GameObject.Find("Eye_only");
            if (material == null)
                gameObject.SetActive(false);
        }

        public void setDwell(float d)
        {
            dwell_pre = Dynamic_cascade.baseDwell / 3;
        }

        public void dwellOn()
        {
            startTime = TimeUtils.currentTimeMillis();
            isDwell = true;
            StartCoroutine("DwellChecker");
        }
        public void leave()
        {

            StopCoroutine("DwellChecker");
            isDwell = false;
            dwell = 0; 
            boxRenderer.material.color = Color.black;
            borderRenderer.material.color = Color.black;


        }

        public bool hitTest()
        {
            bool result = false;
            return result;
        }
        void Update()
        {
            if (isMarked)
            {
                if (!isDwell)
                {
                    leave();
                }
            }
        }

        void OnTriggerEnter(Collider collision)
        {
                dwellOn();
        }
        void OnTriggerExit(Collider collision)
        {
                leave();
        }

        void OnTriggerStay(Collider collision)
        {
                isDwell = true;
        }
        public void keyEnter()
        {
            boxRenderer.material.color = Color.blue;
            DC.GetComponent<Dynamic_cascade>().keyEntered(key);
        }
        IEnumerator DwellChecker()
        {
            isMarked = true;
            while (isDwell)
            {
                dwell = (float)(TimeUtils.currentTimeMillis() - startTime)/1000;

                if (dwell >= Dynamic_cascade.baseDwell)
                {
                    //boxRenderer.material.color = boxColor;
                    keyEnter();
                    yield break;
                }else if (dwell >= dwell_pre)
                {
                    if(borderRenderer.material.color.b < 1)
                    {
                        borderRenderer.material.color = Color.blue;
                        DC.GetComponent<Dynamic_cascade>().keyActivatedRecording(key);
                    }
                }
                float remain = Dynamic_cascade.baseDwell - dwell;
                float wait_next = remain < wait ? remain : wait;
                yield return new WaitForSecondsRealtime(wait);
            }
        }
        // http://theeye.pe.kr/archives/2725
    }
    
}