using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Package
    {
        public enum HttpMethod
        {
            Get, Post, Put, Delete
        }

        public HttpMethod Method { get; set; }
        public object Content { get; set; }
    }
}
