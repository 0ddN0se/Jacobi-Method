using JacobiMethod.Backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JacobiMethod.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class JacobiController : ControllerBase
{
    [HttpPost]
    public IActionResult JacobiMethod([FromBody] JacobiRequest jacobiRequest)
    {
        var swaps = MaxTrack(jacobiRequest.A);
        
        double[] x = new double[5];
        double[] tempX = new double[5];
        double norm;

        do
        {
            for (int i = 0; i < 5; i++)
            {
                tempX[i] = jacobiRequest.B[i];
                
                for (int j = 0; j < 5; j++)
                {
                    if (i != j)
                    {
                        tempX[i] -= jacobiRequest.A[i][j] * x[j];
                    }
                }
                
                tempX[i] /= jacobiRequest.A[i][i];
            }
                
            norm = Math.Abs(x[0] - tempX[0]);
            
            for (int i = 0; i < 5; i++) {
                if (Math.Abs(x[i] - tempX[i]) > norm)
                    norm = Math.Abs(x[i] - tempX[i]);
                
                x[i] = tempX[i];
            }
        } while (norm > jacobiRequest.Error);
        
        ReturnResult(swaps, x);
        
        var response = new JacobiResponse()
        {
            X = x
        };

        return Ok(response);
    }

    private List<Swap> MaxTrack(double[][] a)
    {
        var swap = new List<Swap>();

        for (int i = 0; i < 5; i++)
        {
            var max = i;
            for (int j = 0; j < 5; j++)
            {
                if (a[i][j] > a[i][max])
                {
                    max = j;
                }
            }
            
            for (int j = 0; j < 5; j++)
            {
                (a[j][i], a[j][max]) = (a[j][max], a[j][i]);
            }
            
            swap.Add(new Swap()
            {
                Previous = i,
                Next = max
            });
        }

        return swap;
    }

    private void ReturnResult(List<Swap> swaps, double[] results)
    {
        swaps.Reverse();

        foreach (var swap in swaps)
        {
            (results[swap.Next], results[swap.Previous]) = (results[swap.Previous], results[swap.Next]);
        }
    }

    struct Swap
    {
        public int Previous { get; set; }
        public int Next { get; set; }
    }
}