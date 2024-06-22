using System.ComponentModel.DataAnnotations;

namespace INSPECTORV2.Domain.Entities
{
    public class Inspector
    {
        public int Id { get; set; }
        [MaxLength(length: 60)]// نوعية الحقل 

        public string Name { get; set; }
        [MaxLength(length: 24)]

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
        [MaxLength(length: 60)]

        public string Address { get; set; }
        [MaxLength(length: 60)]

        public int Age { get; set; }
        [MaxLength(length: 60)]
        public string Speialiation { get; set; }
        [MaxLength(length: 24)]
        public List<Teacher> Teachers { get; set; }
    }
}
