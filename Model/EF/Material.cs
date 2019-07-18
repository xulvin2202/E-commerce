namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Material")]
    public partial class Material
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public long? ParentID { get; set; }

        public long? Category_ID { get; set; }
    }
}
