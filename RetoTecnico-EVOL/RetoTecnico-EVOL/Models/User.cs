using System.ComponentModel.DataAnnotations;

namespace RetoTecnico_EVOL.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public Int64 GoRestId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public IEnumerable<ExchangeRates> ExchangeRates { get; set; }
    }
}
