//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EONBussiness.DbContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbduan
    {
        public int ID { get; set; }
        public string tenduan { get; set; }
        public string alias { get; set; }
        public string tukhoa { get; set; }
        public string mota { get; set; }
        public string hinhanh { get; set; }
        public Nullable<int> soluong { get; set; }
        public string noidung { get; set; }
        public Nullable<int> viewus { get; set; }
        public Nullable<bool> ghim { get; set; }
        public Nullable<bool> hienthi { get; set; }
        public Nullable<System.DateTime> ngaycapnhat { get; set; }
    }
}
