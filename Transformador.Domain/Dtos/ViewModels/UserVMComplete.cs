namespace Transformador.Domain.Dtos.ViewModels
{
    public class UserVMComplete
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<TransformerVM> Transformers { get; set; }
    }
}