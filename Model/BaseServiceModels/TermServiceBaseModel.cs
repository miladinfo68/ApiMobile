namespace Model.BaseServiceModels
{
    public class TermServiceBaseModel : BaseServiceBaseModel
    {
        public string TermCode { get; set; }
        public bool IsCurrentTerm { get; set; }
    }
}
