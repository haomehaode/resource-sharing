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
    
    public partial class ResourceComment
    {
        public int RC_ID { get; set; }
        public int User_ID { get; set; }
        public int RC_Target { get; set; }
        public string RC_Content { get; set; }
        public System.DateTime RC_Time { get; set; }
    
        public virtual ResourceInfo ResourceInfo { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
