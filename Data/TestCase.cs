using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TKPMGD_NET.Data {
    public class TestCase : BaseEntity {
        public TestCase() {
            Sentences = new List<Sentence>();
        }
        public IList<Sentence> Sentences { get; set; }
    }
}
