//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CongNgheWeb_3_0.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_User
    {
        public tbl_User()
        {
            this.tbl_Course = new HashSet<tbl_Course>();
        }
    
        public int ID { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string LoginName { get; set; }
        public string PasswordUser { get; set; }
        public Nullable<bool> Employee { get; set; }
        public string DescriptionUser { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string NumberPhone { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Clock { get; set; }
        public Nullable<bool> Admin { get; set; }
        public Nullable<bool> Male { get; set; }
        public Nullable<int> PositionID { get; set; }
        public Nullable<bool> Deleted { get; set; }
    
        public virtual tbl_Position tbl_Position { get; set; }
        public virtual ICollection<tbl_Course> tbl_Course { get; set; }
    }
}