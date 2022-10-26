namespace Transformador.Domain.Dtos.ViewModels
{
    public class TestVMComplete
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public int DurationInSeconds { get; set; }
        public TransformerVMComplete Transformer { get; set; }
    }
}