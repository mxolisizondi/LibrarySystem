//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibrarySystem.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class IssueBooks
    {
        public int IssueID { get; set; }
        public int StudentID { get; set; }
        public int BookID { get; set; }
        public int StaffID { get; set; }
        public int NoOfCopies { get; set; }
        public System.DateTime DateOfIssue { get; set; }
        public System.DateTime DateOfReturn { get; set; }
        public int Status { get; set; }
    
        public virtual Books Books { get; set; }
        public virtual Staffs Staffs { get; set; }
        public virtual Students Students { get; set; }
    }
}
