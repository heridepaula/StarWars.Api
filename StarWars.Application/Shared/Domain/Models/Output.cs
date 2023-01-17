namespace StarWars.Application.Shared.Domain.Models
{
    public class Output<T>
    {
        public bool Success => Data is not null;
        public T? Data { get; private set; }

        public Output(T? data) 
        { 
            Data = data;
        }

        public Output()
        {

        }

        public void AddResult(T data)
        {
            Data = data;
        }
    }
}
