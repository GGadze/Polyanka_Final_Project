//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Polyanka_1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Delivery
    {
        public int id { get; set; }
        public int id_supplies { get; set; }
        public int id_storehouse { get; set; }
        public System.DateTime date_of_delivery { get; set; }
        public decimal full_cost { get; set; }
    
        public virtual Storehouse Storehouse { get; set; }
        public virtual Supplies Supplies { get; set; }
    }
}
