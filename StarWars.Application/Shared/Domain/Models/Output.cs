namespace StarWars.Application.Shared.Domain.Models
{
    public class Output<T>
    {
        public bool Success => Data is not null;
        public T? Data { get; set; }

        public Output(T? data) 
        { 
            Data = data;
        }
    }
}
