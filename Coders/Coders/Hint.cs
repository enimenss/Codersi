//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Coders
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hint
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
        public Nullable<int> Landmark_Id { get; set; }
        public Nullable<int> Radius { get; set; }
    
        public virtual Landmark Landmark { get; set; }
    }
}
