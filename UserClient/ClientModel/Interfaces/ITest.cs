namespace ClientModels.Interfaces
{
    public interface ITest
    {
        long TestID { get; set; }
        string InputArgs { get; set; }
        string ExpectedOutput { get; set; }
    }
}