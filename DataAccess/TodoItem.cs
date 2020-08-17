namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TodoItem")]
    public partial class TodoItem
    {
        public int Id { get; set; }

        [StringLength(60)]
        public string Name { get; set; }

        public bool? IsComplete { get; set; }
    }
}
