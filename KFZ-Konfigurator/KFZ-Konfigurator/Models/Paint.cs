//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KFZ_Konfigurator.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Paint
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public PaintCategory Category { get; set; }
        public string Name { get; set; }
    }
}
