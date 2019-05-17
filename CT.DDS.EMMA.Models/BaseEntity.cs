using System;
using System.Collections.Generic;
using System.Text;

namespace CT.DDS.EMMA.Models
{
   public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime SysStart { get; set; }
        public DateTime SysEnd { get; set; }
        public string SysUser { get; set; }
        public string SysUserNext { get; set; }
    }
}
