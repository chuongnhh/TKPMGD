using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TKPMGD_NET.Data.Mapper {
    public class TestCaseMap {
        public TestCaseMap(EntityTypeBuilder<TestCase> builder) {
            builder.HasKey(t => t.Id);
            builder.HasOne(e => e.User).WithMany(e => e.TestCases).HasForeignKey(e => e.UserId);
        }
    }
}
