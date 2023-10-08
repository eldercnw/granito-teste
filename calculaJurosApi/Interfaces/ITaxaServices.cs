namespace calculaJurosApi.Interfaces
{
    public interface ITaxaServices
    {
        public  Task<decimal?> GetValorTotalAsync(decimal valorInicial, int meses);
    }
}
