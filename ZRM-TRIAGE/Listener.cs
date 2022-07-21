using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ZRM_TRIAGE
{
    public static class Listener
    {
        public delegate void Subscribe(ref Label label);
        public static event Subscribe SubEvent;

        public static void Refresh(ref Label label)
        {
            SubEvent(ref label);
        }
    }
}
