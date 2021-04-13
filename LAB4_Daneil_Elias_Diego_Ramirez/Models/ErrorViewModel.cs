using System;

namespace LAB4_Daneil_Elias_Diego_Ramirez.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
