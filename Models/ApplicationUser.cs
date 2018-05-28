using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TKPMGD_NET.Data;

namespace TKPMGD_NET.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() {
            Sentences = new List<Sentence>();
            TestCases = new List<TestCase>();
        }
        public IList<Sentence> Sentences { get; set; }
        public IList<TestCase> TestCases { get; set; }
    }
}
