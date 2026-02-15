using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyFxOData.Models
{
    public class CurrencyFXRate
    {
        [Key]
        [MaxLength(25)]
        public string UniqueNameDate { get; set; } = default!;

        [MaxLength(20)]
        public string UniqueName { get; set; } = default!;

        [MaxLength(80)]
        public string Name { get; set; } = default!;

        [MaxLength(3)]
        public string CurrencyFrom { get; set; } = default!;

        [MaxLength(3)]
        public string CurrencyTo { get; set; } = default!;

        public DateTime RateDate { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal ExchangeRate { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal CorporateDailyInvoiceRate { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal CorporateDailyInvoiceInverseRate { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal CorporateMonthEndRate { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal CorporateMonthEndInverseRate { get; set; }
    }
}