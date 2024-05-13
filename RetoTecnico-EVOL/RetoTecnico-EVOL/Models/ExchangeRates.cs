using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetoTecnico_EVOL.Models
{
    public class ExchangeRates
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal InitialAmount { get; set; }
        public string InitialCurrency { get; set; }
        public decimal FinalAmount { get; set; }
        public string FinalCurrency { get; set; }
        public DateTime CreationDate { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
