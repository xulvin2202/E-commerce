namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Brand")]
    public partial class Brand
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(550)]
        public string Link { get; set; }

        public long? ParentID { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? Status { get; set; }

        public long? Product_Category_ID { get; set; }

        public long? Category_ID { get; set; }
    }
}
