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
    
    public partial class ResourceType
    {
        public ResourceType()
        {
            this.ResourceInfo = new HashSet<ResourceInfo>();
        }
    
        public int RT_ID { get; set; }
        public string RT_Name { get; set; }
    
        public virtual ICollection<ResourceInfo> ResourceInfo { get; set; }
    }
}