using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.ExpTools;

namespace Assets.Keyboards
{
    public class GAT_keyboard_zone : MonoBehaviour
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
        public string[] highlightKeys;
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
            

            GAT = GameObject.Find("GAT3_adv");
            if (material == null)
                gameObject.SetActive(false);
        }

        void Update()
        {
        }
        public void setBasicKeys(char[] basic)
        {
            int i = 0, h;
            keys = new char[9];
            basicKeys = "";
            highlightKeys = new string[9];
            foreach (char c in basic)
            {
                highlightKeys[i] = "";
                keys[i] = c;
                if (i % 3 == 1)
                {
                    basicKeys += centerHighlightColor;
                }
                basicKeys += c;
                if (i % 3 == 1)
                {
                    basicKeys += wordHighlightColorEnd;
                }
                if (i % 3 < 2)
                {
                    basicKeys += " ";
                }
                if (i % 3 == 2 && i != 8)
                {
                    basicKeys += "\n";
                }
                h = 0; 
                foreach(char k in basic)
                {
                    if(h== i)
                    {
                        basicKeys += wordHighlightColor;

                    }
                    else if (h % 3 == 1)
                    {
                        highlightKeys[i] += centerHighlightColor;
                    }
                    highlightKeys[i] += k;
                    if (h % 3 == 1 || h == i)
                    {
                        highlightKeys[i] += wordHighlightColorEnd;
                    }
                    if(h % 3 < 2)
                    {
                        highlightKeys[i] += " ";
                    }
                    if (h % 3 == 2 && h != 8)
                    {
                        highlightKeys[i] += "\n";
                    }
                    h++;
                }

                i++;
            }

            key.text = basicKeys;
        }

        public bool highlighting(int i)
        {
            if (!selection)
            {
                selection = true;
                key.text = highlightKeys[i];
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