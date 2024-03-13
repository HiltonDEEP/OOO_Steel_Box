//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OOO_Steel_Box.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PCBuilds
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PCBuilds()
        {
            this.Orders = new HashSet<Orders>();
        }
    
        public int PCBuildID { get; set; }
        public string PCBuildName { get; set; }
        public int ProcessorID { get; set; }
        public int VideocardID { get; set; }
        public int MotherboardID { get; set; }
        public int HardDriveID { get; set; }
        public int SolidStateDriveID { get; set; }
        public int PowerUnitID { get; set; }
        public int FrameID { get; set; }
        public int CoolerID { get; set; }
        public string PCBuildDescription { get; set; }
        public int PCBuildPrice { get; set; }
        public Nullable<int> PCBuildDiscount { get; set; }
        public string PCBuildImage { get; set; }
    
        public virtual Coolers Coolers { get; set; }
        public virtual Frames Frames { get; set; }
        public virtual HardDrives HardDrives { get; set; }
        public virtual Motherboards Motherboards { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual PowerUnits PowerUnits { get; set; }
        public virtual Processors Processors { get; set; }
        public virtual SolidStateDrivers SolidStateDrivers { get; set; }
        public virtual Videocards Videocards { get; set; }
    }
}