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
    
    public partial class Orders
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int PCBuildID { get; set; }
        public int OrderStatusID { get; set; }
        public System.DateTime OrderData { get; set; }
        public string OrderDesctription { get; set; }
    
        public virtual OrderStatuses OrderStatuses { get; set; }
        public virtual PCBuilds PCBuilds { get; set; }
        public virtual Users Users { get; set; }
    }
}
