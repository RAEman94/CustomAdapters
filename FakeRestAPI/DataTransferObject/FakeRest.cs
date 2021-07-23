using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeRestAPI.DataTransferObject
{
    public class FakeRestDTO
    {
        public long userId { get; set; }
        public long id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}
