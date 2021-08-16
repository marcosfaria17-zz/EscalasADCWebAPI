namespace EscalasADCWebAPI.Data.Collections
{
    public class Voluntario
    {
        public string Nome { get; set; }
        public string Instrumento { get; set; }
        public Voluntario(string nome, string instrumento)
        {
            this.Nome = nome;
            this.Instrumento = instrumento;
        }
    }
}