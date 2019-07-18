namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Footer")]
    public partial class Footer
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Link { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }

        public int? DisplayOrder { get; set; }

        public long? ParentID { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        public bool? Target { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public bool? Status { get; set; }

        public int? FooterType_ID { get; set; }
    }
}
