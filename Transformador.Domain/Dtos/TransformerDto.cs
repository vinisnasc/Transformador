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
        public string UserId { get; set; }
    }
}