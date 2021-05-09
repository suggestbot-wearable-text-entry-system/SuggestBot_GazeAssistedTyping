using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.ExpTools;

namespace Assets.Keyboards
{
    public class GAT_keyboard : MonoBehaviour
    {
        private Material material;
        private MeshFilter border;
        private MeshRenderer borderRenderer;
        private MeshRenderer boxRenderer;
        GameObject GAT;

        Color boxColor;
        private long startTime = TimeUtils.currentTimeMillis();
        bool isDwell = false;
        float wait = 0.05f; // in ms
        public int id;
        public bool selection = false;
        public string basicKeys;
        public Text key;
        public char[] keys;
        string wordHighlightColor = "<color=#ff0000>";
        string wordHighlightColorEnd = "</color>";
        string centerHighlightColor = "<color=#00ff00>";

        void Start()
        {
            boxColor = Color.black;

            border = GetComponentsInChildren<MeshFilter>()[1];
            borderRenderer = border.GetComponent<MeshRenderer>();
            borderRenderer.material.color = Color.black;

            boxRenderer = GetComponentsInChildren<MeshFilter>()[0].GetComponent<MeshRenderer>();
            boxRenderer.material.color = boxColor;

            material = gameObject.GetComponent<Renderer>().material;
            

            GAT = GameObject.Find("GAT9");
            if (material == null)
                gameObject.SetActive(false);
        }

        void Update()
        {
        }
        public void setBasicKeys(char a, char b, char c)
        {
            
            basicKeys = a + centerHighlightColor + b + wordHighlightColorEnd + c;
            key.text = basicKeys;
            keys = new char[3];
            keys[0] = a;
            keys[1] = b;
            keys[2] = c;
        }

        public bool highlighting(int i)
        {
            if (!selection)
            {
                selection = true;
                switch (i)
                {
                    case 0:

                        key.text = wordHighlightColor + keys[0] + wordHighlightColorEnd + centerHighlightColor + keys[1] + wordHighlightColorEnd + keys[2];
                        break;
                    case 1:

                        key.text =  keys[0]  + wordHighlightColor + keys[1] + wordHighlightColorEnd + keys[2];
                        break;
                    case 2:

                        key.text =  keys[0] + centerHighlightColor + keys[1] + wordHighlightColorEnd + wordHighlightColor + keys[2] + wordHighlightColorEnd;
                        break;
                }
                /*
                string str = "";
                int k;
                for (k = 0; k < i; k++)
                {
                    str += keys[k];
                }
                str += wordHighlightColor + keys[k++] + wordHighlightColorEnd;
                for (; k < 3; k++)
                {
                    str += keys[k];
                }
                key.text = str;*/
                StartCoroutine("resetHighlighting");
            }
            return selection;
        }
        IEnumerator resetHighlighting()
        {
            yield return new WaitForSecondsRealtime(0.05f);
            key.text = basicKeys;
            selection = false;
        }
        public void activate()
        {
            borderRenderer.material.color = Color.blue;
        }
        public void deactivate()
        {
            borderRenderer.material.color = Color.black;
        }
    }

}