using Microsoft.Azure.CosmosRepository;
using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace BlastOff.Shared
{
    public class Image
    {
        public string Title { get; set; }
        public string Copyright { get; set; }
        public DateTime Date { get; set; }
        public string Explanation { get; set; }
        public string Url { get; set; }
        public string HdUrl { get; set; }
    }


}