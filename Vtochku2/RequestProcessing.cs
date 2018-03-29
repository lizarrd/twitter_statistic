using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vtochku2
{
        public class RequestProcessing
    {
            public int count { get; set; }
            public int total { get; set; }
        }

        public class X32
        {
            public string portal { get; set; }
        }

        public class X24
        {
            public string portal { get; set; }
        }

        public class X256
        {
            public string wowp { get; set; }
        }

        public class X64
        {
            public string wot { get; set; }
            public string portal { get; set; }
        }

        public class X195
        {
            public string portal { get; set; }
        }

        public class Emblems
        {
            public X32 x32 { get; set; }
            public X24 x24 { get; set; }
            public X256 x256 { get; set; }
            public X64 x64 { get; set; }
            public X195 x195 { get; set; }
        }

        public class Datum
        {
            public int members_count { get; set; }
            public string name { get; set; }
            public string color { get; set; }
            public int created_at { get; set; }
            public string tag { get; set; }
            public Emblems emblems { get; set; }
            public int clan_id { get; set; }
        }

        public class RootObject
        {
            public string status { get; set; }
            public RequestProcessing meta { get; set; }
            public List<Datum> data { get; set; }
        }


    }
