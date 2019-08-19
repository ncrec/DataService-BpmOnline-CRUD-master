using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService_BpmOnline_CRUD
{
    public static class ReqestMethod
    {
        public const string Patch = "PATCH";
        public const string Put = "PUT";
        public const string Merge = "MERGE";
        public const string Post = "POST";

    }
    public class Configuration
    {
        public enum RequestType
        {
            Select=0,
            Update=1,
            Delete=2,
            Insert=3
        }
    }

   
}
