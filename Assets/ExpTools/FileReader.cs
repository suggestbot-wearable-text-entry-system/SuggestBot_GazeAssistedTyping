using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.ExpTools
{
    class FileReader
    {
        string path = "./phrases.txt";
        string raw;
        string[] examples;
        int numExamples;
        Random rand;
        public FileReader()
        {
            rand = new Random();
            raw = System.IO.File.ReadAllText(path);
            examples = raw.Split('\n');
            numExamples = examples.Length;
            for (int i = 0; i < numExamples; i++)
            {
                examples[i] = examples[i].Split('\r')[0];
                examples[i] = examples[i].ToLower();
            }

        }

        public string getRandomPhrase()
        {
            return examples[rand.Next(0, numExamples)];
        }
    }
}
