using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    public static class UserInfo
    {

        public static string EventId { get; private set; }      
        public static string AmbulanceNumber { get; private set; } 

        public static void SetEventId(string eventId)
        {
            EventId = eventId;
        }

        public static void SetAmbulanceNumber(string ambulanceNumber)
        {
            AmbulanceNumber = ambulanceNumber;
        }

    }
}
