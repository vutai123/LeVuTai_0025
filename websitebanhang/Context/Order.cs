//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace websitebanhang.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
<<<<<<< HEAD
        public Nullable<int> UserId { get; set; }
=======
        public Nullable<int> ProductId { get; set; }
        public Nullable<double> Price { get; set; }
>>>>>>> 16270bbe4a96228c793af4ab11fb6972cea915fd
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
    }
}
