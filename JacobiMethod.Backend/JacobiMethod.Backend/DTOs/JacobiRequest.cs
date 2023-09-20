namespace JacobiMethod.Backend.DTOs;

public class JacobiRequest
{
    public double Error { get; set; }
    public double[][] A { get; set; }
    public double[] B { get; set; }
}