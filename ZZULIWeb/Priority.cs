//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZZULIWeb
{
    using System;
    using System.Collections.Generic;
    
    public partial class Priority
    {
        public Priority()
        {
            this.UserInfo = new HashSet<UserInfo>();
            this.UserInfo1 = new HashSet<UserInfo>();
        }
    
        public int Pri_ID { get; set; }
        public string Pri_Name { get; set; }
        public string Extends1 { get; set; }
        public string Extends2 { get; set; }
        public string Extends3 { get; set; }
        public string Extends4 { get; set; }
    
        public virtual ICollection<UserInfo> UserInfo { get; set; }
        public virtual ICollection<UserInfo> UserInfo1 { get; set; }
    }
}
