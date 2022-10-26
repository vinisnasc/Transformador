namespace Transformador.Domain.Dtos.ViewModels
{
    public class TransformerVMComplete
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int InternalNumber { get; set; }
        public double TensionClass { get; set; }
        public double Potency { get; set; }
        public double Current { get; set; }
        public UserVM User { get; set; }
        public List<TestVMComplete> Testes { get; set; }
    }
}