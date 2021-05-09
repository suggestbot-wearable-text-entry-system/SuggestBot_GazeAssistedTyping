using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.ExpTools
{
    public class GazeTouchEvent
    {
        public const int STATE_NONE = 0;
        public const int STATE_GAZE_AT_DONE = 10;
        public const int STATE_GAZE_AT_EXAMPLE = 20;
        public const int STATE_GAZE_AT_KEYBOARD = 30;
        public const int STATE_INPUTED = 40;

        public const int GESTURE_NONE = -1;
        public const int GESTURE_KEY_MISS = 0;
        public const int GESTURE_TOUCH_DOWN = 1;
        public const int GESTURE_TOUCH_UP = 2;
        public const int GESTURE_KEY_INPUTED = 3;
        public const int GESTURE_KEY_HIGHLIGTED = 4;
        public const int GESTURE_SWIPE_LEFT = 5;
        public const int GESTURE_SWIPE_RIGHT = 6;
        public int state;
        public int xPos;
        public int yPos;
        public int gesture;
        public char selected;
        public char eventLabel;
        public int nth;
        public int activatedWindow;

        public float xGazePos;
        public float yGazePos;
        public int eyeOpen;

        public long eventTime;

        public GazeTouchEvent()
        {
            eventLabel = '-';
            xPos = -1;
            yPos = -1;
            xGazePos = -1;
            yGazePos = -1;
            gesture = GESTURE_NONE;
            selected = (char)0;
            state = STATE_NONE;
            activatedWindow = -1;
            nth = -1;
            eyeOpen = -1;
            eventTime = TimeUtils.currentTimeMillis();
        }
        public GazeTouchEvent(GazeTouchEvent e)
        {
            nth = e.nth;
            eventLabel = e.eventLabel;
            xPos = e.xPos;
            yPos = e.yPos;
            xGazePos = e.xGazePos;
            yGazePos = e.yGazePos;
            gesture = e.gesture;
            selected = e.selected;
            state = e.state;
            eyeOpen = e.eyeOpen;
            activatedWindow = e.activatedWindow;
            eventTime = e.eventTime;
        }

        public GazeTouchEvent(int screenState, int x, int y, int gx, int gy)
        {
            eventLabel = '-';
            eventTime = TimeUtils.currentTimeMillis();
            state = screenState;
            xPos = x;
            yPos = y;
            xGazePos = gx;
            yGazePos = gy;
            eyeOpen = -1;
            gesture = GESTURE_NONE;
            selected = (char)0;

            gesture = -1;
        }
        public GazeTouchEvent(int screenState, int x, int y, int gx, int gy, long eventTime)
        {
            eventLabel = '-';
            this.eventTime = eventTime;
            state = screenState;
            xPos = x;
            yPos = y;
            xGazePos = gx;
            eyeOpen = -1;
            yGazePos = gy;
            selected = (char)0;

            gesture = GESTURE_NONE;
        }

        public GazeTouchEvent(int screenState, int gesture, int x, int y, int gx, int gy, long eventTime)
        {
            eventLabel = '-';
            this.eventTime = eventTime;
            state = screenState;
            this.gesture = gesture;
            xPos = x;
            yPos = y;
            xGazePos = gx;
            yGazePos = gy;
            eyeOpen = -1;
            selected = (char)0;
        }
        public GazeTouchEvent(int activatedWindow, int screenState, int gesture, int x, int y, int gx, int gy, long eventTime)
        {
            eventLabel = '-';
            this.activatedWindow = activatedWindow;
            this.eventTime = eventTime;
            state = screenState;
            this.gesture = gesture;
            xPos = x;
            yPos = y;
            xGazePos = gx;
            yGazePos = gy;
            eyeOpen = -1;
            selected = (char)0;
        }

        public string ToString(long originTime)
        {

            if (selected == 0)
            {
                return "" + nth + "," + (int)(eventTime - originTime) + "," + xGazePos + "," + yGazePos + "," + state + "," + activatedWindow + "," + xPos + "," + yPos + "," + gesture + ",," + eventLabel+","+eyeOpen;
            }
            else
            {
                if (eventLabel == 'w') return "" + nth + "," + (int)(eventTime - originTime) + "," + xGazePos + "," + yGazePos + "," + state + "," + activatedWindow + "," + xPos + "," + yPos + "," + gesture + "," + selected + "," + eventLabel + "," + eyeOpen;
                else return "" + nth + "," + (int)(eventTime - originTime) + "," + xGazePos + "," + yGazePos + "," + state + "," + activatedWindow + "," + xPos + "," + yPos + "," + gesture + "," + selected + "," + eventLabel + "," + eyeOpen;

            }
        }

    }

}
