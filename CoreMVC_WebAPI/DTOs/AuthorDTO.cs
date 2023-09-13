using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC_WebAPI.DTOs
{
    public class AuthorDTO
    {
        [Key]
        [Column("au_id")]
        [StringLength(11)]
        [Required]
        [Unicode(false)]
        public string au_id { get; set; } = null!;

        [Column("au_lname")]
        [Required]
        [StringLength(40)]
        [Unicode(false)]
        public string au_lname { get; set; } = null!;

        [Column("au_fname")]
        [StringLength(20)]
        [Required]
        [Unicode(false)]
        public string au_fname { get; set; } = null!;

        [Column("phone")]
        [StringLength(12)]
        [Unicode(false)]
        public string Phone { get; set; } = null!;

        [Column("address")]
        [StringLength(40)]
        [Unicode(false)]
        public string? Address { get; set; }

        [Column("city")]
        [StringLength(20)]
        [Unicode(false)]
        public string? City { get; set; }

        [Column("state")]
        [StringLength(2)]
        [Unicode(false)]
        public string? State { get; set; }

        [Column("zip")]
        [StringLength(5)]
        [Unicode(false)]
        public string? Zip { get; set; }

        [Column("contract")]
        public bool? Contract { get; set; }
    }
}
