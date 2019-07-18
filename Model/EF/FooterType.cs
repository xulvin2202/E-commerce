namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FooterType")]
    public partial class FooterType
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }
    }
}
