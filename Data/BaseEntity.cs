using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TKPMGD_NET.Models;

namespace TKPMGD_NET.Data
{
    public class BaseEntity
    {
        public BaseEntity() {
            AddedDate = DateTime.Now;
        }
        public long Id { get; set; }
        [Display(Name = "Thời gian cập nhật")]
        public DateTime AddedDate { get; set; }
        [Display(Name = "Người chỉnh sửa")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }
    }
}
