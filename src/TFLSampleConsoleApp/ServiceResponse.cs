using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TFLSampleConsoleApp
{
    public class ServiceResponse
    {
        public string response { get; set; }

        public bool IsSuccessStatusCode { get; set; }
    }
}
