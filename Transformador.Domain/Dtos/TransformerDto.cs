using System.ComponentModel.DataAnnotations;

namespace Transformador.Domain.Dtos
{
    public class TransformerDto
    {
        public string Name { get; set; }
        public int InternalNumber { get; set; }
        public double TensionClass { get; set; }
        public double Potency { get; set; }
        public double Current { get; set; }

        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Valor de Id inválido!")]
        public string UserId { get; set; }
    }
}