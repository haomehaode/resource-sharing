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
    
    public partial class CoursesNotes
    {
        public int CN_ID { get; set; }
        public int Cou_ID { get; set; }
        public int Mov_ID { get; set; }
        public int User_ID { get; set; }
        public string CN_Content { get; set; }
        public System.DateTime CN_Time { get; set; }
        public int CN_Likes { get; set; }
        public decimal CN_Hot { get; set; }
        public string Extends1 { get; set; }
        public string Extends2 { get; set; }
        public string Extends3 { get; set; }
        public string Extends4 { get; set; }
    
        public virtual Courses Courses { get; set; }
        public virtual Move Move { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}