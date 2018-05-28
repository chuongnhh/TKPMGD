using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TKPMGD_NET.Data {
    public class Sentence : BaseEntity {
        public string Content { get; set; }

        public long TestCaseId { get; set; }
        public virtual TestCase TestCase { get; set; }
        public int Time { get; set; }
    }
}
