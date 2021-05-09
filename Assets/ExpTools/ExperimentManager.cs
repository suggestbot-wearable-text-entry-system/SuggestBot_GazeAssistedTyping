using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;


namespace Assets.ExpTools
{
    public class ExperimentManager
    {
        public const int maxBlockDefault = 3;
        public const int maxTrialDefault = 4;
        //for Log
        //System.IO.File rawDataFile, resultFile;
        string rawDataPath, resultPath, keyInputPath;
        ArrayList gazeTouchEvents, inputEvents;
        string inputed;
        int inputCount;
        Measure measure;
        long prevEventTime;
        long startTime;

        public static int participant = 999;


        bool isFirstInput = true;
        FileReader exampleManager;
        string currentTarget;
        private int block, trial;
        private int tempTrial;

        private bool enableLoggingUpdate = true;
        private bool done;
        private bool blockDone;

        public int maxBlock, maxTrial;
        int num = 0;

        public ExperimentManager(int p, int startTrial, int startBlock, int maxBlock, int maxTrial, string expInfo)
        {
            participant = p;
            block = startBlock;
            trial = startTrial;
            expInfo = expInfo + "_" + participant;
            measure = new Measure(expInfo);
            currentTarget = "abcdefghijklmnopqrstuvwxyz";
            measure.setGoalString(currentTarget);

            rawDataPath = expInfo + "_raw_" + num + ".csv";
            System.IO.FileInfo fi = new System.IO.FileInfo(rawDataPath);
            while (fi.Exists)
            {
                num++;
                rawDataPath = expInfo + "_raw_" + num + ".csv";
                fi = new System.IO.FileInfo(rawDataPath);

            }
            rawDataPath = expInfo + "_raw_" + num + ".csv";
            resultPath = expInfo + "_result_" + num + ".csv";
            keyInputPath = expInfo + "_key_" + num + ".csv";
            exampleManager = new FileReader();
            writeHeaders();
            this.maxBlock = maxBlock;
            this.maxTrial = maxTrial;

            inputEvents = new ArrayList();
            gazeTouchEvents = new ArrayList();
            inputCount = 0;

            done = false;
            startTime = TimeUtils.currentTimeMillis();
            prevEventTime = startTime;
        }

        public void setAdditionalPhase()
        {
            tempTrial = trial;
            trial = 0;
            currentTarget = "<Additional>";
            enableLoggingUpdate = true;
        }
        public void recoverPhase()
        {
            enableLoggingUpdate = false;
            trial = tempTrial;
        }
        public void writeHeaders()
        {
            System.IO.File.WriteAllText(rawDataPath, "block,trial,nth,eventTime,gazeX,gazeY,screenState,activated,posX,posY,gesture,inputed,eventLabel,eyeOpen");
            System.IO.File.WriteAllText(resultPath, "block,trial,example,result,wpm,cer,uer,completionTime");
            System.IO.File.WriteAllText(keyInputPath, "block,trial,example,nth,input,inputTime,cummulated");
        }

        public bool isDone()
        {
            return done;
        }

        public void recorRawData()
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(rawDataPath, true);
            string progress = Environment.NewLine + block + "," + trial + ",";
            GazeTouchEvent e;
            for (int i = 0; i < gazeTouchEvents.Count; i++)
            {
                if (gazeTouchEvents[i] != null)
                {
                    e = (GazeTouchEvent)gazeTouchEvents[i];
                    string temp = e.ToString(startTime);
                    sw.Write(progress + temp);
                }
                else
                {
                    string str = "shit?";
                }
            }
            gazeTouchEvents.Clear();
            sw.Close();


        }
        public void setEnableLogging()
        {

            enableLoggingUpdate = true;
        }
        public bool getEnableLoggingUpdate()
        {
            return enableLoggingUpdate;
        }
        public void recordResult()
        {
            System.IO.File.AppendAllText(resultPath, "\n" + block + "," + trial + "," + currentTarget + "," + inputed + "," + measure.getWPM() + "," + measure.getCER() + "," + measure.getUER() + "," + measure.getCompletionTime());
        }

        public void recordInputed()
        {
            string progress = "\n" + block + "," + trial + "," + currentTarget + ",";
            foreach (InputEvent e in inputEvents)
            {
                System.IO.File.AppendAllText(keyInputPath, progress + e.ToString());
            }
            inputEvents.Clear();
            inputCount = 0;

        }

        public void updateInput(char key, string inputed)
        {
            long current = TimeUtils.currentTimeMillis();
            int dt = (int)(current - prevEventTime);
            prevEventTime = current;

            measure.done(key, current);
            if (key == '<')
            {
                measure.userInputBackspace();
            }
            else if (isFirstInput)
            {
                isFirstInput = false;
                measure.start(current);
            }
            inputEvents.Add(new InputEvent(key, inputed, ++inputCount, dt));

        }
        public void appendGazeTouchEvent(GazeTouchEvent e)
        {
            if (enableLoggingUpdate)
            {
                e.nth = inputCount;
                gazeTouchEvents.Add(new GazeTouchEvent(e));
            }
        }

        public void taskDone(string inputed)
        {
            enableLoggingUpdate = false;
            this.inputed = inputed;
            measure.userInputAnalyse(inputed);

        }

        public void recordResults()
        {
            recordResult();
            recorRawData();
            recordInputed();

        }
        public void provideNext()
        {
            addTrial();
            isFirstInput = true;

        }

        public bool addTrial()
        {
            bool update = false;
            currentTarget = exampleManager.getRandomPhrase();
            measure.setGoalString(currentTarget);
            if (block == 0)
            {
                blockDone = true;
                update = true;
                trial = 1;
                block = 1;

            }
            else
            {
                trial++;
                if (trial > maxTrial)
                {
                    blockDone = true;
                    update = true;
                    trial = 1;
                    block++;
                    if (block > maxBlock)
                    {
                        done = true;
                    }
                }
                else
                {
                    blockDone = false;
                }
            }

            return update;
        }
        public string getPhrase()
        {
            return currentTarget;
        }

        public void setBlockDone(bool isdone)
        {
            blockDone = isdone;
        }
        public bool isBlockDone()
        {
            return blockDone;
        }
        public int getBlock()
        {
            return block;
        }
        public int getTrial()
        {
            return trial;
        }

        public string getResultInString()
        {
            return "WPM: " + measure.getWPM() + ", TER: " + measure.getTER() + " (CER: " + measure.getCER() + ", UER: " + measure.getUER() + ")";

        }
    }
}
