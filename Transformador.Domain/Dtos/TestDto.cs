using System.ComponentModel.DataAnnotations;

namespace Transformador.Domain.Dtos
{
    public class TestDto
    {
        public string Name { get; set; }
        public int DurationInSeconds { get; set; }
        public string TransformerId { get; set; }
    }
}