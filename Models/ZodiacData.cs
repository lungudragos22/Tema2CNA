using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tema2.Models
{
    public class ZodiacData
    {
        public string Name { get; set; }
        public int StartDay { get; set; }
        public int StartMonth { get; set; }
        public int EndDay { get; set; }
        public int EndMonth { get; set; }
    }
}
