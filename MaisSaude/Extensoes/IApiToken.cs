namespace MaisSaude.Extensoes
{
    public interface IApiToken
    {
        Task<string> Obter();
    }
}
