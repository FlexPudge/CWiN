//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceCenter.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Types_of_faults
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Types_of_faults()
        {
            this.Orders = new HashSet<Orders>();
        }
    
        public int Id_types { get; set; }
        public Nullable<int> Id_models { get; set; }
        public string Description { get; set; }
        public string Simptoms { get; set; }
        public string Metodi_remonta { get; set; }
        public Nullable<int> id_zapchasti_1 { get; set; }
        public Nullable<int> id_zapchasti_2 { get; set; }
        public Nullable<int> id_zapchasti_3 { get; set; }
        public string Price_work { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual Remont_Models Remont_Models { get; set; }
        public virtual Spare_parts Spare_parts { get; set; }
        public virtual Spare_parts Spare_parts1 { get; set; }
        public virtual Spare_parts Spare_parts2 { get; set; }
    }
}