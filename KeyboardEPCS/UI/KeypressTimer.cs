using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KeyboardEPCS.Logic;
using KeyboardEPCS.Logic.Inputs;
using KeyboardEPCS.Logic.Transitions;

namespace KeyboardEPCS.UI
{
    class KeypressTimer : Timer
    {
        const int MAXIMUM_TIME_BETWEEN_KEYS = 200000;
        readonly TransitionTimeBuilder times;
        KeyboardPosition lastPosition;
        DateTime lastTime;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        public KeypressTimer(TransitionTimeBuilder times)
        {
            this.times = times;
            Interval = 10;
            Enabled = true;
        }

        readonly ISet<Keys> pressedKeys = new HashSet<Keys>(); 
        protected override void OnTick(EventArgs e)
        {
            base.OnTick(e);

            foreach (var key in Enum.GetValues(typeof(Keys)).OfType<Keys>())
            {
                if (IsPressed(key))
                {
                    pressedKeys.Add(key);
                } else if (pressedKeys.Contains(key))
                {
                    pressedKeys.Remove(key);
                    KeyReleased(key);
                }
            }
        }

        void KeyReleased(Keys releasedKey)
        {
            var releasedChar = GetKeyChar(releasedKey);
            if (releasedKey == Keys.Space || releasedChar == null)
            {
                lastPosition = null;
                return;
            }

            var now = DateTime.Now;
            var elapsedTime = (now - lastTime).TotalMilliseconds;
            var currentPosition = KeyboardLayout.QWERTY.GetCharPosition(releasedChar.Value);
            if (lastPosition != null && elapsedTime < MAXIMUM_TIME_BETWEEN_KEYS)
            {
                times[currentPosition, lastPosition].AddMeasurement(elapsedTime);
                Console.WriteLine(string.Format("adding measurement {0}", elapsedTime));
            }
            lastPosition = currentPosition;
            lastTime = now;
        }

        static char? GetKeyChar(Keys singlyPressedChar)
        {
            foreach (var charr in Keyboard.StandardKeyboard.AllChars.Where(charr => GetCharKey(charr) == singlyPressedChar))
                return charr;
            return null;
        }

        static bool IsPressed(Keys charKey)
        {
            return GetAsyncKeyState(charKey) != 0;
        }

        static Keys GetCharKey(char charr)
        {
            Keys result;
            var success = Enum.TryParse(charr.ToString(), true, out result);
            if (success)
                return result;
            
            var charInt = (int)charr;
            switch (charInt)
            {
                case 59: 
                case 26:
                    return Keys.OemSemicolon;
                case 46:
                    return Keys.OemPeriod;
                case 47: 
                    return Keys.OemBackslash;
                case 44:
                case 27:
                    return Keys.Oemcomma;
                case 28:
                    return Keys.OemPeriod;
                case 29:
                    return Keys.OemQuestion;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}