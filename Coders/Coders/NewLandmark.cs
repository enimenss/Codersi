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
    
    public partial class NewLandmark
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NewLandmark()
        {
            this.UserRatings = new HashSet<UserRating>();
        }
    
        public int Id { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public Nullable<int> Rate { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> UserId { get; set; }
    
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRating> UserRatings { get; set; }
    }
}
