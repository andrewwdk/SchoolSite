//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolWebSite2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public int PersonId { get; set; }
        public Nullable<int> Class { get; set; }
    
        public virtual Class Class1 { get; set; }
        public virtual Person Person { get; set; }
    }
}
