using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMusicAPI.Entity
{
   
        public class Base
        {
           
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public string Id { get; set; }

          
            public string UserId { get; set; }
            public DateTime UpdateDate { get; set; } = DateTime.Now;
            public DateTime CreateDate { get; set; } = DateTime.Now;
            public bool Status { get; set; } = true;
        }
    
}
