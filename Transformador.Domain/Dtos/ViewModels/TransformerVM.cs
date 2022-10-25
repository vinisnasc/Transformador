namespace Transformador.Domain.Dtos.ViewModels
{
    public class TransformerVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int InternalNumber { get; set; }
        public double TensionClass { get; set; }
        public double Potency { get; set; }
        public double Current { get; set; }
        public string UserId { get; set; }
    }
}