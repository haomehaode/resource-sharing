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
    
    public partial class ObjectTags
    {
        public int OT_ID { get; set; }
        public int Obj_Type { get; set; }
        public int Obj_ID { get; set; }
        public int Obj_TagID { get; set; }
        public string Extends1 { get; set; }
        public string Extends2 { get; set; }
        public string Extends3 { get; set; }
        public string Extends4 { get; set; }
    
        public virtual Navigations Navigations { get; set; }
        public virtual OptionType OptionType { get; set; }
    }
}
