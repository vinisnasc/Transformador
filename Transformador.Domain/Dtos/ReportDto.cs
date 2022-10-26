using System.ComponentModel.DataAnnotations;

namespace Transformador.Domain.Dtos
{
    public class ReportDto
    {
        public string Name { get; set; }
        public bool Status { get; set; }

        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Valor de Id inválido!")]
        public string TestId { get; set; }
    }
}