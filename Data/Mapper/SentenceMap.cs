using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TKPMGD_NET.Data.Mapper
{
    public class SentenceMap {
        public SentenceMap(EntityTypeBuilder<Sentence> builder) {
            builder.HasKey(t => t.Id);
            builder.HasOne(e => e.User).WithMany(e => e.Sentences).HasForeignKey(e => e.UserId);
            builder.HasOne(e => e.TestCase).WithMany(e => e.Sentences).HasForeignKey(e => e.TestCaseId);
        }
    }
}
