//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToshkentGullari.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Answer
    {
        public int ID { get; set; }
        public int AdminID { get; set; }
        public string Answer1 { get; set; }
        public int QuestionID { get; set; }
        public System.DateTime TimeAnswered { get; set; }
    
        public virtual Support Support { get; set; }
    }
}
