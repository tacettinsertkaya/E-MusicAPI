using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Entity
{
    public class Base
    {
        [JsonProperty("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }
        [JsonProperty("updateDate")]
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        [JsonProperty("createDate")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [JsonProperty("status")]
        public bool Status { get; set; } = true;
    }
}
