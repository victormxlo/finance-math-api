namespace FinanceMath.Application
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public T? Value { get; set; }

        public static Result<T> Ok(T value)
            => new() { Success = true, Value = value };

        public static Result<T> Fail(string error)
            => new() { Success = false, Error = error };
    }
}
