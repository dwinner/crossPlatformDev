using System.ComponentModel.DataAnnotations;

namespace SocialService.Models
{
   public class Friend
   {
      [Key]
      public int Id { get; set; }

      [Required]
      public string Name { get; set; }

      public string Email { get; set; }

      public string Phone { get; set; }
   }
}