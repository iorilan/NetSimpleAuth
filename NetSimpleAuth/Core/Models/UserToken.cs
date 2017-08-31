using Newtonsoft.Json;

namespace Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserToken")]
    public partial class UserToken
    {

        [Key]
        public long Id { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpireAt { get; set; }

        public long UserId { get; set; }
    }
}
