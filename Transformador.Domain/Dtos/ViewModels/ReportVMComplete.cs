namespace Transformador.Domain.Dtos.ViewModels
{
    public class ReportVMComplete
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public TestVMComplete Test { get; set; }
    }
}
